import { Button, Card, Col, Form, Input, Modal, Row, Select, Space, Tooltip, Image, Upload, UploadFile } from 'antd';
import React, { useEffect, useState } from 'react';
import imageIcon from "../../assets/Image.svg";
import { CapNhatDiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import { diemDatQuangCaoAPI } from '../../apis/diemDatQuangCao';
import { LoaiViTriModel } from '../../apis/loaiViTri/model';
import { HinhThucQuangCaoModel } from '../../apis/hinhThucQuangCao/model';
import { loaiViTriAPI } from '../../apis/loaiViTri';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';
import { PlusOutlined } from '@ant-design/icons';
import axios from 'axios';
import { getDistrict, getWardByDistrict } from '../../utils/getWard';
import { hinhThucQuangCaoAPI } from '../../apis/hinhThucQuangCao';

interface Props {
    diemDatQuangCao?: CapNhatDiemDatQuangCaoModel;
    onCancel:()=>void;
}

const tinhTrangDiemDatQuangCao = [
    {
        ma:"DaQuyHoach",
        ten:"Đã quy hoạch",
    },
    {
        ma:"ChuaQuyHoach",
        ten:"Chưa quy hoạch"
    }
]

const getBase64 = (file) =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
});


export function ModalUpdateDiemDatQuangCao(props: Props): JSX.Element {
    const { diemDatQuangCao, onCancel } = props;
    const [form] = Form.useForm<CapNhatDiemDatQuangCaoModel>();
    const [loading, setLoading] = useState(false);
    const [user,setUser] = useState<UserStorage>();
    const [fileList,setFileList] = useState<UploadFile[]>();
    const [loaiViTris,setLoaiViTris] = useState<LoaiViTriModel[]>([]);
    const [hinhThucQuangCaos,setHinhThucQuangCaos] = useState<HinhThucQuangCaoModel[]>([]);
    const [isVisible, setIsVisible] = useState(false);
    const [srcImage, setSrcImage] = useState('');

    useEffect(() => {
        if(diemDatQuangCao)
        {
            getLoaiViTris();
            getHinhThucQuangCaos();
            form.setFieldValue('quan', "Quận " + getDistrict(diemDatQuangCao.quan).name)
            form.setFieldValue('idDiemDatQuangCao', "Phường " + getWardByDistrict(diemDatQuangCao.quan,diemDatQuangCao.phuong).name)
            const fileImages:UploadFile[] = [];
            for (const image of diemDatQuangCao.danhSachHinhAnh) {
                const imageInfo: UploadFile = {
                    name: image,
                    uid: image,
                    status: 'done',
                    url: `${process.env.REACT_APP_BASE_API}Upload/image/${image}`,
                    thumbUrl: `${process.env.REACT_APP_BASE_API}Upload/image/${image}`,
                };
                fileImages.push(imageInfo);
            }
            setFileList(fileImages)
            const useInfo = UserInfoStorage.get();
            if(useInfo)
            {
                setUser(useInfo);
            }
        }
    }, [])

    if (!diemDatQuangCao) {
        return <></>;
    }

    async function uploadImages() {
        let danhSachHinhAnh = []
        if (fileList) {
            var data = new FormData();
            fileList.forEach(file=> data.append('hinhAnhs', file.originFileObj || ''));
            await axios.post(`${process.env.REACT_APP_BASE_API}api/hinhanh/multip`,data,{
                headers: {'Content-Type': 'multipart/form-data'}
                }).then((response) => {
                console.log("response",response)
                if(response && response.status === 200)
                {
                    danhSachHinhAnh = response.data;
                }
            }).catch((e)=>{
                console.log(e)
            });
        }
        return danhSachHinhAnh
    }

    const uploadButton = (
        <div>
            <PlusOutlined />
            <div style={{ marginTop: 8 }}>Upload</div>
        </div>
    );

    const onChange = async (info) => {
        const file = info.file;
        if (file.status === 'error') {
            file.status = 'done';
            setFileList([...fileList || [],file]);
        } 
        setFileList(info.fileList);
    };

    const handlePreview = async (file) => {
        if (!file.url && !file.preview) {
            file.preview = await getBase64(file.originFileObj);
        }
        setSrcImage(file.url || file.preview);
        setIsVisible(true);
    };

    async function onSubmit(_model: CapNhatDiemDatQuangCaoModel) {
        setLoading(true)
        if (_model &&  diemDatQuangCao) {
            _model.danhSachHinhAnh = await uploadImages();
            _model.danhSachViTri = diemDatQuangCao.danhSachViTri;
            _model.quan = getDistrict(_model.quan).postcode;
            _model.phuong = getWardByDistrict(_model.quan,_model.phuong).postcode;
            diemDatQuangCaoAPI
            .CapNhat(diemDatQuangCao.id,_model)
            .then(() => {
                form.resetFields();
                onCancel()
            });
        }
            setLoading(false)
    }

    function getLoaiViTris() {
        setLoading(true)
        loaiViTriAPI
            .DanhSach()
            .then((response) => {
                if(response && response.status === 200)
                {
                    setLoaiViTris(response.data || []);
                }
            });
        setLoading(false)
    }

    
    function getHinhThucQuangCaos() {
        setLoading(true)
        hinhThucQuangCaoAPI
            .DanhSach()
            .then((response) => {
                if(response && response.status === 200)
                {
                    setHinhThucQuangCaos(response.data || []);
                }
            });
        setLoading(false)
    }

    return (
        <>
        <Modal
        confirmLoading={loading}
        getContainer={() => document.getElementById('modal-container') || document.body}
        title={"Cập nhật điểm đặt quảng cáo"}
        keyboard={false}
        maskClosable={false}
        destroyOnClose
        open
        forceRender
        onCancel={()=>{
            onCancel();
            form.resetFields();
        }}
        width={1000}
        footer={[
            <Button key='back' onClick={()=>{
            onCancel()
            form.resetFields();
            }}>
            Đóng
            </Button>,
            <Button
            key='submit'
            type='primary'
            loading={loading}
            onClick={(e) => {
                e.preventDefault();
                form.submit();
            }}
            >
            Lưu
            </Button>,
        ]}
        >
        <Form
            form={form}
            initialValues={diemDatQuangCao}
            layout='vertical'
            onFinish={onSubmit}
        >
            <Row gutter={[50,50]}>
                <Col span={12}>
                    <Form.Item label={"Địa chỉ"} name='diaChi'>
                            <Input/>
                    </Form.Item>
                    <Form.Item label={"Phường"} name='phuong'>
                        <Input readOnly/>
                    </Form.Item>
                    <Form.Item label={"Quận"} name='quan'>
                        <Input readOnly/>
                    </Form.Item>
                    <Form.Item label={"Loại vị trí"} name={'idLoaiViTri'}>
                        <Select placeholder="Vui lòng chọn loại vị trí" >
                            {loaiViTris && loaiViTris.map((option) => (
                                <Select.Option key={option.id} value={option.id}>{option.ma} - {option.ten}</Select.Option>
                            ))}
                        </Select>
                    </Form.Item>
                    <Form.Item label={"Hình thức quảng cáo"} name={'idHinhThucQuangCao'}>
                        <Select placeholder="Vui lòng chọn hình thức quảng cáo" >
                            {hinhThucQuangCaos && hinhThucQuangCaos.map((option) => (
                                <Select.Option key={option.id} value={option.id}>{option.ma} - {option.ten}</Select.Option>
                            ))}
                        </Select>
                    </Form.Item>
                    <Form.Item label={"Tình trạng"} name={'idTinhTrang'}>
                        <Select placeholder="Vui lòng chọn tình trạng" >
                            {tinhTrangDiemDatQuangCao && tinhTrangDiemDatQuangCao.map((option) => (
                                <Select.Option key={option.ma} value={option.ma}>{option.ten}</Select.Option>
                            ))}
                        </Select>
                    </Form.Item>
                </Col>
                <Col span={12}>
                    <Space direction='vertical' style={{marginTop:'20px',width:'100%'}}>
                            <Card
                                className='card-avatar'
                                bordered={false}
                                title={
                                <Space size={15} align='center'>
                                    <img src={imageIcon} alt='information' />
                                    <b>Danh sách hình ảnh</b>
                                </Space>
                                }
                            >
                                <Image.PreviewGroup>
                                    <Space direction='vertical' size={0}>
                                    <Upload
                                        listType="picture-card"
                                        fileList={fileList}
                                        onChange={onChange}
                                        onPreview={handlePreview}
                                    >
                                        {fileList && fileList.length < 2 && uploadButton}
                                    </Upload>
                                    </Space>
                                </Image.PreviewGroup>
                            </Card>
                        </Space>
                    {/* <Space direction='vertical' style={{width:'100%'}}>
                        <div id="map" ref={mapContainerRef}  />
                    </Space> */}
                </Col>
            </Row>
        </Form>
        </Modal>
        <Image
        width={200}
        style={{ display: 'none' }}
        preview={{
            visible: isVisible,
            src: srcImage,
            onVisibleChange: (value) => {
                setIsVisible(value);
                setSrcImage('');
            },
        }}
    />
    </>
    );
}
