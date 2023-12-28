import { Button, Col, DatePicker, Form, Input, Modal, Radio, Row, Select, Tooltip } from 'antd';
import React, { useEffect, useState } from 'react';
import { CapNhatDiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import { diemDatQuangCaoAPI } from '../../apis/diemDatQuangCao';
import { LoaiViTriModel } from '../../apis/loaiViTri/model';
import { HinhThucQuangCaoModel } from '../../apis/hinhThucQuangCao/model';
import { loaiViTriAPI } from '../../apis/loaiViTri';
import { hinhThucBaoCaoAPI } from '../../apis/hinhThucBaoCao';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';

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

export function ModalUpdateDiemDatQuangCao(props: Props): JSX.Element {
    const { diemDatQuangCao, onCancel } = props;
    const [form] = Form.useForm<CapNhatDiemDatQuangCaoModel>();
    const [loading, setLoading] = useState(false);
    const [user,setUser] = useState<UserStorage>();
    const [loaiViTris,setLoaiViTris] = useState<LoaiViTriModel[]>([]);
    const [hinhThucQuangCaos,setHinhThucQuangCaos] = useState<HinhThucQuangCaoModel[]>([]);

    
    useEffect(() => {
        getLoaiViTris();
        getHinhThucQuangCaos();
        const useInfo = UserInfoStorage.get();
        if(useInfo)
        {
            setUser(useInfo);
        }
    }, [])

    if (!diemDatQuangCao) {
        return <></>;
    }

    function onSubmit(_model: CapNhatDiemDatQuangCaoModel) {
        setLoading(true);
        if(diemDatQuangCao) {
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
        hinhThucBaoCaoAPI
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
        width={400}
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
                {/* <Col span={12}>
                    <Form.Item label={"Địa chỉ"} name='diaChi'>
                        <Tooltip placement='top' style={{ width: '100%' }} title={diemDatQuangCao.diaChi}>
                            <Input style={{ textOverflow: 'ellipsis' }} />
                        </Tooltip>
                    </Form.Item>
                    <Form.Item label={"Phường"} name='phuong'>
                        <Input value={"Phường " + getWardByDistrict(diemDatQuangCao.quan,diemDatQuangCao.phuong).name} readOnly/>
                    </Form.Item>
                    <Form.Item label={"Quận"}>
                        <Input value={"Quận " + getDistrict(diemDatQuangCao.quan).name} readOnly />
                    </Form.Item>
                    <Form.Item label={"Loại vị trí"}>
                        <Input value={diemDatQuangCao.tenLoaiViTri} readOnly/>
                    </Form.Item>
                    <Form.Item label={"Hình thức quảng cáo"}>
                        <Input value={diemDatQuangCao.tenHinhThucQuangCao} readOnly/>
                    </Form.Item>
                    <Form.Item label={"Tình trạng"}>
                        <Input value={tinhTrangDiemDatQuangCao[diemDatQuangCao.idTinhTrang]} readOnly />
                    </Form.Item>
                </Col>
                <Col span={12}>
                <Space direction='vertical' style={{width:'100%'}}>
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
                            {diemDatQuangCao.danhSachHinhAnh && diemDatQuangCao.danhSachHinhAnh.length > 0 && 
                                <Image.PreviewGroup>
                                    <Space direction='vertical' size={0} style={{marginBottom:'10px'}}>
                                        {diemDatQuangCao.danhSachHinhAnh.length > 1 ? (
                                        <Space size={5}>
                                            {diemDatQuangCao.danhSachHinhAnh.map((t, index) => {
                                                return <Image key={index.toString()} width={120} height={120} src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} alt={index.toString()} />;
                                            })}
                                        </Space>
                                        ):<Image width={150} height={150}  src={`${process.env.REACT_APP_BASE_API}Upload/image/${diemDatQuangCao.danhSachHinhAnh[0]}`} alt='avatar' />}
                                    </Space>
                                </Image.PreviewGroup> }
                        </Card>
                    </Space>
                    <Space direction='vertical' style={{width:'100%'}}>
                        <div id="map" ref={mapContainerRef}  />
                    </Space>
                </Col> */}
            </Row>
        </Form>
        </Modal>
    );
}
