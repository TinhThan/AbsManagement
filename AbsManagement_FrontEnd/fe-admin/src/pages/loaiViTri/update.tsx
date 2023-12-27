import { Button, Col, DatePicker, Form, Input, Modal, Radio, Row } from 'antd';
import React from 'react';
import { CapNhatLoaiViTriModel, LoaiViTriModel } from '../../apis/loaiViTri/model';
import { useState } from 'react';
import { loaiViTriAPI } from '../../apis/loaiViTri';

interface Props {
    loaiViTri?: LoaiViTriModel;
    onCancel:()=>void;
}
export function ModalUpdateLoaiViTri(props: Props): JSX.Element {
  const { loaiViTri, onCancel } = props;
  const [form] = Form.useForm<CapNhatLoaiViTriModel>();
  const [loading,setLoading] = useState(false);

  if (!loaiViTri) {
    return <></>;
  }

  function onSubmit(_model: CapNhatLoaiViTriModel) {
    if(loaiViTri)
    {
      loaiViTriAPI
      .CapNhat(loaiViTri.id,_model)
      .then(() => {
        form.resetFields();
        onCancel();
      })
      .finally(() => setLoading(false));
    }
  }

  return (
    <Modal
      getContainer={() => document.getElementById('modal-container') || document.body}
      title={"Cập nhật loại vị trí"}
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
        initialValues={loaiViTri}
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
