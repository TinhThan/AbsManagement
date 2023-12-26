import { Button, Col, DatePicker, Form, Input, Modal, Radio, Row, Select, Tooltip } from 'antd';
import React from 'react';
import { CapNhatCanBoModel } from '../../../apis/canBo/canBoModel';
import { canBoAPI } from '../../../apis/canBo/canBoAPI';
import { useState } from 'react';
import { Ward } from '../create';
import { getDistrictsAndWards } from '../../../utils/getWard';
import dataHCM from '../../../assets/new-dataHCM.json';

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
  const [wards,setWards] = useState<Ward[]>(getDistrictsAndWards(canBo?.noiCongTac[0] ?? ''));
  const [role,setRole] = useState(canBo?.role);

  if (!canBo) {
    return <></>;
  }

  function onSubmit(_model: CapNhatCanBoModel) {
    if(_model.role !== "CanBoSo")
    {
      _model.noiCongTac = [_model.quan,_model.phuong]
    }else {_model.noiCongTac = [] }
    setLoading(true)
    if(canBo)
    {
      canBoAPI
      .CapNhat(canBo.id,_model)
      .then(() => {
        form.resetFields();
        onCancel()
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
            <DatePicker />
          </Form.Item>
          <Form.Item label={"Quyền"} name={'role'}>
          <Radio.Group style={{ width: '100%' }} onChange={(value)=>setRole(value.target.value)}>
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
          {role && role !== roleCanBo[2].ma && 
          <>
            <Form.Item label={"Quận"} name={'quan'}>
              <Select placeholder="Vui lòng chọn quận" onChange={(value)=>{
                setWards(getDistrictsAndWards(value))
                form.setFieldValue('huyen',undefined)
              }}>
                  {dataHCM[0].districts.map((option) => (
                    <Select.Option key={option.postcode} value={option.postcode}>Quận {option.name}</Select.Option>
                  ))}
                </Select>
            </Form.Item>
            <Form.Item label={"Phường"} name={'phuong'}>
              <Select placeholder="Vui lòng chọn phường">
                  {wards.map((option) => (
                    <Select.Option key={option.postcode} value={option.postcode}>Phường {option.name}</Select.Option>
                  ))}
                </Select>
            </Form.Item>
          </>}
      </Col>
      </Form>
    </Modal>
  );
}
