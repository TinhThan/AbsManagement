import { PageContainer, PageLoading } from "@ant-design/pro-components";
import { Suspense, useEffect, useState } from "react";
import { baoCaoViPhamAPI } from "../../apis/baoCaoViPham";
import { BaoCaoViPhamModel } from "../../apis/baoCaoViPham/baoCaoViPhamModel";
import { Button, Card, Col, Form, Input, Row, Space, Image, Flex, Spin, Empty } from "antd";
import { getDistrictWithCode, getWardByDistrictWithCode } from "../../utils/getWard";
import imageIcon from "../../assets/Image.svg";
import { useNavigate, useSearchParams } from "react-router-dom";

export default function DetailBaoCaoViPham(): JSX.Element {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const paramId = searchParams.get('id');
    const [loading, setLoading] = useState(false);
    const [baoCaoViPham, setBaoCaoViPham] = useState<BaoCaoViPhamModel>();
    const [form] = Form.useForm();

    const onFinish = (value: any) => {
        console.log(value);
    }

    async function getDetail(id: string) {
        setLoading(true);
        await baoCaoViPhamAPI
            .ChiTiet(parseInt(id))
            .then((response) => {
                if (response && response.status === 200) {
                    setLoading(false);
                    setBaoCaoViPham(response.data);
                }
            })
            .catch(() => {
                setLoading(false);
            });
    }

    useEffect(() => {
        if (paramId) getDetail(paramId);
    }, [paramId])

    return (
        <Suspense fallback={<PageLoading />}>
            <PageContainer title="Chi tiết điểm đặt quảng cáo">
                <Spin spinning={loading}>
                <Space className='space-button-on-top'>
                    <Button danger onClick={()=> navigate(-1)}><b>Thoát</b></Button>
                </Space>
                <Form
                    form={form}
                    layout="vertical"
                    onFinish={onFinish}
                >
                    <Row gutter={[10,10]}>
                        <Col span={14}>
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
                                <Form.Item label='Địa chỉ'>
                                    <Input.TextArea value={baoCaoViPham?.diaChi} readOnly rows={3} autoSize={{ minRows: 3, maxRows: 5 }}/>
                                </Form.Item>
                                <Form.Item label='Phường'>
                                    <Input value={`Phường ${getWardByDistrictWithCode(baoCaoViPham?.quan || '',baoCaoViPham?.phuong || '').name}`} readOnly />
                                </Form.Item>
                                <Form.Item label='Quận'>
                                    <Input value={`Quận ${getDistrictWithCode(baoCaoViPham?.quan || '').name}`} readOnly />
                                </Form.Item>
                            </Card>
                        </Col>
                        <Col span={10}>
                            <Space direction='vertical' style={{width:'100%', marginBottom:'10px'}}>
                                <Card
                                    className='card-avatar'
                                    bordered={false}
                                    title={
                                    <Space size={15} align='center'>
                                        <img src={imageIcon} alt='information' />
                                        <b>Danh sách hình ảnh báo cáo</b>
                                    </Space>
                                    }
                                >
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
                                </Card>
                            </Space>
                            <Space style={{width:'100%'}}>
                                <Card title={<b>Thông tin địa điểm</b>} bordered={false}>
                                    <div>
                                        {/* <div id="map" ref={mapContainerRef} className='map-container-space'/> */}
                                    </div>
                                </Card>
                            </Space>
                        </Col>
                    </Row>
                </Form>
            </Spin>
            </PageContainer>
        </Suspense>
    );
}