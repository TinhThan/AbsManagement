import { Card, Col, Image, Form, Input, Row, Space, Button, Empty, Spin } from 'antd';
import React, { Suspense, useEffect, useRef, useState } from 'react';
import imageIcon from "../../assets/Image.svg";
import { DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import { getDistrict, getWardByDistrict } from '../../utils/getWard';
import { tinhTrangDiemDatQuangCao } from './list';
import mapboxgl from 'mapbox-gl';
import './style.scss'
import MapboxGeocoder from '@mapbox/mapbox-gl-geocoder';
import { PageLoading } from '@ant-design/pro-components';
import { diemDatQuangCaoAPI } from '../../apis/diemDatQuangCao';
import { useNavigate, useParams } from 'react-router-dom';
import TextArea from 'antd/es/input/TextArea';
import { Notification } from '../../utils';


export function DetailDiemDatQuangCao(): JSX.Element {   
    const {id} = useParams();
    const navigate = useNavigate();
    const [loading,setLoading] = useState(false);
    const [diemDatQuangCao,setDiemDatQuangCao] = useState<DiemDatQuangCaoModel>();
    const mapContainerRef = useRef(null);
    
    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY || '';

    function getDetail(id: any) {
        setLoading(true);
        diemDatQuangCaoAPI
            .ChiTiet(id)
            .then((response) => {
            if(response && response.status === 200){
                    setLoading(false);
                    setDiemDatQuangCao(response.data);
                }
            })
            .catch(() => {
                Notification.Warning("Điểm đặt quảng cáo không tồn tại.")
                navigate(-1)
            });
            setLoading(false);
    }

    useEffect(() => {
        if(!diemDatQuangCao)
        {
            getDetail(id);
            return;
        }
        if(!mapContainerRef.current)
        {
            return;
        }
        const map = new mapboxgl.Map({
            container: mapContainerRef.current,
            style: "mapbox://styles/mapbox/streets-v11",
            center: [diemDatQuangCao.danhSachViTri[0],diemDatQuangCao.danhSachViTri[1]], // Ho Chi Minh City
            zoom: 15,
        });

        map.on('load', () => {

            new mapboxgl.Marker()
                .setLngLat([diemDatQuangCao.danhSachViTri[0],diemDatQuangCao.danhSachViTri[1]])
                .addTo(map);
            map.addControl(new mapboxgl.FullscreenControl());
        });
        return () => map.remove();
    }, [diemDatQuangCao])

    return (
        <Suspense fallback={<PageLoading/>}>
                <Spin spinning={loading}>
                <Space className='space-button-on-top'>
                    <Button danger onClick={()=> navigate(-1)}><b>Thoát</b></Button>
                </Space>
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
                                <Form.Item label={"Phường"}>
                                    <Input value={"Phường " + getWardByDistrict(diemDatQuangCao?.quan || '',diemDatQuangCao?.phuong || '').name} readOnly/>
                                </Form.Item>
                                <Form.Item label={"Quận"}>
                                    <Input value={"Quận " + getDistrict(diemDatQuangCao?.quan || '').name} readOnly />
                                </Form.Item>
                                <Form.Item label={"Địa chỉ"}>
                                        <TextArea value={diemDatQuangCao?.diaChi} rows={4} readOnly/>
                                </Form.Item>
                                <Form.Item label={"Tình trạng"}>
                                    <Input value={tinhTrangDiemDatQuangCao[diemDatQuangCao?.idTinhTrang  || 'ChuaXuLy']} readOnly />
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
                                        <b>Danh sách hình ảnh</b>
                                    </Space>
                                    }
                                >
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
                                </Card>
                        </Space>
                        <Space style={{width:'100%'}}>
                            <Card title={<b>Thông tin địa điểm</b>} bordered={false}>
                                <div>
                                    <div id="map" ref={mapContainerRef} className='map-container-space'/>
                                </div>
                            </Card>
                        </Space>
                    </Col>
                </Row>
            </Form>
                </Spin>
        </Suspense>
    );
}
