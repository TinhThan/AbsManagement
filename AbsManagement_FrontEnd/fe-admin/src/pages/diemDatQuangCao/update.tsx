import { Button, Card, Col, Form, Input, Row, Select, Space, Tooltip, Image, Upload, UploadFile, Spin } from 'antd';
import React, { Suspense, useEffect, useRef, useState } from 'react';
import imageIcon from "../../assets/Image.svg";
import { CapNhatDiemDatQuangCaoModel, DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import { diemDatQuangCaoAPI } from '../../apis/diemDatQuangCao';
import { LoaiViTriModel } from '../../apis/loaiViTri/model';
import { HinhThucQuangCaoModel } from '../../apis/hinhThucQuangCao/model';
import { loaiViTriAPI } from '../../apis/loaiViTri';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';
import { PlusOutlined } from '@ant-design/icons';
import axios from 'axios';
import { getDistrict, getWardByDistrict } from '../../utils/getWard';
import { hinhThucQuangCaoAPI } from '../../apis/hinhThucQuangCao';
import { PageLoading } from '@ant-design/pro-components';
import { useNavigate, useParams } from 'react-router-dom';
import TextArea from 'antd/es/input/TextArea';
import mapboxgl, { Marker } from 'mapbox-gl';
import { Notification } from '../../utils';
import MapboxGeocoder from '@mapbox/mapbox-gl-geocoder';

const tinhTrangDiemDatQuangCao = [
    {
        ma:"DaQuyHoach",
        ten:"Đã quy hoạch",
    },
    {
        ma:"ChuaQuyHoach",
        ten:"Chưa quy hoạch"
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


export function UpdateDiemDatQuangCao(): JSX.Element {
    const navigate = useNavigate();
    const { id } = useParams();
    const [diemDatQuangCao, setDiemDatQuangCao] = useState<DiemDatQuangCaoModel>();
    const [form] = Form.useForm<CapNhatDiemDatQuangCaoModel>();
    const [loading, setLoading] = useState(false);
    const [user,setUser] = useState<UserStorage>();
    const [danhSachViTri,setDanhSachViTri] = useState<any[]>([]);
    const [fileList,setFileList] = useState<UploadFile[]>([]);
    const [loaiViTris,setLoaiViTris] = useState<LoaiViTriModel[]>([]);
    const [hinhThucQuangCaos,setHinhThucQuangCaos] = useState<HinhThucQuangCaoModel[]>([]);
    const [isVisible, setIsVisible] = useState(false);
    const [srcImage, setSrcImage] = useState('');

    const mapContainerRef = useRef(null);
    const marketMap = useRef(new mapboxgl.Marker());
    
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
            getDetail(id)
            return;
        }
        form.setFieldsValue(diemDatQuangCao)
        getLoaiViTris();
        getHinhThucQuangCaos();
        form.setFieldValue('quan', "Quận " + getDistrict(diemDatQuangCao.quan).name)
        setDanhSachViTri(diemDatQuangCao.danhSachViTri)
        form.setFieldValue('phuong', "Phường " + getWardByDistrict(diemDatQuangCao.quan,diemDatQuangCao.phuong).name)
        const fileImages:UploadFile[] = [];
        diemDatQuangCao.danhSachHinhAnh.forEach((image,index)=>{
            const imageInfo: UploadFile = {
                name: image,
                uid: image + index,
                status: 'done',
                url: `${process.env.REACT_APP_BASE_API}Upload/image/${image}`,
                thumbUrl: `${process.env.REACT_APP_BASE_API}Upload/image/${image}`,
            };
            fileImages.push(imageInfo);
        })
        setFileList(fileImages)
        const useInfo = UserInfoStorage.get();
        if(useInfo)
        {
            setUser(useInfo);
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
            marketMap.current = new mapboxgl.Marker()
                .setLngLat([diemDatQuangCao.danhSachViTri[0],diemDatQuangCao.danhSachViTri[1]])
                .addTo(map);
            map.addControl(new mapboxgl.FullscreenControl());
        });

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
            marker: false, // Use the geocoder's default marker style
            bbox: [106.6297, 10.6958, 106.8413, 10.8765], // Set the bounding box coordinates
            placeholder: "Tìm kiếm địa điểm", // Placeholder text for the search bar,
            autocomplete: true,
            language:'vi'
        });
        map.addControl(geocoder, "top-left");

        //onclick mapp
        map.on("click", async (event) => {
            const lng = event.lngLat.lng.toFixed(6);
            const lat = event.lngLat.lat.toFixed(6);
            const data = await reverseGeocoding(lat, lng);
            marketMap.current.setLngLat([data?.lng,data?.lat])
            form.setFieldValue('quan', "Quận " + getDistrict(data?.quan).name)
            setDanhSachViTri([data?.lng,data?.lat])
            form.setFieldValue('phuong', "Phường " + getWardByDistrict(data?.quan,data?.phuong).name)
            form.setFieldValue('diaChi', data?.diaChi)
            // new mapboxgl.Popup({
            //     closeOnClick: true,
            //     closeButton: true,
            // })
            //     .setLngLat(event.lngLat)
            //     .setHTML(point)
            //     .addTo(map.current);
        });

        return () => map.remove();
    }, [diemDatQuangCao])

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

    async function onSubmit(_model: CapNhatDiemDatQuangCaoModel) {
        setLoading(true)
        if (_model &&  diemDatQuangCao) {
            console.log("_model",_model)
            _model.danhSachHinhAnh = await uploadImages();
            _model.quan = getDistrict(_model.quan).postcode;
            _model.danhSachViTri = danhSachViTri;
            _model.phuong = getWardByDistrict(_model.quan,_model.phuong).postcode;
            diemDatQuangCaoAPI
            .CapNhat(diemDatQuangCao.id,_model);
        }
            setLoading(false)
    }

    function getLoaiViTris() {
        setLoading(true)
        loaiViTriAPI
            .DanhSach()
            .then((response) => {
                if(response && response.status === 200)
                {
                    setLoaiViTris(response.data || []);
                }
            });
        setLoading(false)
    }

    
    function getHinhThucQuangCaos() {
        setLoading(true)
        hinhThucQuangCaoAPI
            .DanhSach()
            .then((response) => {
                if(response && response.status === 200)
                {
                    setHinhThucQuangCaos(response.data || []);
                }
            });
        setLoading(false)
    }

    return (
        <>
        <Suspense fallback={<PageLoading/>}>
            <Spin spinning={loading}>
                <Space className='space-button-on-top'>
                    <Button danger onClick={()=> navigate(-1)}><b>Thoát</b></Button>
                    <Button type='primary' onClick={()=>form.submit()}><b>Lưu</b></Button>
                </Space>
                <Form layout='vertical'             
                    form={form}
                    onFinish={onSubmit}>
                    <Row gutter={[10,10]}>
                        <Col span={14}>
                            <Card title={<b>Thông tin điểm đặt quảng cáo</b>} bordered={false}>
                                <Form.Item label={"Loại vị trí"} name={'idLoaiViTri'}>
                                    <Select placeholder="Vui lòng chọn loại vị trí" >
                                        {loaiViTris && loaiViTris.map((option) => (
                                            <Select.Option key={option.id} value={option.id}>{option.ma} - {option.ten}</Select.Option>
                                        ))}
                                    </Select>
                                </Form.Item>
                                <Form.Item label={"Hình thức quảng cáo"} name={'idHinhThucQuangCao'}>
                                    <Select placeholder="Vui lòng chọn hình thức quảng cáo" >
                                        {hinhThucQuangCaos && hinhThucQuangCaos.map((option) => (
                                            <Select.Option key={option.id} value={option.id}>{option.ma} - {option.ten}</Select.Option>
                                        ))}
                                    </Select>
                                </Form.Item>
                                <Form.Item label={"Phường"} name='phuong'>
                                    <Input/>
                                </Form.Item>
                                <Form.Item label={"Quận"} name='quan'>
                                    <Input/>
                                </Form.Item>
                                <Form.Item label={"Địa chỉ"} name='diaChi'>
                                        <TextArea rows={4}/>
                                </Form.Item>
                                <Form.Item label={"Tình trạng"} name={'idTinhTrang'}>
                                    <Select placeholder="Vui lòng chọn tình trạng" >
                                        {tinhTrangDiemDatQuangCao && tinhTrangDiemDatQuangCao.map((option) => (
                                            <Select.Option key={option.ma} value={option.ma}>{option.ten}</Select.Option>
                                        ))}
                                    </Select>
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
                                    }>
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
