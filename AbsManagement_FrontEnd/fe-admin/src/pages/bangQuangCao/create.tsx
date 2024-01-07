import { Button, Card, Col, Form, Input, Row, Select, Space, Tooltip, Image, Upload, UploadFile, Spin, Empty, DatePicker } from 'antd';
import React, { Suspense, useEffect, useRef, useState } from 'react';
import imageIcon from "../../assets/Image.svg";
import { CapNhatBangQuangCaoModel, BangQuangCaoModel, ThemMoiBangQuangCaoModel } from '../../apis/bangQuangCao/bangQuangCaoModel';
import { LoaiViTriModel } from '../../apis/loaiViTri/model';
import { HinhThucQuangCaoModel } from '../../apis/hinhThucQuangCao/model';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';
import { PlusOutlined } from '@ant-design/icons';
import axios from 'axios';
import { getDistrictWithCode, getDistrictWithName, getWardByDistrictWithCode, getWardByDistrictWithName } from '../../utils/getWard';
import { PageContainer, PageLoading } from '@ant-design/pro-components';
import { useNavigate, useParams } from 'react-router-dom';
import TextArea from 'antd/es/input/TextArea';
import mapboxgl from 'mapbox-gl';
import { Notification } from '../../utils';
import MapboxGeocoder from '@mapbox/mapbox-gl-geocoder';
import dataHCM from '../../assets/new-dataHCM.json';
import { phieuChinhSuaAPI } from '../../apis/phieuChinhSua';
import { bangQuangCaoAPI } from '../../apis/bangQuangCao/bangQuangCaoAPI';
import { DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import { diemDatQuangCaoAPI } from '../../apis/diemDatQuangCao';
import { loaiBangQuangCaoAPI } from '../../apis/loaiBangQuangCao';
import { LoaiBangQuangCaoModel } from '../../apis/loaiBangQuangCao/model';
import { tinhTrangDiemDatQuangCao } from '../diemDatQuangCao/list';
import { SpaceToGeoJson } from '../../utils/anyToGeoJson';
import { LayerSpaceNotPanned, LayerSpaceNotPannedLabel, LayerSpaceNotPannedPoint, LayerSpacePanned, LayerSpacePannedLabel, LayerSpacePannedPoint } from '../../utils/layerMap';
import dayjs from 'dayjs';
import { ThemPhieuCapPhepModel } from '../../apis/phieuCapPhepBangQuangCao/model';
import { phieuCapPhepBangQuangCaoAPI } from '../../apis/phieuCapPhepBangQuangCao';
import { messageValidate } from '../../utils/validator';

export const tinhTrangBangQuangCao = [
    {
        ma:"ChuaQuyHoach",
        ten:"Chưa quy hoạch",
    },
    {
        ma:"ChoCapPhep",
        ten:"Chờ cấp phép",
    },
    {
        ma:"ChoDuyet",
        ten:"Chờ duyệt chỉnh sửa"
    },
    {
        ma:"DaQuyHoach",
        ten:"Đã quy hoạch"
    }
]

const getBase64 = (file) =>
    new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
});

function convertImageToJpg(fileName: string): string {
    const fileNameWithoutExtension = fileName.split(".")[0];
    return `${fileNameWithoutExtension}.jpg`;
}


