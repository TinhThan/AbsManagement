import { Card, Col, Image, Form, Input, Row, Space, Tooltip } from 'antd';
import React, { useEffect, useRef } from 'react';
import imageIcon from "../../assets/Image.svg";
import { DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import ModalDetail from '../../components/Modal/modalDetail';
import { getDistrict, getWardByDistrict } from '../../utils/getWard';
import { tinhTrangDiemDatQuangCao } from '.';
import mapboxgl from 'mapbox-gl';
import './style.scss'

interface Props {
  onCancel: () => void;
  diemDatQuangCao?: DiemDatQuangCaoModel;
  
}

export function ModalDetailDiemDatQuangCao(props: Props): JSX.Element {
    const { onCancel, diemDatQuangCao } = props;

    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY || '';

    const mapContainerRef = useRef(null);
    useEffect(() => {
        const map = new mapboxgl.Map({
            container: mapContainerRef.current || '',
            style: "mapbox://styles/mapbox/streets-v11",
            center: [106.707222, 10.752444], // Ho Chi Minh City
            zoom: 12,
        });
        return () => map.remove();
    }, []);


    if (!diemDatQuangCao) {
        return <></>;
    }
    return (
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
                    <div id="map" ref={mapContainerRef}  />
                </Space>
            </Col>
        </Row>
    </ModalDetail>
    );
}
