import { Button, Col, DatePicker, Form, Input, Modal, Radio, Row, Select } from 'antd';
import React from 'react';
import { ThemMoiCanBoModel } from '../../../apis/canBo/canBoModel';
import { canBoAPI } from '../../../apis/canBo/canBoAPI';
import { useState } from 'react';
import dataHCM from '../../../assets/new-dataHCM.json';
import { getDistrictsAndWards } from '../../../utils/getWard';
import { messageValidate, validateDatePicker } from '../../../utils/validator';

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

export interface Ward{
  name:string,
  postcode:string
}

export function ModalCreateCanBo(props: Props): JSX.Element {
  const { onCancel } = props;
  const [form] = Form.useForm<ThemMoiCanBoModel>();
  const [loading,setLoading] = useState(false);
  const [wards,setWards] = useState<Ward[]>([]);
  const [role,setRole] = useState(roleCanBo[0].ma);

  function onSubmit(_model: ThemMoiCanBoModel) {
    if(_model.role === roleCanBo[0].ma)
    {
      _model.noiCongTac = [_model.quan,_model.phuong]
    }    
    if(_model.role === roleCanBo[1].ma)
    {
      _model.noiCongTac = [_model.quan]
    }if(_model.role === roleCanBo[2].ma)
    {
      _model.noiCongTac = []
    }
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
          <Form.Item label={"Email"} name={"email"}
              rules={[{ required: true ,message: messageValidate.RequireEmail}, { type:'email',message: messageValidate.RequireEmailInvalid}]}>
            <Input className='input-code' />
          </Form.Item>
          <Form.Item label={"Họ và tên"} name={"hoTen"} rules={[{ required: true ,message: messageValidate.RequireName}]}>
              <Input/>
          </Form.Item>
          <Form.Item label={"Số điện thoại"} name={"soDienThoai"} rules={[{ required: true ,message: messageValidate.RequireSoDienThoai}]}>
            <Input/>
          </Form.Item>
          <Form.Item label={"Ngày sinh"} name={"ngaySinh"} rules={[{ required: true ,type: 'date',message: messageValidate.RequireNgaySinh}, {validator : validateDatePicker}]}>
            <DatePicker/>
          </Form.Item>
          <Form.Item label={"Quyền"} name={'role'} rules={[{ required: true ,message:  messageValidate.RequireQuyen}]}>
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
            <Form.Item label={"Quận"} name={'quan'} rules={[{ required: true ,message:  messageValidate.RequireQuan}]}>
              <Select placeholder="Vui lòng chọn quận" onChange={(value)=>{
                setWards(getDistrictsAndWards(value))
                form.setFieldValue('phuong',undefined)
              }}>
                  {dataHCM[0].districts.map((option) => (
                    <Select.Option key={option.postcode} value={option.postcode}>Quận {option.name}</Select.Option>
                  ))}
                </Select>
            </Form.Item>
            {
              role === roleCanBo[0].ma && <Form.Item label={"Phường"} name={'phuong'} rules={[{ required: true ,message:  messageValidate.RequirePhuong}]}>
              <Select placeholder="Vui lòng chọn phường">
                  {wards.map((option) => (
                    <Select.Option key={option.postcode} value={option.postcode}>Phường {option.name}</Select.Option>
                  ))}
                </Select>
            </Form.Item>
            }
          </>}
      </Col>
      </Form>
    </Modal>
  );
}
