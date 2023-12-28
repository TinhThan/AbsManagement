import { Card, Col, Image, Form, Input, Row, Space, Tooltip } from 'antd';
import React, { useEffect, useRef, useState } from 'react';
import imageIcon from "../../assets/Image.svg";
import { DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import ModalDetail from '../../components/Modal/modalDetail';
import { getDistrict, getWardByDistrict } from '../../utils/getWard';
import { tinhTrangDiemDatQuangCao } from '.';
import mapboxgl from 'mapbox-gl';
import './style.scss'
import { SpaceAnyToGeoJson } from '../../utils/anyToGeoJson';
import { cluterLayers, unclusteredLabelLayer, unclusteredLayer } from '../../components/map/layers';
import MapboxGeocoder from '@mapbox/mapbox-gl-geocoder';
import pointInfo from '../../components/point/pointInfo';
import SpaceInfo from '../../components/point/spaceInfo';

interface Props {
  onCancel: () => void;
  diemDatQuangCao?: DiemDatQuangCaoModel;
  
}

export function ModalDetailDiemDatQuangCao(props: Props): JSX.Element {
    const { onCancel, diemDatQuangCao } = props;
    const mapContainerRef = useRef(null);

    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY || '';
    useEffect(() => {
        
        const map = new mapboxgl.Map({
            container: mapContainerRef.current || '',
            style: "mapbox://styles/mapbox/streets-v11",
            center: [106.707222, 10.752444], // Ho Chi Minh City
            zoom: 12,
        });
        map.on("load", () => {
            map.addSource("points", {
                type: "geojson",
                data: SpaceAnyToGeoJson(diemDatQuangCao) || undefined,
                cluster: true,
                clusterMaxZoom: 14,
                clusterRadius: 50,
            });

            map.addLayer(cluterLayers);
    
            map.addLayer(unclusteredLayer);

            map.addLayer(unclusteredLabelLayer);

            map.on('mouseenter', 'unclustered-point', () => {
                map.getCanvas().style.cursor = 'pointer';
                });

            map.on('mouseleave', 'unclustered-point', () => {
            map.getCanvas().style.cursor = '';
            });
        });
        
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

        //onclick mapp
        map.on("click", async (event) => {
            const featuresLayerClusters = map.queryRenderedFeatures(event.point, {
                layers: ['clusters','unclustered-point']
            });

            if(featuresLayerClusters.length > 0)
            {
                console.log("featuresLayerClusters",featuresLayerClusters)
                return;
            }

            lng = event.lngLat.lng.toFixed(6);
            lat = event.lngLat.lat.toFixed(6);
            const data = await reverseGeocoding(lat, lng);
            // setLocation(data)
            // setSurfaces([])
            // setCollapsed(false)
            const point = pointInfo(data?.diaChi);
            new mapboxgl.Popup({
                closeOnClick: true,
                closeButton: false,
            })
                .setLngLat(event.lngLat)
                .setHTML(point)
                .addTo(map);
        });

        map.on('click', 'unclustered-point', async (e : any) => {
            const coordinates = e.features[0].geometry.coordinates.slice();
            // const description = e.features[0].properties;

            // await getSurfaceBySpace(description.id)
            
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }
            // const space = spaces.find(t=>t.id === description.id);
            // if(space){
                // console.log("space",space)
                // setLocation({
                //     lng: coordinates[0],
                //     lat: coordinates[1],
                //     diaChi: space.diaChi,
                //     phuong: space.phuong,
                //     quan: space.quan
                // })
                // console.log("description",description)
                // setCollapsed(false)
            //     new mapboxgl.Popup()
            //     .setLngLat(coordinates)
            //     .setHTML(SpaceInfo(space))
            //     .addTo(map);
            // }
        });

        map.on('click', 'clusters', (e:any) => {
            const features : any = map.queryRenderedFeatures(e.point, {
                layers: ['clusters']
            });
            const clusterId = features[0].properties.cluster_id;
            const sources : any = map.getSource('points');
            sources.getClusterExpansionZoom(
                clusterId,
                (err, zoom) => {
                if (err) return;
                map.easeTo({
                center: features[0].geometry.coordinates,
                zoom: zoom
                });
                }
            );
        });

        // Clean up on unmount
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
        <ModalDetail
        width={1000}
        title={
            <Row wrap={false}>
                <Col flex='auto'>Chi tiết điểm đặt quảng cáo</Col>
            </Row>
        }
        modalCancel={onCancel}
        >
        <Row gutter={[50,50]}>
            <Col span={12}>
                <Form.Item label={"Địa chỉ"}>
                    <Tooltip placement='top' style={{ width: '100%' }} title={diemDatQuangCao.diaChi}>
                        <Input value={diemDatQuangCao.diaChi} readOnly style={{ textOverflow: 'ellipsis' }} />
                    </Tooltip>
                </Form.Item>
                <Form.Item label={"Phường"}>
                    <Input value={"Phường " + getWardByDistrict(diemDatQuangCao.quan,diemDatQuangCao.phuong).name} readOnly/>
                </Form.Item>
                <Form.Item label={"Quận"}>
                    <Input value={"Quận " + getDistrict(diemDatQuangCao.quan).name} readOnly />
                </Form.Item>
                <Form.Item label={"Loại vị trí"}>
                    <Input value={diemDatQuangCao.tenLoaiViTri} readOnly/>
                </Form.Item>
                <Form.Item label={"Hình thức quảng cáo"}>
                    <Input value={diemDatQuangCao.tenHinhThucQuangCao} readOnly/>
                </Form.Item>
                <Form.Item label={"Tình trạng"}>
                    <Input value={tinhTrangDiemDatQuangCao[diemDatQuangCao.idTinhTrang]} readOnly />
                </Form.Item>
            </Col>
            <Col span={12}>
            <Space direction='vertical' style={{width:'100%'}}>
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
                        {diemDatQuangCao.danhSachHinhAnh && diemDatQuangCao.danhSachHinhAnh.length > 0 && 
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
                            </Image.PreviewGroup> }
                    </Card>
                </Space>
                <Space direction='vertical' style={{width:'100%'}}>
                    <div id="map" ref={mapContainerRef}  className='map-container'/>
                </Space>
            </Col>
        </Row>
    </ModalDetail>
    </>
    );
}
