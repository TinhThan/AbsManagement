import { Card, Col, Image, Form, Input, Row, Space, Tooltip, Button, Empty } from 'antd';
import React, { Suspense, useEffect, useRef, useState } from 'react';
import imageIcon from "../../assets/Image.svg";
import { DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import ModalDetail from '../../components/Modal/modalDetail';
import { getDistrict, getWardByDistrict } from '../../utils/getWard';
import { tinhTrangDiemDatQuangCao } from './list';
import mapboxgl from 'mapbox-gl';
import './style.scss'
import { SpaceAnyToGeoJson } from '../../utils/anyToGeoJson';
import { cluterLayers, unclusteredLabelLayer, unclusteredLayer } from '../../components/map/layers';
import MapboxGeocoder from '@mapbox/mapbox-gl-geocoder';
import pointInfo from '../../components/point/pointInfo';
import SpaceInfo from '../../components/point/spaceInfo';
import { PageLoading } from '@ant-design/pro-components';
import { diemDatQuangCaoAPI } from '../../apis/diemDatQuangCao';
import { useParams } from 'react-router-dom';
import TextArea from 'antd/es/input/TextArea';


export function DetailDiemDatQuangCao(): JSX.Element {   
    const {id} = useParams();
    const [loading,setLoading] = useState(false);
    const [diemDatQuangCao,setDiemDatQuangCao] = useState<DiemDatQuangCaoModel>();
    const mapContainerRef = useRef(null);

    async function getDetail(id: any) {
        setLoading(true);
        await diemDatQuangCaoAPI
            .ChiTiet(id)
            .then((response) => {
            if(response && response.status === 200){
                    setLoading(false);
                    setDiemDatQuangCao(response.data);
                    // let fileImages:string[] = [];
                    // for (const image of response?.data.danhSachHinhAnh || []) {
                    //     if (image) {
                    //         fileImages.push(`${process.env.REACT_APP_BASE_API}Upload/image/${image}`);
                    //     }
                    // }
                    // setImages(fileImages);
                }
            })
            .catch(() => {
                setLoading(false);
            });
    }

    useEffect(() => {
      getDetail(id);
    }, [])
    

    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY || '';
    useEffect(() => {
        if(! mapContainerRef.current)
        {
            return;
        }
        const map = new mapboxgl.Map({
            container: mapContainerRef.current,
            style: "mapbox://styles/mapbox/streets-v11",
            center: [diemDatQuangCao?.danhSachViTri[0] ||106.707222,diemDatQuangCao?.danhSachViTri[1] || 10.752444], // Ho Chi Minh City
            zoom: 12,
        });

        map.on('load', () => {

            new mapboxgl.Marker()
                .setLngLat([106.693771,10.780295])
                .addTo(map);

            //Add control
            map.addControl(
                new mapboxgl.GeolocateControl({
                    positionOptions: {
                    enableHighAccuracy: true,
                    },
                    trackUserLocation: true,
                }),
                "bottom-right"
            );

            map.addControl(new mapboxgl.FullscreenControl());
        
            const geocoder = new MapboxGeocoder({
                accessToken: mapboxgl.accessToken, // Set the access token
                mapboxgl: mapboxgl, // Set the mapbox-gl instance
                marker: true, // Use the geocoder's default marker style
                bbox: [106.6297, 10.6958, 106.8413, 10.8765], // Set the bounding box coordinates
                placeholder: "Tìm kiếm địa điểm", // Placeholder text for the search bar,
                autocomplete: true,
                language:'vi'
            });
            let lng;
            let lat;
            map.addControl(geocoder, "top-left");
            
        });
        return () => map.remove();
    }, []);

    async function reverseGeocoding(lat,lng){
        try {
            const response = await fetch(
                `https://api.mapbox.com/geocoding/v5/mapbox.places/${lng},${lat}.json?access_token=${process.env.REACT_APP_MAP_BOX_KEY}`
            );
            const data = await response.json();
            let features = data.features;
            let diaChi = features[0].place_name; //địa chỉ
            let  phuong = features[1].text;
            let quan = features[3].text;
            return {
                diaChi,
                phuong,
                quan,
                lng,
                lat
            };
        } catch (error) {
        console.log(error);
        }
    }


    if (!diemDatQuangCao) {
        return <></>;
    }
    return (
        <>
        <Suspense fallback={<PageLoading/>}>
                <Space className='space-button-on-top'>
                    <Button danger ><b>Thoát</b></Button>
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
                                                        return <Image key={index.toString()} width={120} height={120} src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} alt={index.toString()} />;
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
        </Suspense>
    </>
    );
}
