import React, { useState, useRef, useEffect } from 'react'
import { Button, Card, Col, Row, Form, Input, Modal, Image, Space, Empty } from 'antd';
import TextArea from 'antd/es/input/TextArea';
import { tinhTrangReports } from '../map';
import parse from 'html-react-parser';


export default function ModalDetailReport(props) {
    const { onCancel, baoCaoViPham } = props;
    return (
    <>
        <Modal
            getContainer={() => document.getElementById('modal-container') || document.body}
            title={(<>Chi tiết báo cáo vi phạm 
            <p>{tinhTrangReports[baoCaoViPham.idTinhTrang]}</p></>)}
            keyboard={false}
            maskClosable={false}
            destroyOnClose
            open
            forceRender
            onCancel={()=>{
                onCancel();
            }}
            width={800}
            footer={[
            <Button key='back' onClick={()=>{
                onCancel()
            }}>
                Đóng
            </Button>,
            ]}
        >
            <Form
                    layout="vertical"
                >
                    <Row gutter={[5,5]}>
                        <Col span={12}>
                            <Space direction='vertical' style={{width:'100%'}}>
                                <Card
                                    title={<b>Thông tin người báo cáo</b>}
                                    bordered={false}
                                >
                                    <Form.Item label='Họ tên'>
                                        <Input value={baoCaoViPham?.hoTen} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Email'>
                                        <Input value={baoCaoViPham?.email} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Số điện thoại'>
                                        <Input value={baoCaoViPham?.soDienThoai} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Nội dung vi phạm'>
                                        <div>
                                            {parse(baoCaoViPham?.noiDung)}
                                        </div>
                                    </Form.Item>
                                    <Form.Item label="Danh sách hình ảnh">
                                        {baoCaoViPham && baoCaoViPham.danhSachHinhAnh && baoCaoViPham.danhSachHinhAnh.length > 0 ? 
                                            <Image.PreviewGroup>
                                                <Space direction='vertical' size={0} style={{marginBottom:'10px'}}>
                                                    {baoCaoViPham.danhSachHinhAnh.length > 1 ? (
                                                    <Space size={5}>
                                                        {baoCaoViPham.danhSachHinhAnh.map((t, index) => {
                                                            return <Image key={index.toString()} width={120} height={120}
                                                                    src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} />;
                                                        })}
                                                    </Space>
                                                    ):<Image width={150} height={150}  src={`${process.env.REACT_APP_BASE_API}Upload/image/${baoCaoViPham.danhSachHinhAnh[0]}`} alt='avatar' />}
                                                </Space>
                                            </Image.PreviewGroup> :
                                            <Empty/>}
                                    </Form.Item>
                                </Card>
                            </Space>
                        </Col>
                        <Col span={12}>
                            <Space direction='vertical' style={{width:'100%'}}>
                                <Card
                                    title={<b>Thông tin cán bộ xử lý</b>}
                                    bordered={false}
                                >
                                    {baoCaoViPham.idCanBoXuLy && baoCaoViPham.idCanBoXuLy > 0 ? <>
                                    <Form.Item label='Họ tên'>
                                        <Input value={baoCaoViPham?.hoTenCanBoXuLy} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Email'>
                                        <Input value={baoCaoViPham?.emailCanBoXuLy} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Số điện thoại'>
                                        <Input value={baoCaoViPham?.soDienThoaiCanBoXuLy} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Nội dung xử lý'>
                                        <div>
                                            {parse(baoCaoViPham.noiDungXuLy)}
                                        </div>
                                    </Form.Item></>:
                                    <Empty/>}
                                </Card>
                            </Space>
                        </Col>
                    </Row>
                </Form>
    </Modal>
    </>
    );
}