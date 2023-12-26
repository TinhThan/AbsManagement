import { Button, Col, Form, Input, Modal } from 'antd';
import React from 'react';
import { useState } from 'react';
import { CapNhatHinhThucQuangCaoModel, HinhThucQuangCaoModel } from '../../apis/hinhThucQuangCao/model';
import { hinhThucQuangCaoAPI } from '../../apis/hinhThucQuangCao';

interface Props {
    hinhThucQuangCao?: HinhThucQuangCaoModel;
    onCancel:()=>void;
}
export function ModalUpdateHinhThucQuangCao(props: Props): JSX.Element {
  const { hinhThucQuangCao, onCancel } = props;
  const [form] = Form.useForm<CapNhatHinhThucQuangCaoModel>();
  const [loading,setLoading] = useState(false);

  if (!hinhThucQuangCao) {
    return <></>;
  }

  function onSubmit(_model: CapNhatHinhThucQuangCaoModel) {
    if(hinhThucQuangCao)
    {
      hinhThucQuangCaoAPI
      .CapNhat(hinhThucQuangCao.id,_model)
      .then(() => {
        form.resetFields();
      })
      .finally(() => setLoading(false));
    }
  }

  return (
    <Modal
      getContainer={() => document.getElementById('modal-container') || document.body}
      title={"Cập nhật hình thức quảng cáo"}
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
        initialValues={hinhThucQuangCao}
        layout='vertical'
        onFinish={onSubmit}
      >
        <Col>
          <Form.Item label={"Mã hình thức quảng cáo"} name={"ma"}>
            <Input className='input-code' />
          </Form.Item>
          <Form.Item label={"Tên hình thức quảng cáo"} name={"ten"}>
              <Input/>
          </Form.Item>
      </Col>
      </Form>
    </Modal>
  );
}
