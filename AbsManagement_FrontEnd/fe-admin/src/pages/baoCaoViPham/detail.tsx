import { PageLoading } from "@ant-design/pro-components";
import { Suspense, useEffect, useState } from "react";
import { baoCaoViPhamAPI } from "../../apis/baoCaoViPham";
import { BaoCaoViPhamModel } from "../../apis/baoCaoViPham/baoCaoViPhamModel";
import { Button, Card, Col, Form, Input, Row, Space, Image, Flex } from "antd";
import { getDistrict, getWardByDistrict } from "../../utils/getWard";
import { FileImageOutlined } from "@ant-design/icons";
import { useNavigate, useSearchParams } from "react-router-dom";

export default function DetailBaoCaoViPham(): JSX.Element {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const paramId = searchParams.get('id');
    const [loading, setLoading] = useState(false);
    const [baoCaoViPham, setBaoCaoViPham] = useState<BaoCaoViPhamModel>();
    const [images, setImages] = useState<string[]>([]);
    const [form] = Form.useForm();

    const onSubmited = () => {
        form.submit();
    }
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
                    setBaoCaoViPham(response.data[0]);
                    let fileImages: string[] = [];
                    for (const image of response?.data[0].danhSachHinhAnh || []) {
                        if (image) {
                            fileImages.push(`${process.env.REACT_APP_BASE_API}Upload/image/${image}`);
                        }
                    }
                    setImages(fileImages);
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
            <Space direction='vertical' size={0}>
                <Form
                    form={form}
                    className='form-layout'
                    onFinish={onFinish}
                >
                    <Row gutter={[20, 10]}>
                        <Col span={12}>
                            <Space direction='vertical' size={20}>
                                <Row gutter={[20, 20]}>
                                    <Col span={12}>
                                        <Card
                                            title={<b>Thông tin người báo cáo {baoCaoViPham?.hoTen}</b>}
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
                                        </Card>
                                    </Col>
                                    <Col span={12}>
                                        <Card title={<b>Thông tin địa điểm</b>} bordered={false} className='card-money'>
                                            <Form.Item label='Địa chỉ'>
                                                <Input.TextArea value={baoCaoViPham?.diaChi} readOnly rows={3} autoSize={{ minRows: 3, maxRows: 5 }} />
                                            </Form.Item>
                                            <Form.Item label='Phường'>
                                                <Input value={baoCaoViPham?.quan && baoCaoViPham?.phuong ? `Phường ${getWardByDistrict(baoCaoViPham?.quan || '', baoCaoViPham?.phuong || '')}` : ''} readOnly />
                                            </Form.Item>
                                            <Form.Item label='Quận'>
                                                <Input value={baoCaoViPham?.quan ? `Quận ${getDistrict(baoCaoViPham?.quan || '')}` : ''} readOnly />
                                            </Form.Item>
                                        </Card>
                                    </Col>
                                </Row>
                                {/* <Row>
                                <Col span={24}>
                                <Card title={<b>{t(nameof(() => vi.DinhTinh_DinhLuong))}</b>} bordered={false}>
                                    <Row gutter={[30, 10]}>
                                    <Col xs={24} sm={24} md={24} lg={24} xl={12} xxl={12}>
                                        <Form.Item label={t(nameof(() => vi.Material))} name={nameof<TypeProduct>((t) => t.tenNguyenLieu_MacDinh)}>
                                        <Input readOnly />
                                        </Form.Item>
                                        <Form.Item label={t(nameof(() => vi.Unit))} name={nameof<TypeProduct>((t) => t.tenDonViTinh)}>
                                        <Input readOnly />
                                        </Form.Item>
                                        <Form.Item label={t(nameof(() => vi.CurrencyUnitName))} name={nameof<TypeProduct>((t) => t.tenDonViTienTe)}>
                                        <Input readOnly />
                                        </Form.Item>
                                        <Form.Item label={t(nameof(() => vi.WeightUnit))} name={nameof<TypeProduct>((t) => t.tenDonViCan)}>
                                        <Input readOnly />
                                        </Form.Item>
                                    </Col>
                                    <Col xs={24} sm={24} md={24} lg={24} xl={12} xxl={12}>
                                        <Form.Item label={t(nameof(() => vi.KhoiLuongTong_MacDinh))} name={nameof<TypeProduct>((t) => t.khoiLuongTong_MacDinh)}>
                                        <Input readOnly />
                                        </Form.Item>
                                        <Form.Item label={t(nameof(() => vi.KhoiLuongDa_MacDinh))} name={nameof<TypeProduct>((t) => t.khoiLuongDa_MacDinh)}>
                                        <Input readOnly />
                                        </Form.Item>
                                        <Form.Item label={t(nameof(() => vi.KhoiLuongVang_MacDinh))} name={nameof<TypeProduct>((t) => t.khoiLuongVang_MacDinh)}>
                                        <Input readOnly />
                                        </Form.Item>
                                    </Col>
                                    </Row>
                                </Card>
                                </Col>
                            </Row> */}
                            </Space>
                        </Col>
                        <Col span={12}>
                            <Space direction='vertical'>
                                <Card
                                    bordered={false}
                                    title={
                                        <b>
                                            <FileImageOutlined /> {'Danh sách hình ảnh'}
                                        </b>
                                    }
                                >
                                    <Image.PreviewGroup>
                                        <Space direction='vertical' size={0}>
                                            <Image width='100%' height='100%' src={images[0]} alt='avatar' />
                                            {images.length > 1 && (
                                                <Space size={5}>
                                                    {images.map((t, index) => {
                                                        return <Image key={index.toString()} width='100%' height='100%' src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} alt={index.toString()} />;
                                                    })}
                                                </Space>
                                            )}
                                        </Space>
                                    </Image.PreviewGroup>
                                </Card>
                            </Space>
                        </Col>
                    </Row>

                </Form>
                <Flex gap="small" className="mt-9">
                    <Button className="mb-4" onClick={() => navigate(-1)}>Trở về danh sách báo cáo</Button>
                    <Button type="primary" onClick={() => onSubmited}>Cập nhật báo cáo</Button>
                </Flex>
            </Space>
        </Suspense>
    );
}