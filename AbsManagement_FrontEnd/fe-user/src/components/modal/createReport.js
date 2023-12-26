import React, { useState, useRef, useEffect } from 'react'
import { Button, Card, Col, Row, Form, Input, Modal, Image, Space, Upload, Radio, Select } from 'antd';
import imageIcon from "../../assets/Image.svg";
import { Editor } from '@tinymce/tinymce-react';
import { PlusOutlined } from '@ant-design/icons';
import ReCAPTCHA from 'react-google-recaptcha'
import axios from 'axios'
import { Notification } from '../../utils/messagebox';

const getBase64 = (file) =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
});

export default function ModalCreateReport(props) {
    const { onCancel,lat,lng, diaChi, phuong, quan } = props;
    const [form] = Form.useForm();
    const editorRef = useRef(null)
    const [loading, setLoading] = useState(false);
    const [capVal, setCapVal] = useState(false);
    const [isVisible, setIsVisible] = useState(false);
    const [srcImage, setSrcImage] = useState('');
    const [fileList, setFileList] = useState([])
    const [hinhThucBaoCaos, setHinhThucBaoCaos] = useState([])

    useEffect(() => {
      getHinhThucBaoCaos()
      form.setFieldValue('diaChi',diaChi)
    }, [])
    

    async function onSubmit(_model) {
        setLoading(true)
        if (_model) {
            if (editorRef.current) {
                console.log("editorRef",editorRef.current.getContent());
            }
            _model.danhSachHinhAnh = await uploadImages();
            _model.danhSachViTri = [lng ?? 0,lat ?? 0];
            _model.phuong = phuong
            _model.quan = quan
            _model.noiDung = editorRef.current.getContent()
            console.log("model",_model)
            await axios.post(`${process.env.REACT_APP_BASE_API}baocaovipham/taomoi`,_model).then((response) => {
                console.log("response",response)
                if(response && response.status === 200)
                {
                    Notification.Success(response.data)
                }else{
                    Notification.Fail("Thêm báo cáo vi phạm thất bại.")
                }
                onCancel()
            }).catch((e)=>{
                console.log(e)
            });
        }
        setLoading(false)
    }

    async function uploadImages() {
        let danhSachHinhAnh = []
        if (fileList) {
            var data = new FormData();
            fileList.forEach(file=> data.append('hinhAnhs',file.originFileObj));
            await axios.post(`${process.env.REACT_APP_BASE_API}hinhanh/multip`,data,{
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

    async function getHinhThucBaoCaos() {
        await axios.get(`${process.env.REACT_APP_BASE_API}hinhthucbaocao`).then((response) => {
            if(response && response.status === 200)
            {
                setHinhThucBaoCaos(response.data)
            }
        }).catch((e)=>{
            console.log(e)
        });
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
            setFileList([...fileList,file]);
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
    return (
    <>
        <Modal
            getContainer={() => document.getElementById('modal-container') || document.body}
            title={"Thêm mới báo cáo vi phạm"}
            keyboard={false}
            maskClosable={false}
            destroyOnClose
            open
            forceRender
            onCancel={()=>{
                onCancel();
                form.resetFields();
            }}
            width={800}
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
                disabled={!capVal}
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
            layout='vertical'
            onFinish={onSubmit}
            >
            <Row gutter={[50,50]}>
                <Col span={12}>
                    <Form.Item label={"Họ Tên"} name={"hoten"}>
                        <Input/>
                    </Form.Item>
                    <Form.Item label={"Email"} name={"email"}>
                        <Input/>
                    </Form.Item>
                    <Form.Item label={"Số điện thoại"} name={"soDienThoai"}>
                        <Input/>
                    </Form.Item>
                    <Form.Item label={"Hình thức báo cáo"} name={'idHinhThucBaoCao'}>
                        <Select placeholder="Vui lòng chọn hình thức quảng cáo" >
                            {hinhThucBaoCaos && hinhThucBaoCaos.map((option) => (
                               <Select.Option key={option.id} value={option.id}>{option.ma} - {option.ten}</Select.Option>
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
                                    {fileList.length < 2 && uploadButton}
                                </Upload>
                                </Space>
                            </Image.PreviewGroup>
                        </Card>
                    </Space>
                </Col>
            </Row>
            <Form.Item label={"Địa chỉ"} name={"diaChi"}>
                <Input/>
            </Form.Item>
            <Editor
                onInit={(evt, editor) => editorRef.current = editor}
                initialValue="<p>Nhập nội dung tại đây</p>"
                init={{
                language:'vi_VN',
                height: 200,
                menubar: false,
                plugins: [
                    'advlist autolink lists link image charmap print preview anchor',
                    'searchreplace visualblocks code fullscreen',
                    'insertdatetime media table paste code help wordcount'
                ],
                toolbar: 'undo redo | formatselect | ' +
                'bold italic backcolor | alignleft aligncenter ' +
                'alignright alignjustify | bullist numlist outdent indent | ' +
                'removeformat | help',
                content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }'
                }}
            />
        </Form>
        <ReCAPTCHA style={{margin:'10px'}} sitekey={process.env.REACT_APP_SITEKEY} onChange={val=>setCapVal(val)}/>
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