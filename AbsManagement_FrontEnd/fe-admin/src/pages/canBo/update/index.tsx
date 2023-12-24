import { Button, Col, DatePicker, Form, Input, Modal, Radio, Row, Tooltip } from 'antd';
import React from 'react';
import { CapNhatCanBoModel } from '../../../apis/canBo/canBoModel';
import { canBoAPI } from '../../../apis/canBo/canBoAPI';
import { useState } from 'react';

interface Props {
  canBo?: CapNhatCanBoModel;
  onCancel:()=>void;
}

const roleCanBo = [
  {
    ma:"CanBoPhuong",
    ten:"Cán bộ phường",
  },
  {
    ma:"CanBoQuan",
    ten:"Cán bộ quận"
  },
  {
    ma:"CanBoSo",
    ten:"Cán bộ Sở"
  }
]

export function ModalUpdateCanBo(props: Props): JSX.Element {
  const { canBo, onCancel } = props;
  const [form] = Form.useForm<CapNhatCanBoModel>();
  const [loading, setLoading] = useState(false);

  if (!canBo) {
    return <></>;
  }

  function onSubmit(_model: CapNhatCanBoModel) {
    _model.noiCongTac = []
    setLoading(true)
    if(canBo)
    {
      canBoAPI
      .CapNhat(canBo.id,_model)
      .then(() => {
        form.resetFields();
      });
    }
    setLoading(false)
  }

  return (
    <Modal
      confirmLoading={loading}
      getContainer={() => document.getElementById('modal-container') || document.body}
      title={"Cập nhật cán bộ"}
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
        initialValues={canBo}
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
          <Form.Item label={"Quyền"} name={'role'}>
            <Radio.Group style={{ width: '100%' }}>
              <Row gutter={5}>
                {roleCanBo.map((option) => (
                  <Col xs={12} sm={12} md={12} lg={10} xl={10} xxl={10} key={option.ma}>
                    <Radio key={option.ma} value={option.ma}>
                      {option.ten}
                    </Radio>
                  </Col>
                ))}
              </Row>
            </Radio.Group>
          </Form.Item>
      </Col>
      </Form>
    </Modal>
  );
}
