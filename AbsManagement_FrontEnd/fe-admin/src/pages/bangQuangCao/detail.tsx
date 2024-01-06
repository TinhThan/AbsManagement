import { Button, Card, Col, Form, Input, Row, Space, Image, UploadFile, Spin, Empty, DatePicker } from 'antd';
import React, { Suspense, useEffect, useRef, useState } from 'react';
import imageIcon from "../../assets/Image.svg";
import { CapNhatBangQuangCaoModel, BangQuangCaoModel } from '../../apis/bangQuangCao/bangQuangCaoModel';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';
import { getDistrictWithCode, getWardByDistrictWithCode } from '../../utils/getWard';
import { PageContainer, PageLoading } from '@ant-design/pro-components';
import { useNavigate, useParams } from 'react-router-dom';
import TextArea from 'antd/es/input/TextArea';
import mapboxgl from 'mapbox-gl';
import { Notification } from '../../utils';
import { bangQuangCaoAPI } from '../../apis/bangQuangCao/bangQuangCaoAPI';
import { DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import { diemDatQuangCaoAPI } from '../../apis/diemDatQuangCao';
import { tinhTrangDiemDatQuangCao } from '../diemDatQuangCao/list';
import dayjs from 'dayjs';
import { SpaceAnyToGeoJson } from '../../utils/anyToGeoJson';
import { tinhTrangBangQuangCao } from './create';

export function DetailBangQuangCao(): JSX.Element {
    const navigate = useNavigate();
    const { id } = useParams();
    const [bangQuangCao, setBangQuangCao] = useState<BangQuangCaoModel>();
    const [form] = Form.useForm<CapNhatBangQuangCaoModel>();
    const [loading, setLoading] = useState(false);
    const [user,setUser] = useState<UserStorage>();
    const [diemQuangCao,setDiemDatQuangCao]= useState<DiemDatQuangCaoModel>();

    const mapContainerRef = useRef(null);
    
    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY || '';

    function getDetail(id: any) {
        bangQuangCaoAPI
            .ChiTiet(id)
            .then((response) => {
            if(response && response.status === 200){
                    setLoading(false);
                    setBangQuangCao(response.data);
                }
            })
            .catch(() => {
                Notification.Warning("Bảng quảng cáo không tồn tại.")
                navigate(-1)
            });
    }

    function getDiemDatQuangCao(id: any) {
        diemDatQuangCaoAPI
            .ChiTiet(id)
            .then((response) => {
            if(response && response.status === 200){
                    setDiemDatQuangCao(response.data);
                }
            })
            .catch(() => {
                Notification.Warning("Điểm đặt quảng cáo không tồn tại.")
                navigate(-1)
            });
    }

    useEffect(() => {
        setLoading(true);
        if(!bangQuangCao)
        {
            getDetail(id)
            return;
        }
        getDiemDatQuangCao(bangQuangCao.idDiemDatQuangCao)
        const useInfo = UserInfoStorage.get();
        if(useInfo)
        {
            setUser(useInfo);
        }
    }, [bangQuangCao])

    useEffect(()=>{
        if(!mapContainerRef.current || !bangQuangCao || !diemQuangCao)
        {
            return;
        }
        const map = new mapboxgl.Map({
            container: mapContainerRef.current,
            style: "mapbox://styles/mapbox/streets-v11",
            center: [bangQuangCao.danhSachViTri[0],bangQuangCao.danhSachViTri[1]], // Ho Chi Minh City
            zoom: 15,
        });
        
        map.on('load', () => {
            console.log("diemquangcao",diemQuangCao)
            map.addSource("spacePanneds", {
                type: "geojson",
                data: SpaceAnyToGeoJson(diemQuangCao),
                cluster: true,
                clusterMaxZoom: 14,
                clusterRadius: 50,
            });
            map.addLayer({
                id: 'space-panned',
                type: 'circle',
                source: 'spacePanneds',
                filter: ['has', 'point_count'],
                paint: {
                    'circle-color': [
                        'step',
                        ['get', 'point_count'],
                        '#51bbd6',
                        100,
                        '#f1f075',
                        750,
                        '#f28cb1'
                    ],
                    'circle-radius': [
                        'step',
                        ['get', 'point_count'],
                        20,
                        100,
                        30,
                        750,
                        40
                    ]
                }
            });

            map.addLayer({
                id: "space-panned-point",
                type: "circle",
                source: "spacePanneds",
                filter: ["!", ["has", "point_count"]], // Filter out clustered points
                paint: {
                    "circle-color": "#abad15",
                    "circle-radius": 15,
                    "circle-stroke-width": 1,
                    "circle-stroke-color": "#fff"
                }
            });

            map.addLayer({
                id: 'space-panned-label',
                type: 'symbol',
                source: "spacePanneds",
                filter: ['!', ['has', 'point_count']], // Use the same filter as your circle layer
                layout: {
                    'text-field': 'QC', // This is the text that will be displayed
                    'text-font': ['Open Sans Semibold', 'Arial Unicode MS Bold'], // Set the text font
                    'text-size': 12, // Set the text size
                },
                paint: {
                    'text-color': '#ffffff', // Set the text color
                }
            })

            new mapboxgl.Marker()
                .setLngLat([bangQuangCao.danhSachViTri[0],bangQuangCao.danhSachViTri[1]])
                .addTo(map);
            map.addControl(new mapboxgl.FullscreenControl());
        });
        setLoading(false)
    },[bangQuangCao,diemQuangCao])

    return (
        <>
        <Suspense fallback={<PageLoading/>}>
            <PageContainer title={"Chi tiết bảng quảng cáo"}>
            <Spin spinning={loading}>
                <Space className='space-button-on-top'>
                    <Button danger onClick={()=> navigate(-1)}><b>Thoát</b></Button>
                    <Button type='primary' onClick={()=>form.submit()}><b>{user?.role !== 'CanBoSo' ? 'Gữi phiếu chỉnh sửa': 'Lưu'}</b></Button>
                </Space>
                <Form layout='vertical' >
                    <Space direction='vertical' style={{width:'100%'}}>
                    <Row gutter={[10,10]}>
                        <Col span={14}>
                            <Card title={<b>Thông tin bảng quảng cáo</b>} bordered={false}>
                                <Form.Item label={"Loại bảng quảng cáo"}>
                                    <Input value={bangQuangCao?.tenLoaiBangQuangCao} readOnly/>
                                </Form.Item>
                                <Form.Item label={"Kích thước"}>
                                    <Input value={bangQuangCao?.kichThuoc} readOnly/>
                                </Form.Item>
                                <Form.Item label={"Ngày bắt đầu"}>
                                    <DatePicker value={dayjs(bangQuangCao?.ngayBatDau)} disabled/>
                                </Form.Item>
                                <Form.Item label={"Ngày hết hạn"}>
                                    <DatePicker value={dayjs(bangQuangCao?.ngayHetHan)} disabled/>
                                </Form.Item>
                                <Form.Item label={"Tình trạng"}>
                                    <Input value={tinhTrangBangQuangCao[bangQuangCao?.idTinhTrang || "HoanThanh"]} readOnly/>
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
                                        <b>Danh sách hình ảnh bảng quảng cáo</b>
                                    </Space>
                                    }>
                                        {bangQuangCao && bangQuangCao.danhSachHinhAnh && bangQuangCao.danhSachHinhAnh.length > 0 ? 
                                        <Image.PreviewGroup>
                                                <Space size={5}>
                                                    {bangQuangCao.danhSachHinhAnh.map((t, index) => {
                                                        return <Image key={index.toString()} width={120} height={120}
                                                                src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} />;
                                                    })}
                                                </Space>
                                        </Image.PreviewGroup> :
                                <Empty/>}
                                </Card>
                            </Space>
                        </Col>
                </Row>
                <Row  gutter={[10,10]}>
                    <Col span={14}>
                            <Card title={<b>Thông tin điểm đặt quảng cáo</b>} bordered={false}>
                                <Form.Item label={"Hình thức quảng cáo"}>
                                    <Input value={diemQuangCao?.tenHinhThucQuangCao} readOnly/>
                                </Form.Item>
                                <Form.Item label={"Loại vị trí"}>
                                    <Input value={diemQuangCao?.tenLoaiViTri} readOnly/>
                                </Form.Item>
                                <Form.Item label={"Quận"}>
                                    <Input  readOnly value={`Quận ${getDistrictWithCode(diemQuangCao?.quan || '').name}`}/>
                                </Form.Item>
                                <Form.Item label={"Phường"}>
                                    <Input  readOnly value={`Quận ${getWardByDistrictWithCode(diemQuangCao?.quan || '',diemQuangCao?.phuong || '').name}`}/>
                                </Form.Item>
                                <Form.Item label={"Địa chỉ"} >
                                        <TextArea rows={4} value={diemQuangCao?.diaChi}  readOnly/>
                                </Form.Item>
                                <Form.Item label={"Tình trạng"}>
                                    <Input value={tinhTrangDiemDatQuangCao[diemQuangCao?.idTinhTrang || 'DaQuyHoach']} readOnly/>
                                </Form.Item>
                                <Form.Item label={"Danh sách hình ảnh"}>
                                    {diemQuangCao && diemQuangCao.danhSachHinhAnh && diemQuangCao.danhSachHinhAnh.length > 0 ? 
                                            <Image.PreviewGroup>
                                                    <Space size={5}>
                                                        {diemQuangCao.danhSachHinhAnh.map((t, index) => {
                                                            return <Image key={index.toString()} width={120} height={120}
                                                                    src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} />;
                                                        })}
                                                    </Space>
                                            </Image.PreviewGroup> :
                                    <Empty/>}
                                </Form.Item>
                            </Card>
                    </Col>
                    <Col span={10}>
                        <Card title={<b>Thông tin địa điểm</b>} bordered={false}>
                            <div>
                                <div id="map" ref={mapContainerRef} className='map-container-space-diemQuangCao'/>
                            </div>
                        </Card>
                    </Col>
                </Row>
                    </Space>
            </Form>
            </Spin>
            </PageContainer>
        </Suspense>
    </>
    );
}
