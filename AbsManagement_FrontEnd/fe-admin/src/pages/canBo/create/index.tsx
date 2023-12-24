import { Button, Col, DatePicker, Form, Input, Modal, Radio, Row } from 'antd';
import React from 'react';
import { ThemMoiCanBoModel } from '../../../apis/canBo/canBoModel';
import { canBoAPI } from '../../../apis/canBo/canBoAPI';
import { useState } from 'react';

interface Props {
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
    ten:"Cán bộ sở"
  }
]

export function ModalCreateCanBo(props: Props): JSX.Element {
  const { onCancel } = props;
  const [form] = Form.useForm<ThemMoiCanBoModel>();
  const [loading,setLoading] = useState(false);

  function onSubmit(_model: ThemMoiCanBoModel) {
    _model.noiCongTac = []
    setLoading(true)
    canBoAPI
    .TaoMoi(_model)
    .then((response) => {
        if(response && response.status === 200)
        {
            form.resetFields();
            onCancel()
        }
    });
    setLoading(false)
  }

  return (
    <Modal
      confirmLoading={loading}
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