export function CreateBangQuangCao(): JSX.Element {
    const navigate = useNavigate();
    const [form] = Form.useForm<ThemMoiBangQuangCaoModel>();
    const [loading, setLoading] = useState(false);
    const [user,setUser] = useState<UserStorage>();
    const [fileList,setFileList] = useState<UploadFile[]>([]);
    const [loaiBangQuangCaos,setLoaiBangQuangCaos] = useState<LoaiBangQuangCaoModel[]>([]);
    const [isVisible, setIsVisible] = useState(false);
    const [srcImage, setSrcImage] = useState('');
    const [diemDatQuangCaos,setDiemDatQuangCaos] = useState<DiemDatQuangCaoModel[]>([]);
    const [diemQuangCao,setDiemDatQuangCao]= useState<DiemDatQuangCaoModel>();

    const mapContainerRef = useRef(null);
    const marketMap = useRef(new mapboxgl.Marker());
    const map = useRef<any>(null);
    
    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY || '';

    function getDiemDatQuangCaos(useInfo) {
        diemDatQuangCaoAPI
            .DanhSach(useInfo?.noiCongTac[0] || '',useInfo?.noiCongTac[1] || '')
            .then((response) => {
                if(response && response.status === 200)
                {
                    setDiemDatQuangCaos(response.data || []);
                }
            });
    }

    useEffect(() => {
        setLoading(true);
        const useInfo = UserInfoStorage.get();
        if(useInfo)
        {
            setUser(useInfo);
        }
        const now = new Date;
        form.setFieldValue('ngayBatDau',dayjs(now))
        form.setFieldValue('ngayBatDau',dayjs(now.setFullYear(now.getFullYear() + 1)))
        getDiemDatQuangCaos(useInfo)
        getLoaiBangQuangCaos();
    }, [])

    useEffect(() => {
        if(!mapContainerRef.current || !diemDatQuangCaos || !user)
        {
            return;
        }
        let quanUser;
        if(user.role !== "CanBoSo")
        {
            quanUser = getDistrictWithCode(user.noiCongTac[0] || '');
        }

        map.current = new mapboxgl.Map({
            container: mapContainerRef.current,
            style: "mapbox://styles/mapbox/streets-v11",
            center: quanUser ? quanUser.center : [106.707222, 10.752444], // Ho Chi Minh City
            zoom: 12,
        });

        map.current.on('load', () => {
            //Handle point space panneds
            map.current.addSource("spacePanneds", {
                type: "geojson",
                data: SpaceToGeoJson(diemDatQuangCaos, "DaQuyHoach"),
                cluster: true,
                clusterMaxZoom: 14,
                clusterRadius: 50,
            });

            map.current.addLayer(LayerSpacePanned);

            map.current.addLayer(LayerSpacePannedPoint);

            map.current.addLayer(LayerSpacePannedLabel);

            //Handle poin space not panned
            map.current.addSource("spaceNotPanneds", {
                type: "geojson",
                data: SpaceToGeoJson(diemDatQuangCaos, null),
                cluster: true,
                clusterMaxZoom: 14,
                clusterRadius: 50,
            });

            map.current.addLayer(LayerSpaceNotPanned);

            map.current.addLayer(LayerSpaceNotPannedPoint);

            map.current.addLayer(LayerSpaceNotPannedLabel);

            marketMap.current = new mapboxgl.Marker()
                .setLngLat([0,0])
                .addTo( map.current);
            map.current.addControl(new mapboxgl.FullscreenControl());
        });

        map.current.addControl(
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
            marker: false, // Use the geocoder's default marker style
            bbox: [106.6297, 10.6958, 106.8413, 10.8765], // Set the bounding box coordinates
            placeholder: "Tìm kiếm địa điểm", // Placeholder text for the search bar,
            autocomplete: true,
            language:'vi'
        });
        map.current.addControl(geocoder, "top-left");

        map.current.on('mouseenter', ['space-panned-point','space-not-panned-point'], () => {
            map.current.getCanvas().style.cursor = 'pointer';
            });

        map.current.on('mouseleave', ['space-panned-point','space-not-panned-point'], () => {
                map.current.getCanvas().style.cursor = '';
        });

        //Onclick space
        map.current.on('click', ['space-panned-point','space-not-panned-point'], async (e) => {
            const coordinates = e.features[0].geometry.coordinates.slice();
            const description = e.features[0].properties;
            
            const diaDiem = diemDatQuangCaos.find(t=>t.id === description.id);
            setDiemDatQuangCao(diaDiem);
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }
            marketMap.current.setLngLat(coordinates)
        });

        map.current.on('click', 'space-panned', (e) => {
            const features = map.current.queryRenderedFeatures(e.point, {
                layers: ['space-panned']
            });
            if(features.length > 0) {
                const clusterId = features[0].properties.cluster_id;
                map.current.getSource('spacePanneds').getClusterExpansionZoom(
                    clusterId,
                    (err, zoom) => {
                    if (err) return;
                    
                    map.current.easeTo({
                    center: features[0].geometry.coordinates,
                    zoom: zoom
                    });
                    }
                );
            }
        });

        map.current.on('click', 'space-not-panned', (e) => {
            const features = map.current.queryRenderedFeatures(e.point, {
                layers: ['space-not-panned']
            });
            if(features.length > 0) {
                const clusterId = features[0].properties.cluster_id;
                map.current.getSource('spaceNotPanneds').getClusterExpansionZoom(
                    clusterId,
                    (err, zoom) => {
                    if (err) return;
                    
                    map.current.easeTo({
                    center: features[0].geometry.coordinates,
                    zoom: zoom
                    });
                    }
                );
            }
        });

        setLoading(false);
        return () =>  map.current.remove();
    }, [diemDatQuangCaos])
    

    async function uploadImages() {
        let danhSachHinhAnh:string[] = []
        if (fileList) {
            var data = new FormData();
            fileList.filter(file=> file.originFileObj).forEach(file=> data.append('hinhAnhs', file.originFileObj || ''));
            await axios.post(`${process.env.REACT_APP_BASE_API}api/hinhanh/multip`,data,{
                headers: {'Content-Type': 'multipart/form-data'}
                }).then((response) => {
                console.log("response",response)
                if(response && response.status === 200)
                {
                    danhSachHinhAnh = fileList.map(el=> convertImageToJpg(el.name));
                }
            }).catch((e)=>{
                console.log(e)
            });
        }
        return danhSachHinhAnh
    }

    const uploadButton = (
        <div>
            <PlusOutlined />
            <div style={{ marginTop: 8 }}>Upload</div>
        </div>
    );

    const onChange = async (info) => {
        const file = info.file;
        if (file.status === 'error') {
            file.status = 'done';
            setFileList([...fileList,file]);
        } 
        setFileList(info.fileList);
    };

    const handlePreview = async (file) => {
        if (!file.url && !file.preview) {
            file.preview = await getBase64(file.originFileObj);
        }
        setSrcImage(file.url || file.preview);
        setIsVisible(true);
    };

    async function onSubmit(_model: ThemMoiBangQuangCaoModel) {
        setLoading(true)
        if (_model) {
            console.log("_model",_model)
            _model.danhSachHinhAnh = await uploadImages();
            _model.idDiemDatQuangCao = diemQuangCao?.id ?? 0;   
            bangQuangCaoAPI.TaoMoi(_model).then((response)=>{
                if(response && response.status === 200){
                    navigate(-1)
                }
            });
        }
            setLoading(false)
    }

    function getLoaiBangQuangCaos() {
        setLoading(true)
        loaiBangQuangCaoAPI
            .DanhSach()
            .then((response) => {
                if(response && response.status === 200)
                {
                    setLoaiBangQuangCaos(response.data || []);
                }
            });
        setLoading(false)
    }

    return (
        <>
        <Suspense fallback={<PageLoading/>}>
            <PageContainer title={"Tạo mới bảng quảng cáo"}>
            <Spin spinning={loading}>
                <Space className='space-button-on-top'>
                    <Button danger onClick={()=> navigate(-1)}><b>Thoát</b></Button>
                    <Button type='primary' onClick={()=>form.submit()}><b>{'Tạo mới'}</b></Button>
                </Space>
                <Form layout='vertical'             
                    form={form}
                    onFinish={onSubmit}>
                    <Space direction='vertical' style={{width:'100%'}}>
                    <Row gutter={[10,10]}>
                        <Col span={14}>
                            <Card title={<b>Thông tin bảng quảng cáo</b>} bordered={false}>
                                <Form.Item label={"Loại bảng quảng cáo"} name={'idLoaiBangQuangCao'}
                                rules={[{ required: true ,message: messageValidate.RequireLoaiBangQuangCao}]}>
                                    <Select placeholder="Vui lòng chọn loại bảng quảng cáo" >
                                        {loaiBangQuangCaos && loaiBangQuangCaos.map((option) => (
                                            <Select.Option key={option.id} value={option.id}>{option.ma} - {option.ten}</Select.Option>
                                        ))}
                                    </Select>
                                </Form.Item>
                                <Form.Item label={"Kích thước"} name={'kichThuoc'}
                                    rules={[{ required: true , message: messageValidate.RequireKichThuoc}]}>
                                    <Input/>
                                </Form.Item>
                                <Form.Item label={"Ngày bắt đầu"} name={"ngayBatDau"}
                                            rules={[{ required: true ,type:'date' , message: messageValidate.RequireNgayBatDau}]}>
                                    <DatePicker />
                                </Form.Item>
                                <Form.Item label={"Ngày hết hạn"} name={"ngayHetHan"}
                                         rules={[{ required: true , type:'date' , message: messageValidate.RequireNgayHetHan}]}>
                                    <DatePicker />
                                </Form.Item>
                                <Form.Item label="Danh sách hình ảnh">
                                    <Image.PreviewGroup>
                                                <Space direction='vertical' size={0}>
                                                <Upload
                                                    listType="picture-card"
                                                    fileList={fileList}
                                                    onChange={onChange}
                                                    onPreview={handlePreview}
                                                >
                                                    {fileList && fileList.length < 3 && uploadButton}
                                                </Upload>
                                                </Space>
                                            </Image.PreviewGroup>
                                </Form.Item>
                            </Card>
                        </Col>
                        <Col span={10}>
                            <Space direction='vertical' style={{width:'100%'}}>
                                <Card title={<b>Thông tin công ty</b>} bordered={false}>
                                    <Form.Item label={"Tên công ty"} name='tenCongTy'
                                rules={[{ required: true ,message: messageValidate.RequireTenCongTy}]}>
                                        <Input/>
                                    </Form.Item>
                                    <Form.Item label={"Email"} name='email'
                                            rules={[{ required: true ,message: messageValidate.RequireEmail}]}>
                                        <Input type='email'/>
                                    </Form.Item>
                                    <Form.Item label={"Số điện thoại"} name='soDienThoai'
                                rules={[{ required: true ,message: messageValidate.RequireSoDienThoai}]}>
                                        <Input/>
                                    </Form.Item>
                                    <Form.Item label={"Địa chỉ"} name='diaChiCongTy'
                                rules={[{ required: true ,message: messageValidate.RequireDiaChi}]}>
                                        <TextArea rows={2}/>
                                    </Form.Item>
                                </Card>
                            </Space>
                        </Col>
                </Row>
                <Row  gutter={[10,10]}>
                    <Col span={14}>
                            <Card title={<b>Thông tin điểm đặt quảng cáo</b>} bordered={false}>
                                <Form.Item label={"Hình thức quảng cáo"}>
                                    <Input value={diemQuangCao?.tenHinhThucQuangCao} disabled/>
                                </Form.Item>
                                <Form.Item label={"Loại vị trí"}>
                                    <Input value={diemQuangCao?.tenLoaiViTri} disabled/>
                                </Form.Item>
                                <Form.Item label={"Quận"}>
                                    <Input  disabled value={`Quận ${getDistrictWithCode(diemQuangCao?.quan || '').name}`}/>
                                </Form.Item>
                                <Form.Item label={"Phường"}>
                                    <Input  disabled value={`Quận ${getWardByDistrictWithCode(diemQuangCao?.quan || '',diemQuangCao?.phuong || '').name}`}/>
                                </Form.Item>
                                <Form.Item label={"Địa chỉ"} >
                                        <TextArea rows={4} value={diemQuangCao?.diaChi}  disabled/>
                                </Form.Item>
                                <Form.Item label={"Tình trạng"}>
                                    <Input value={tinhTrangDiemDatQuangCao[diemQuangCao?.idTinhTrang || 'DaQuyHoach']} disabled/>
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
        <Image
        width={200}
        style={{ display: 'none' }}
        preview={{
            visible: isVisible,
            src: srcImage,
            onVisibleChange: (value) => {
                setIsVisible(value);
                setSrcImage('');
            },
        }}
    />
    </>
    );
}
