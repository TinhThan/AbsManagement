import React, { useState } from 'react'
import { ThemMoiBangQuangCaoModel } from '../../../apis/bangQuangCao/bangQuangCaoModel';
import { Button, Card, Col, DatePicker, Form, Input, Modal, Image, Space, Upload, UploadFile } from 'antd';
import { bangQuangCaoAPI } from '../../../apis/bangQuangCao/bangQuangCaoAPI';
import imageIcon from "../../../assets/Image.svg";
import { PlusOutlined } from '@ant-design/icons';

interface Props {
    onCancel:()=>void;
}

export default function ModalCreateBangQuangCao(props: Props): JSX.Element {
    const { onCancel } = props;
    const [form] = Form.useForm<ThemMoiBangQuangCaoModel>();
    const [loading,setLoading] = useState(false);

    const [previewOpen, setPreviewOpen] = useState(false);
    const [previewImage, setPreviewImage] = useState('');
    const [previewTitle, setPreviewTitle] = useState('');
    const [fileList, setFileList] = useState<UploadFile[]>([
        {
        uid: '-1',
        name: 'image.png',
        status: 'done',
        url: 'https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png',
        }
    ])

    function onSubmit(_model: ThemMoiBangQuangCaoModel) {
        bangQuangCaoAPI
        .TaoMoi(_model)
        .then((response) => {
            if(response && response.status === 200)
            {
                form.resetFields();
                onCancel()
            }
        })
        .finally(() => setLoading(false));
    }

    const uploadButton = (
        <div>
          <PlusOutlined />
          <div style={{ marginTop: 8 }}>Upload</div>
        </div>
      );

    return (
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
            layout='vertical'
            onFinish={onSubmit}
            >
            <Col>
                <Form.Item label={"Email"} name={"email"}>
                    <Input className='input-code' />
                </Form.Item>
                <Form.Item label={"Họ và tên"} name={"hoTen"}>
                    <Input/>
                </Form.Item>
                <Form.Item label={"Số điện thoại"} name={"soDienThoai"}>
                    <Input/>
                </Form.Item>
                <Form.Item label={"Ngày sinh"} name={"ngaySinh"}>
                    <DatePicker/>
                </Form.Item>
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
                                fileList={fileList}
                                listType='picture-card'
                                onPreview={(file) => {
                                window.open(file.url, file.fileName);
                                }}
                            >
                                {fileList.length < 3 && uploadButton}
                            </Upload>
                        </Space>
                        </Image.PreviewGroup>
                    </Card>
                </Space>
            </Col>
            </Form>
        </Modal>
    );
}

