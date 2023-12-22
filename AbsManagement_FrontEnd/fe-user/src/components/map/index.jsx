import React, {useEffect,useRef} from 'react'
import mapboxgl from 'mapbox-gl'
import { CloseCircleOutlined, ExclamationCircleFilled} from '@ant-design/icons'
import MapboxGeocoder from "@mapbox/mapbox-gl-geocoder"
import pointInfo from '../points/pointInfo'
import PropTypes from 'prop-types'
import { Layout, Card ,Divider,Button,Typography} from 'antd';
import { useState } from 'react';
import SpaceInfo from '../points/spaceInfo'
import SpaceToGeoJson from '../../utils/spaceToGeoJson'
import { renderModal } from '../../utils/render-modal'
import ModalCreateReport from '../modal/createReport'
import './style.scss'

const { Content, Sider } = Layout;
const { Text } = Typography; 


Map.propTypes = {
    setCollapsed: PropTypes.func,
}

const spaceExamples = [
    {
        diaChi:"Đường nguyễn văn cừ",
        phuong:"Phường 10",
        quan:"Quận 5",
        viTri:[106.682462, 10.764324],
        tenLoaiViTri:"Vị trí ở Now zone",
        tenHinhThucQuangCao:"Mới thêm"
    },
    {
        diaChi:"PNJ",
        phuong:"Phường PNJ",
        quan:"Quận PNV",
        viTri:[106.682806, 10.763993],
        tenLoaiViTri:"Vị trí ở PNJ",
        tenHinhThucQuangCao:"Mới thêm PNJ"
    },
    {
        diaChi:"Trường trung học phổ thông chuyên Lê Hồng Phong",
        phuong:"Phường Trường trung học phổ thông chuyên Lê Hồng Phong",
        quan:"Quận Trường trung học phổ thông chuyên Lê Hồng Phong",
        viTri:[106.681473, 10.763485],
        tenLoaiViTri:"Vị trí ở Trường trung học phổ thông chuyên Lê Hồng Phong",
        tenHinhThucQuangCao:"Mới thêm Trường trung học phổ thông chuyên Lê Hồng Phong"
    },
    {
        diaChi:"Trường đại học khoa học tự nhiên",
        phuong:"Phường Trường đại học khoa học tự nhiên",
        quan:"Quận Trường đại học khoa học tự nhiên",
        viTri:[106.682475, 10.762828],
        tenLoaiViTri:"Vị trí ở Trường đại học khoa học tự nhiên",
        tenHinhThucQuangCao:"Mới thêm Trường đại học khoa học tự nhiên"
    }
]

