import { Button, Col, Form, Input, Modal } from 'antd';
import React from 'react';
import { useState } from 'react';
import { hinhThucBaoCaoAPI } from '../../apis/hinhThucBaoCao';
import { ThemMoiHinhThucBaoCaoModel } from '../../apis/hinhThucBaoCao/model';

interface Props {
    onCancel:()=>void;
}

export function ModalCreateHinhThucBaoCao(props: Props): JSX.Element {
    const { onCancel } = props;
    const [form] = Form.useForm<ThemMoiHinhThucBaoCaoModel>();
    const [loading,setLoading] = useState(false);

    function onSubmit(_model: ThemMoiHinhThucBaoCaoModel) {
        hinhThucBaoCaoAPI
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
        title={"Thêm mới hình thức báo cáo"}
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
                <Form.Item label={"Mã hình thức báo cáo"} rules={[{ required: true, message: 'Vui lòng nhập mã!' }]} name={"ma"}>
                    <Input className='input-code' />
                </Form.Item>
                <Form.Item label={"Tên hình thức báo cáo"} rules={[{ required: true, message: 'Vui lòng nhập tên!' }]} name={"ten"}>
                    <Input/>
                </Form.Item>
        </Col>
        </Form>
        </Modal>
    );
}
