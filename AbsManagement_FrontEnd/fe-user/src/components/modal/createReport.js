import React, { useState, useRef } from 'react'
import { Button, Card, Col, DatePicker, Form, Input, Modal, Image, Space, Upload } from 'antd';
import imageIcon from "../../assets/Image.svg";
import { Editor } from '@tinymce/tinymce-react';
import { PlusOutlined } from '@ant-design/icons';
import ReCAPTCHA from 'react-google-recaptcha'
import axios from 'axios'

const getBase64 = (file) =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
  });

export default function ModalCreateReport(props) {
    const { onCancel } = props;
    const [form] = Form.useForm();
    const editorRef = useRef(null)
    const [loading, setLoading] = useState(false);
    const [capVal, setCapVal] = useState(false);
    const [isVisible, setIsVisible] = useState(false);
    const [srcImage, setSrcImage] = useState('');
    const [fileList, setFileList] = useState([])

    function onSubmit(_model) {
        if (_model) {

            if (editorRef.current) {
                console.log("editorRef",editorRef.current.getContent());
            }

            console.log({
                ..._model
            })
        }
    }

    async function onModalOk(file) {
        if (window.File && window.FileReader && window.FileList && window.Blob) {
            if (file) {
                setFileList([...fileList,file])
                var data = new FormData();
                data.append('hinhAnhs', file);
                // await axios.post('https://localhost:7286/api/hinhanh',data).then((response) => {
                //     if(response && response.status === 200)
                //     {
                //         setImages([...images, response.data]);
                //         setFileList([{
                //             uid: response.data,
                //             name: response.data,
                //             status: 'done',
                //             url: process.env.REACT_APP_URL_IMAGE +  response.data,
                //         },...fileList])
                //         console.log("setFileList",fileList)
                //     }
                //     console.log("response",response)
                // });
            }
        }
    }   

    const uploadButton = (
        <div>
            <PlusOutlined />
            <div style={{ marginTop: 8 }}>Upload</div>
        </div>
    );

    const onChange = (info) => {
        const file = info.file;
        console.log("update file",file)
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
            title={"Thêm mới cán bộ"}
            keyboard={false}
            maskClosable={false}
            destroyOnClose
            open
            forceRender
            onCancel={()=>{
            onCancel();
            form.resetFields();
            }}
            width={900}
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
            <Col>
                <Form.Item label={"Họ Tên"} name={"hoten"}>
                    <Input/>
                </Form.Item>
                <Form.Item label={"Email"} name={"email"}>
                    <Input/>
                </Form.Item>
                <Form.Item label={"Số điện thoại"} name={"soDienThoai"}>
                    <Input/>
                </Form.Item>
                <Editor
                    onInit={(evt, editor) => editorRef.current = editor}
                    initialValue="<p>This is the initial content of the editor.</p>"
                    init={{
                    language:'vi_VN',
                    height: 300,
                    width:800,
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
                <Space direction='vertical' size={10}>
                    <Card
                        className='card-avatar'
                        bordered={false}
                        title={
                        <Space size={5} align='center'>
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
                                {fileList.length < 5 && uploadButton}
                            </Upload>
                            </Space>
                        </Image.PreviewGroup>
                    </Card>
                </Space>
            </Col>
            </Form>
            <ReCAPTCHA sitekey='6LeydTkpAAAAAJzoOBspUKjupQq7FDZi2-ByYGX4' onChange={val=>setCapVal(val)}/>
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