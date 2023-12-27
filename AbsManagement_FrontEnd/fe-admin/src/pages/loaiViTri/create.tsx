import { Button, Col, Form, Input, Modal } from 'antd';
import React from 'react';
import { useState } from 'react';
import { loaiViTriAPI } from '../../apis/loaiViTri';
import { ThemMoiLoaiViTriModel } from '../../apis/loaiViTri/model';

interface Props {
    onCancel:()=>void;
}

export function ModalCreateLoaiViTri(props: Props): JSX.Element {
    const { onCancel } = props;
    const [form] = Form.useForm<ThemMoiLoaiViTriModel>();
    const [loading,setLoading] = useState(false);

    function onSubmit(_model: ThemMoiLoaiViTriModel) {
        loaiViTriAPI
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

    return (
        <Modal
        getContainer={() => document.getElementById('modal-container') || document.body}
        title={"Thêm mới loại vị trí"}
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
            <Form.Item label={"Mã loại vị trí"} rules={[{ required: true, message: 'Vui lòng nhập mã!' }]}  name={"ma"}>
                <Input className='input-code' />
            </Form.Item>
            <Form.Item label={"Tên loại vị trí"} rules={[{ required: true, message: 'Vui lòng nhập tên!' }]}  name={"ten"}>
                <Input/>
            </Form.Item>
        </Col>
        </Form>
        </Modal>
    );
}
