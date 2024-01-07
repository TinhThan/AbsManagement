import { Button, Card, Col, DatePicker, Empty, Form, Input, Modal, Image, Row, Select, Space, TableColumnType, Table } from 'antd';
import React, { useEffect } from 'react';
import { ThemMoiCanBoModel } from '../../../apis/canBo/canBoModel';
import { canBoAPI } from '../../../apis/canBo/canBoAPI';
import { useState } from 'react';
import dataHCM from '../../../assets/new-dataHCM.json';
import imageIcon from "../../../assets/Image.svg";
import { getDistrictWithCode, getDistrictsAndWards, getWardByDistrictWithCode } from '../../../utils/getWard';
import { DiemDatQuangCaoModel } from '../../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import TextArea from 'antd/es/input/TextArea';
import { tinhTrangDiemDatQuangCao } from '../../../pages/diemDatQuangCao/list';
import { BangQuangCaoModel } from '../../../apis/bangQuangCao/bangQuangCaoModel';
import { bangQuangCaoAPI } from '../../../apis/bangQuangCao/bangQuangCaoAPI';

interface Props {
  onCancel:()=>void;
  diemDatQuangCao: DiemDatQuangCaoModel;
}
export interface Ward{
  name:string,
  postcode:string
}

export function ModalDeTailSpace(props: Props): JSX.Element {
  const { onCancel,diemDatQuangCao } = props;
  const [form] = Form.useForm<ThemMoiCanBoModel>();
  const [loading,setLoading] = useState(false);
  const [bangQuangCaos,setBangQuangCaos] = useState<BangQuangCaoModel[]>([]);

  
  async function getBangQuangCaos(id:number) {
    setLoading(true);
    try {
        const response = await bangQuangCaoAPI.DanhSachBySpace(id);
        const newData = response.data.map((item: any) => {
        return {
            ...item,
            key: item.id
        }
    })
        setBangQuangCaos(newData);
    } finally {
        setLoading(false);
    }
}

useEffect(()=>{
  getBangQuangCaos(diemDatQuangCao.id)
},[]);

  const columns: TableColumnType<BangQuangCaoModel>[] = [
    {
        title: "Loại bảng quảng cáo",
        width: 200,
        sorter: true,
        dataIndex: 'tenLoaiBangQuangCao',
    },
    {
        title: "Kích thước",
        width: 200,
        sorter: true,
        dataIndex: 'kichThuong',
    },
    {
        title: "Ngày bắt đầu",
        width: 200,
        sorter: true,
        dataIndex: 'ngayBatDau',
    },
    {
        title: "Ngày hết hạn",
        width: 200,
        sorter: true,
        dataIndex: 'ngayHetHan',
    },
    {
        title: "Tình trạng",
        width: 200,
        sorter: true,
        dataIndex: 'idTinhTrang',
    },
    {
        title: "Hành động",
        width: 80,
        key: 'function',
        fixed: 'right'
    }
];

  return (
    <Modal
      confirmLoading={loading}
      getContainer={() => document.getElementById('modal-container') || document.body}
      title={"Điểm đặt quảng cáo"}
      keyboard={false}
      maskClosable={false}
      destroyOnClose
      open
      forceRender
      onCancel={()=>{
        onCancel();
        form.resetFields();
      }}
      width={1000}
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
      <Form layout='vertical'>
            <Row gutter={[10,10]}>
                <Col span={14}>
                    <Card title={<b>Thông tin điểm đặt quảng cáo</b>} bordered={false}>
                        <Form.Item label={"Hình thức quảng cáo"}>
                            <Input value={diemDatQuangCao?.tenHinhThucQuangCao} readOnly/>
                        </Form.Item>
                        <Form.Item label={"Loại vị trí"}>
                            <Input value={diemDatQuangCao?.tenLoaiViTri} readOnly/>
                        </Form.Item>
                        <Form.Item label={"Danh sách hình ảnh"}>
                        {diemDatQuangCao && diemDatQuangCao.danhSachHinhAnh && diemDatQuangCao.danhSachHinhAnh.length > 0 ? 
                                <Image.PreviewGroup>
                                    <Space direction='vertical' size={0} style={{marginBottom:'10px'}}>
                                        {diemDatQuangCao.danhSachHinhAnh.length > 1 ? (
                                        <Space size={5}>
                                            {diemDatQuangCao.danhSachHinhAnh.map((t, index) => {
                                                return <Image key={index.toString()} width={120} height={120}
                                                        src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} />;
                                            })}
                                        </Space>
                                        ):<Image width={150} height={150}  src={`${process.env.REACT_APP_BASE_API}Upload/image/${diemDatQuangCao.danhSachHinhAnh[0]}`} alt='avatar' />}
                                    </Space>
                                </Image.PreviewGroup> :
                                <Empty/>}
                        </Form.Item>
                    </Card>
                </Col>
            <Col span={10}>
                    <Card title={<b>Thông tin địa điểm</b>} bordered={false}>
                            <Form.Item label={"Phường"}>
                            <Input value={"Phường " + getWardByDistrictWithCode(diemDatQuangCao?.quan || '',diemDatQuangCao?.phuong || '').name} readOnly/>
                        </Form.Item>
                        <Form.Item label={"Quận"}>
                            <Input value={"Quận " + getDistrictWithCode(diemDatQuangCao?.quan || '').name} readOnly />
                        </Form.Item>
                        <Form.Item label={"Địa chỉ"}>
                                <TextArea value={diemDatQuangCao?.diaChi} rows={2 } readOnly/>
                        </Form.Item>
                        <Form.Item label={"Tình trạng"}>
                            <Input value={tinhTrangDiemDatQuangCao[diemDatQuangCao?.idTinhTrang  || 'ChuaXuLy']} readOnly />
                        </Form.Item>
                        </Card>
            </Col>
        </Row>
        
        <Form.Item label='Danh sách bảng quảng cáo'>
          <Table columns={columns} dataSource={bangQuangCaos} scroll={{ x: 'max-content' }}/>
        </Form.Item>
      </Form>
    </Modal>
  );
}
