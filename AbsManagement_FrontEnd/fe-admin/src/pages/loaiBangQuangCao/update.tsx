import { Button, Col, Form, Input, Modal } from 'antd';
import React from 'react';
import { useState } from 'react';
import { CapNhatLoaiBangQuangCaoModel, LoaiBangQuangCaoModel } from '../../apis/loaiBangQuangCao/model';
import { loaiBangQuangCaoAPI } from '../../apis/loaiBangQuangCao';

interface Props {
    loaiBangQuangCao?: LoaiBangQuangCaoModel;
    onCancel:()=>void;
}
export function ModalUpdateLoaiBangQuangCao(props: Props): JSX.Element {
  const { loaiBangQuangCao, onCancel } = props;
  const [form] = Form.useForm<CapNhatLoaiBangQuangCaoModel>();
  const [loading,setLoading] = useState(false);

  if (!loaiBangQuangCao) {
    return <></>;
  }

  function onSubmit(_model: CapNhatLoaiBangQuangCaoModel) {
    if(loaiBangQuangCao)
    {
      loaiBangQuangCaoAPI
      .CapNhat(loaiBangQuangCao.id,_model)
      .then(() => {
        form.resetFields();
      })
      .finally(() => setLoading(false));
    }
  }

  return (
    <Modal
      getContainer={() => document.getElementById('modal-container') || document.body}
      title={"Cập nhật loại bảng quảng cáo"}
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
        initialValues={loaiBangQuangCao}
        layout='vertical'
        onFinish={onSubmit}
      >
        <Col>
          <Form.Item label={"Mã loại bảng quảng cáo"} name={"ma"}>
            <Input className='input-code' />
          </Form.Item>
          <Form.Item label={"Tên loại bảng quảng cáo"} name={"ten"}>
              <Input/>
          </Form.Item>
      </Col>
      </Form>
    </Modal>
  );
}
