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
import { getDistrictWithCode, getDistrictWithName, getWardByDistrictWithCode, getWardByDistrictWithName } from '../../utils/getWard';
import { hinhThucQuangCaoAPI } from '../../apis/hinhThucQuangCao';
import { PageContainer, PageLoading } from '@ant-design/pro-components';
import { useNavigate, useParams } from 'react-router-dom';
import TextArea from 'antd/es/input/TextArea';
import mapboxgl from 'mapbox-gl';
import { Notification } from '../../utils';
import MapboxGeocoder from '@mapbox/mapbox-gl-geocoder';
import dataHCM from '../../assets/new-dataHCM.json';
import { ThemPhieuChinhSuaModel } from '../../apis/phieuChinhSua/model';
import { phieuChinhSuaAPI } from '../../apis/phieuChinhSua';
import { MessageBox } from '../../utils/messagebox';

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
    const [phuong, setPhuong] = useState<any>();
    const [quan, setQuan] = useState<any>();

    const mapContainerRef = useRef(null);
    const marketMap = useRef(new mapboxgl.Marker());
    const map = useRef<any>(null);
    
    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY || '';

    function getDetail(id: any) {
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
    }

    useEffect(() => {
        setLoading(true);
        if(!diemDatQuangCao)
        {
            getDetail(id)
            return;
        }
        const useInfo = UserInfoStorage.get();
        let quanUser;
        if(useInfo)
        {
            setUser(useInfo);
        }

        form.setFieldsValue(diemDatQuangCao)
        getLoaiViTris();
        getHinhThucQuangCaos();
        setQuan(getDistrictWithCode(diemDatQuangCao.quan))
        setPhuong(getWardByDistrictWithCode(diemDatQuangCao.quan,diemDatQuangCao.phuong))
        setDanhSachViTri(diemDatQuangCao.danhSachViTri)
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

        if(!mapContainerRef.current)
        {
            return;
        }
        map.current = new mapboxgl.Map({
            container: mapContainerRef.current,
            style: "mapbox://styles/mapbox/streets-v11",
            center: [diemDatQuangCao.danhSachViTri[0],diemDatQuangCao.danhSachViTri[1]], // Ho Chi Minh City
            zoom: 15,
        });

        map.current.on('load', () => {
            marketMap.current = new mapboxgl.Marker()
                .setLngLat([diemDatQuangCao.danhSachViTri[0],diemDatQuangCao.danhSachViTri[1]])
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

        //onclick mapp
        map.current.on("click", async (event) => {
            const lng = event.lngLat.lng.toFixed(6);
            const lat = event.lngLat.lat.toFixed(6);
            const data = await reverseGeocoding(lat, lng);
            const district = getDistrictWithName(data?.quan);
            const ward = getWardByDistrictWithName(district.postcode, data?.phuong);

            if(useInfo?.role === "CanBoQuan" && district.postcode !== useInfo.noiCongTac[0]){
                console.log("Warring chọn địa điểm")
                Notification.Warning(`Bạn chỉ có quyền hạng trên quận ${quanUser?.name}`)
                return;
            }

            if(useInfo?.role === "CanBoPhuong" && (district.postcode !==useInfo.noiCongTac[0] || ward.postcode !== useInfo.noiCongTac[1])){
                console.log("Warring chọn địa điểm")
                Notification.Warning(`Bạn chỉ có quyền hạng trên quận ${quanUser?.name}, phường ${phuong?.name}`)
                return;
            }

            marketMap.current.setLngLat([data?.lng,data?.lat])
            setDanhSachViTri([data?.lng,data?.lat])
            form.setFieldValue('diaChi', data?.diaChi)
            setQuan(district)
            setPhuong(ward)
        });
        setLoading(false);
        return () =>  map.current.remove();
    }, [diemDatQuangCao])

    async function reverseGeocoding(lat,lng){
        try {
            const response = await fetch(
                `https://api.mapbox.com/geocoding/v5/mapbox.places/${lng},${lat}.json?access_token=${process.env.REACT_APP_MAP_BOX_KEY}`
            );
            const data = await response.json();
            let features = data.features;
            let diaChi = features[0].place_name; //địa chỉ
            let quan = features[3].text;
            let  phuong = features[1].text;
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
        if(!danhSachViTri || danhSachViTri.length <2)
        {
            MessageBox.Warning("Vui lòng chọn vị trí điểm đặt quảng cáo.");
            return;
        }
        if (_model &&  diemDatQuangCao) {
            console.log("_model",_model)
            _model.danhSachHinhAnh = await uploadImages();
            _model.danhSachViTri = danhSachViTri;            
            _model.quan = quan.postcode;
            _model.phuong = phuong.postcode;
            if(user?.role !== 'CanBoSo'){
                const modelPhieuChinhSua:any = {
                    idDiemDat:diemDatQuangCao.id,
                    capNhatDiemQuangCao: _model
                }            
                phieuChinhSuaAPI.TaoMoi(modelPhieuChinhSua).then(()=>{
                    navigate(-1)
                });
                return;
            }else{
                diemDatQuangCaoAPI
                .CapNhat(diemDatQuangCao.id,_model).then(()=>{
                    navigate(-1)
                });
            }

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
            <PageContainer title={user?.role === "CanBoSo" ? "Cập nhật điểm đặt quảng cáo" : "Cấp phép chỉnh sửa điểm đặt quảng cáo"}>
            <Spin spinning={loading}>
                <Space className='space-button-on-top'>
                    <Button danger onClick={()=> navigate(-1)}><b>Thoát</b></Button>
                    <Button type='primary' onClick={()=>form.submit()}><b>Gữi phiếu chỉnh sửa</b></Button>
                </Space>
                <Form layout='vertical'             
                    form={form}
                    onFinish={onSubmit}>
                    <Row gutter={[10,10]}>
                        <Col span={14}>
                            <Card title={<b>Thông tin điểm đặt quảng cáo</b>} bordered={false}>
                                <Form.Item label={"Loại vị trí"} name={'idLoaiViTri'} rules={[{ required: true ,message: }]}>
                                    <Select placeholder="Vui lòng chọn loại vị trí" >
                                        {loaiViTris && loaiViTris.map((option) => (
                                            <Select.Option key={option.id} value={option.id}>{option.ma} - {option.ten}</Select.Option>
                                        ))}
                                    </Select>
                                </Form.Item>
                                <Form.Item label={"Hình thức quảng cáo"} name={'idHinhThucQuangCao'} rules={[{ required: true ,message:"Thông tin bắt buộc."}]}>
                                    <Select placeholder="Vui lòng chọn hình thức quảng cáo" >
                                        {hinhThucQuangCaos && hinhThucQuangCaos.map((option) => (
                                            <Select.Option key={option.id} value={option.id}>{option.ma} - {option.ten}</Select.Option>
                                        ))}
                                    </Select>
                                </Form.Item>
                                <Form.Item label={"Quận"}>
                                    <Select placeholder="Vui lòng chọn quận" value={quan?.postcode || undefined} 
                                        onChange={(value)=>{
                                            const newQuan : any = getDistrictWithCode(value);
                                            setQuan(newQuan)
                                            setPhuong(undefined)
                                            form.setFieldValue('diaChi',undefined);
                                            marketMap.current.setLngLat([0,0]);
                                            if(map.current)
                                            {
                                                map.current.flyTo({
                                                    center: newQuan?.center || [106.707222, 10.752444]
                                                })
                                            }
                                        }}
                                        disabled={user?.role !== "CanBoSo"}>
                                            {dataHCM[0].districts.map((option) => (
                                                <Select.Option key={option.postcode} value={option.postcode}>Quận {option.name}</Select.Option>
                                            ))}
                                    </Select>
                                </Form.Item>
                                <Form.Item label={"Phường"}>
                                    <Select placeholder="Vui lòng chọn phường" 
                                            onChange={(value)=>{
                                                setPhuong(getWardByDistrictWithCode(quan.postcode,value))
                                                form.setFieldValue('diaChi',undefined);
                                                marketMap.current.setLngLat([0,0]);
                                            }}
                                            value={phuong?.postcode || undefined}
                                            disabled={user?.role === "CanBoPhuong"}>
                                            {quan && quan.ward.map((option) => {
                                                return (
                                                    <Select.Option key={option.postcode} value={option.postcode}>Phường {option.name}</Select.Option>
                                                )
                                            })}
                                    </Select>
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
