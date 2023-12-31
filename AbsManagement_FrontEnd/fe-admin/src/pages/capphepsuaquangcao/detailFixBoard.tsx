import { PageContainer, PageLoading } from "@ant-design/pro-components";
import { FC, Suspense, useEffect, useState } from "react"
import { Button, Card, Col, Form, Input, Row, Space, Image, Spin, Empty } from "antd";
import imageIcon from "../../assets/Image.svg";
import { useNavigate, useSearchParams } from "react-router-dom";
import { phieuChinhSuaAPI } from "../../apis/phieuChinhSua";
import moment from 'moment';
import mapboxgl from "mapbox-gl";

const tinhTrangDiemDatQuangCao = {
    DaQuyHoach: "Đã quy hoạch",
    ChuaQuyHoach: "Chưa quy hoạch"
}

const DetailFixBoard : FC = () => {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const paramId = searchParams.get('id');
    const [loading, setLoading] = useState(false);
    const [data, setData] = useState<any>();
    const [form] = Form.useForm();

    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY || '';

    async function getDetail(id: string) {
        setLoading(true);
        await phieuChinhSuaAPI
            .ChiTietPhieuSuaBangQuangCao(parseInt(id))
            .then((response) => {
                if (response && response.status === 200) {
                    setLoading(false);
                    setData(response.data);
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
            <PageContainer title="Chi tiết bảng quảng cáo">
                <Spin spinning={loading}>
                    <Space className='space-button-on-top'>
                        <Button danger onClick={() => navigate(-1)}><b>Thoát</b></Button>
                    </Space>
                    <Form
                        form={form}
                        layout="vertical"
                    >
                        <Row gutter={[10, 10]}>
                            <Col span={14}>
                                <Card
                                    title={<b>Thông tin người gửi</b>}
                                    bordered={false}
                                >
                                    <Form.Item label='Họ tên'>
                                        <Input value={data?.tenCanBoGui} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Email'>
                                        <Input value={data?.emailCanBoGui} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Ngày gửi'>
                                        <Input value={data?.ngayGui ? moment(data?.ngayGui).format('DD-MM-YYYY HH:mm') : ''} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Địa chỉ'>
                                        <Input.TextArea value={data?.bangQuangCao.diaChiCongTy} readOnly rows={3} autoSize={{ minRows: 3, maxRows: 5 }} />
                                    </Form.Item>
                                    <Form.Item label='Kích thước'>
                                        <Input value={data?.bangQuangCao.kichThuoc} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Tình trạng'>
                                        <Input value={tinhTrangDiemDatQuangCao[data?.bangQuangCao.idTinhTrang]} readOnly />
                                    </Form.Item>
                                </Card>
                            </Col>
                            <Col span={10}>
                                <Space direction='vertical' style={{ width: '100%', marginBottom: '10px' }}>
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
                                        {data && data.bangQuangCao.danhSachHinhAnh && data.bangQuangCao.danhSachHinhAnh.length > 0 ?
                                            <Image.PreviewGroup>
                                                <Space direction='vertical' size={0} style={{ marginBottom: '10px' }}>
                                                    {data.bangQuangCao.danhSachHinhAnh.length > 1 ? (
                                                        <Space size={5}>
                                                            {data.danhSachHinhAnh.map((t, index) => {
                                                                return <Image key={index.toString()} width={120} height={120}
                                                                    src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} />;
                                                            })}
                                                        </Space>
                                                    ) : <Image width={150} height={150} src={`${process.env.REACT_APP_BASE_API}Upload/image/${data.bangQuangCao.danhSachHinhAnh[0]}`} alt='avatar' />}
                                                </Space>
                                            </Image.PreviewGroup> :
                                            <Empty />}
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

export default DetailFixBoard