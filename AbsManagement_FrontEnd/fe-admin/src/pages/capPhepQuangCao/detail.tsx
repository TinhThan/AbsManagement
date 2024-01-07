import { PageContainer, PageLoading } from "@ant-design/pro-components";
import { FC, Suspense, useEffect, useRef, useState } from "react"
import { Button, Card, Col, Form, Input, Row, Space, Image, Spin, Empty } from "antd";
import { getDistrictWithCode, getWardByDistrictWithCode } from "../../utils/getWard";
import imageIcon from "../../assets/Image.svg";
import { useNavigate, useSearchParams } from "react-router-dom";
import moment from 'moment';
import mapboxgl from "mapbox-gl";
import { phieuCapPhepBangQuangCaoAPI } from "../../apis/phieuCapPhepBangQuangCao";

const DetailAcceptAds : FC = () => {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const paramId = searchParams.get('id');
    const [loading, setLoading] = useState(false);
    const [data, setData] = useState<any>();
    const [form] = Form.useForm();
    const mapContainerRef = useRef(null);

    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY || '';

    async function getDetail(id: string) {
        setLoading(true);
        await phieuCapPhepBangQuangCaoAPI
            .ChiTiet(parseInt(id))
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

    useEffect(() => {
        if (!mapContainerRef.current) {
            return;
        }
        const map = new mapboxgl.Map({
            container: mapContainerRef.current,
            style: "mapbox://styles/mapbox/streets-v11",
            center: [data ? data.danhSachViTri[0] : '106.681216', data ? data.danhSachViTri[1] : '10.76252'], // Ho Chi Minh City
            zoom: 15,
        });

        map.on('load', () => {
            new mapboxgl.Marker()
                .setLngLat([data ? data.danhSachViTri[0] : '106.681216', data ? data?.danhSachViTri[1] : '10.76252'])
                .addTo(map);
            map.addControl(new mapboxgl.FullscreenControl());
        });
        return () => map.remove();
    }, [data])

    return (
        <Suspense fallback={<PageLoading />}>
            <PageContainer title="Chi tiết điểm quảng cáo">
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
                                        <Input.TextArea value={data?.diaChi} readOnly rows={3} autoSize={{ minRows: 3, maxRows: 5 }} />
                                    </Form.Item>
                                    <Form.Item label='Phường'>
                                        <Input value={`Phường ${getWardByDistrictWithCode(data?.quan || '', data?.phuong || '').name}`} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Quận'>
                                        <Input value={`Quận ${getDistrictWithCode(data?.quan || '').name}`} readOnly />
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
                                        {data && data.danhSachHinhAnh && data.danhSachHinhAnh.length > 0 ?
                                            <Image.PreviewGroup>
                                                <Space direction='vertical' size={0} style={{ marginBottom: '10px' }}>
                                                    {data.danhSachHinhAnh.length > 1 ? (
                                                        <Space size={5}>
                                                            {data.danhSachHinhAnh.map((t, index) => {
                                                                return <Image key={index.toString()} width={120} height={120}
                                                                    src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} />;
                                                            })}
                                                        </Space>
                                                    ) : <Image width={150} height={150} src={`${process.env.REACT_APP_BASE_API}Upload/image/${data.danhSachHinhAnh[0]}`} alt='avatar' />}
                                                </Space>
                                            </Image.PreviewGroup> :
                                            <Empty />}
                                    </Card>
                                </Space>
                                <Space style={{ maxWidth: '100%', overflowX: 'auto' }}>
                                    <Card title={<b>Thông tin địa điểm</b>} bordered={false}>
                                        <div>
                                            <div id="map" ref={mapContainerRef} className='map-container-space w-full' />
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

export default DetailAcceptAds