export default function Map() {
    const [collapsed, setCollapsed] = useState(false);
    const [location,setLocation] = useState({
        address: 'Địa điểm',
        full_address: 'full thông tin địa điểm'
    });
    const [spaces,setSpaces] = useState(spaceExamples);
    const [surfaces,setSurfaces] = useState([]);

    // mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY;
    // const mapContainerRef = useRef(null);
    // useEffect(() => {
    //     const map = new mapboxgl.Map({
    //         container: mapContainerRef.current,
    //         style: "mapbox://styles/mapbox/streets-v11",
    //         center: [106.707222, 10.752444], // Ho Chi Minh City
    //         zoom: 12,
    //     });
    //     map.on("load", () => {
    //         map.addSource("points", {
    //             type: "geojson",
    //             data: SpaceToGeoJson(spaces),
    //             cluster: true, // Enables clustering
    //             clusterMaxZoom: 14, // Maximum zoom level at which clustering is enabled
    //             clusterRadius: 50, // The radius of each cluster when clustering points
    //         });

    //         map.addLayer({
    //             id: 'clusters',
    //             type: 'circle',
    //             source: 'points',
    //             filter: ['has', 'point_count'],
    //             paint: {
    //                 'circle-color': [
    //                     'step',
    //                     ['get', 'point_count'],
    //                     '#51bbd6',
    //                     100,
    //                     '#f1f075',
    //                     750,
    //                     '#f28cb1'
    //                 ],
    //                 'circle-radius': [
    //                     'step',
    //                     ['get', 'point_count'],
    //                     20,
    //                     100,
    //                     30,
    //                     750,
    //                     40
    //                 ]
    //             }
    //         });
    
    //         map.addLayer({
    //             id: "unclustered-point",
    //             type: "circle",
    //             source: "points",
    //             filter: ["!", ["has", "point_count"]], // Filter out clustered points
    //             paint: {
    //                 "circle-color": "#11b4da",
    //                 "circle-radius": 15,
    //                 "circle-stroke-width": 1,
    //                 "circle-stroke-color": "#fff",
    //             },
    //         });

    //         map.addLayer({
    //             id: 'unclustered-point-label',
    //             type: 'symbol',
    //             source: "points",
    //             filter: ['!', ['has', 'point_count']], // Use the same filter as your circle layer
    //             layout: {
    //                 'text-field': 'QC', // This is the text that will be displayed
    //                 'text-font': ['Open Sans Semibold', 'Arial Unicode MS Bold'], // Set the text font
    //                 'text-size': 12 // Set the text size
    //             },
    //             paint: {
    //                 'text-color': '#ffffff' // Set the text color
    //             }
    //             });

    //         map.on('mouseenter', 'unclustered-point', () => {
    //             map.getCanvas().style.cursor = 'pointer';
    //             });
                    
    //         // Change it back to a pointer when it leaves.
    //         map.on('mouseleave', 'unclustered-point', () => {
    //         map.getCanvas().style.cursor = '';
    //         });
    //     });
        
    //     //Add control
    //     map.addControl(
    //         new mapboxgl.GeolocateControl({
    //             positionOptions: {
    //             enableHighAccuracy: true,
    //             },
    //             trackUserLocation: true,
    //         }),
    //         "bottom-right"
    //     );
    
    //     const geocoder = new MapboxGeocoder({
    //         accessToken: mapboxgl.accessToken, // Set the access token
    //         mapboxgl: mapboxgl, // Set the mapbox-gl instance
    //         marker: true, // Use the geocoder's default marker style
    //         bbox: [106.6297, 10.6958, 106.8413, 10.8765], // Set the bounding box coordinates
    //         placeholder: "Tìm kiếm địa điểm", // Placeholder text for the search bar,
    //         autocomplete: true,
    //     });
    //     let lng;
    //     let lat;
    //     map.addControl(geocoder, "top-left");

    //     //onclick mapp
    //     map.on("click", async (event) => {
    //         console.log("Click map", event)
    //         const featuresLayer = map.queryRenderedFeatures(event.point, {
    //             layers: ['clusters','unclustered-point']
    //         });
    //         lng = event.lngLat.lng.toFixed(6);
    //         lat = event.lngLat.lat.toFixed(6);
    //         const data = await reverseGeocoding(lat, lng);
    //         let features = data.features;
    //         let address = features[0].text; // địa danh: "Bách hoá xanh, chợ, trường, công ty,..."
    //         let  ward = features[1].text;
    //         let district = features[3].text;
    //         let city = features[4].text;
    //         const full_address = `${address}, ${ward}, ${district}, ${city}`;
    //         setLocation({
    //             address: address,
    //             full_address: full_address,
    //             ward: ward,
    //             district: district,
    //             city: city,
    //         })
    //         setCollapsed(false);
    //         if(!featuresLayer){
    //             const elementReport = pointInfo(address, full_address);
    //             new mapboxgl.Popup({
    //                 closeOnClick: true,
    //                 closeButton: false,
    //             })
    //                 .setLngLat(event.lngLat)
    //                 .setHTML(elementReport)
    //                 .addTo(map);
    //         }
    //     });

    //     // When a click event occurs on a feature in the places layer, open a popup at the
    //     // location of the feature, with description HTML from its properties.
    //     map.on('click', 'unclustered-point', (e) => {
    //         // Copy coordinates array.
    //         console.log("Click unclustered-point",e.features)
    //         const coordinates = e.features[0].geometry.coordinates.slice();
    //         const description = e.features[0].properties;
            
    //         // Ensure that if the map is zoomed out such that multiple
    //         // copies of the feature are visible, the popup appears
    //         // over the copy being pointed to.
    //         while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
    //         coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
    //         }
            
    //         new mapboxgl.Popup()
    //         .setLngLat(coordinates)
    //         .setHTML(SpaceInfo(description))
    //         .addTo(map);
    //     });

        
    //             // inspect a cluster on click
    //     map.on('click', 'clusters', (e) => {
    //         const features = map.queryRenderedFeatures(e.point, {
    //         layers: ['clusters']
    //         });
    //         const clusterId = features[0].properties.cluster_id;
    //         map.getSource('points').getClusterExpansionZoom(
    //             clusterId,
    //             (err, zoom) => {
    //             if (err) return;
                
    //             map.easeTo({
    //             center: features[0].geometry.coordinates,
    //             zoom: zoom
    //             });
    //             }
    //         );
    //     });

    //     // Clean up on unmount
    //     return () => map.remove();
    // }, [spaces]);

    // async function reverseGeocoding(lat,long){
    //     try {
    //         const response = await fetch(
    //             `https://api.mapbox.com/geocoding/v5/mapbox.places/${long},${lat}.json?access_token=${process.env.REACT_APP_MAP_BOX_KEY}`
    //         );
    //         const data = await response.json();
    //         console.log(data)
    //         return data;
    //     } catch (error) {
    //     console.log(error);
    //     }
    // }

    function onCreateReportClick(){
        const _root = renderModal(<ModalCreateReport onCancel={() => {
            _root?.unmount()
        }}/>);
    }

    return (
        <Layout style={{height:'100vh', width:'100vw'}}>
            <Content>
                {/* <div id="map" ref={mapContainerRef}  /> */}
            </Content>
            <Sider theme='light' width={350}  style={{padding:'5px 5px 10px 10px',visibility:collapsed ? 'hidden' : 'visible'}} collapsedWidth={0} collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
                <Button style={{float:'right', margin:'0 0 10px 5px'}} onClick={()=>setCollapsed(!collapsed)} icon={<CloseCircleOutlined />} danger type='primary'></Button>
                <Divider style={{margin:'10px 10px 10px 0px'}}/> 
                <Card title="Thông tin bảng quảng cáo" bordered={true} className='info-surface'>
                {
                    surfaces ? 
                    <>
                        <Text strong>Chưa có dữ liệu.</Text>
                        <Text>Vui lòng chọn điểm trên bảng đồ để xem.</Text>
                    </> :
                    <>
                        <p>{location.address}</p>
                        <p>{location.full_address}</p>
                        <Button icon={<ExclamationCircleFilled />} danger>Báo cáo vi phạm</Button>
                    </>
                }
                </Card>
                {location &&  <>
                    <Divider style={{margin:'10px 10px 10px 0px'}}/> 
                    <Card title="Thông tin địa điểm" bordered={true} className='info-location'>
                        <p>{location.address}</p>
                        <p>{location.full_address}</p>
                        <Button icon={<ExclamationCircleFilled />} onClick={onCreateReportClick} danger>Báo cáo vi phạm</Button>
                    </Card>
                </>}
            </Sider>
        </Layout>
    )
}
