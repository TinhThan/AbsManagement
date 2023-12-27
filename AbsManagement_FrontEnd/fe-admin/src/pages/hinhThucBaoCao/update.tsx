import { Button, Col, Form, Input, Modal } from 'antd';
import React from 'react';
import { useState } from 'react';
import { CapNhatHinhThucBaoCaoModel, HinhThucBaoCaoModel } from '../../apis/hinhThucBaoCao/model';
import { hinhThucBaoCaoAPI } from '../../apis/hinhThucBaoCao';

interface Props {
    HinhThucBaoCao?: HinhThucBaoCaoModel;
    onCancel:()=>void;
}
export function ModalUpdateHinhThucBaoCao(props: Props): JSX.Element {
  const { HinhThucBaoCao, onCancel } = props;
  const [form] = Form.useForm<CapNhatHinhThucBaoCaoModel>();
  const [loading,setLoading] = useState(false);

  if (!HinhThucBaoCao) {
    return <></>;
  }

  function onSubmit(_model: CapNhatHinhThucBaoCaoModel) {
    if(HinhThucBaoCao)
    {
      hinhThucBaoCaoAPI
      .CapNhat(HinhThucBaoCao.id,_model)
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
      title={"Cập nhật hình thức báo cáo"}
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
        initialValues={HinhThucBaoCao}
        layout='vertical'
        onFinish={onSubmit}
      >
        <Col>
          <Form.Item label={"Mã hình thức báo cáo"} rules={[{ required: true, message: 'Vui lòng nhập mã!' }]}  name={"ma"}>
            <Input className='input-code' />
          </Form.Item>
          <Form.Item label={"Tên hình thức báo cáo"} rules={[{ required: true, message: 'Vui lòng nhập tên!' }]}  name={"ten"}>
              <Input/>
          </Form.Item>
      </Col>
      </Form>
    </Modal>
  );
}
