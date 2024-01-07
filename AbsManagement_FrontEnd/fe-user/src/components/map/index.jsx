import React, {useEffect,useRef} from 'react'
import mapboxgl from 'mapbox-gl'
import { CloseCircleOutlined} from '@ant-design/icons'
import MapboxGeocoder from "@mapbox/mapbox-gl-geocoder"
import pointInfo from '../points/pointInfo'
import PropTypes from 'prop-types'
import { Layout, Divider, Button, Affix, Radio, Checkbox} from 'antd';
import { useState } from 'react';
import SpaceInfo from '../points/spaceInfo'
import {ReportToGeoJson, SpaceToGeoJson} from '../../utils/spaceToGeoJson'
import { renderModal } from '../../utils/render-modal'
import ModalCreateReport from '../modal/createReport'
import axios from 'axios'
import './style.scss'
import CardPoint from '../card/point'
import CardSurface from '../card/surface'
import { cluterLayersReport, unclusteredLabelLayerReport, unclusteredLayerReport, LayerSpacePanned, LayerSpacePannedLabel, LayerSpacePannedPoint, LayerSpaceNotPanned, LayerSpaceNotPannedLabel, LayerSpaceNotPannedPoint } from './layers'
import ReportInfo from '../points/reportInfo'
import ModalDetailReport from '../modal/detailReport'

const { Content, Sider } = Layout;

export const tinhTrangReports = {
    "ChuaXuLy": "Chưa xử lý",
    "DangXuLy": "Đang xử lý",
    "DaXuLy": "Đã xử lý"
}

export default function Map({spaces,reports}) {
    const mapContainerRef = useRef(null);
    const map = useRef(null);
    const [collapsed, setCollapsed] = useState(true);
    const [location,setLocation] = useState();
    const [surfaces,setSurfaces] = useState([]); // bảng quảng cáo
    const [email,setEmail] = useState([]);

    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY;

    function checkedSpacePanned(e){
        map.current.setLayoutProperty('space-panned-point','visibility', e.target.checked ? 'visible' : 'none');
        map.current.setLayoutProperty('space-panned-label','visibility', e.target.checked ? 'visible' : 'none');
        map.current.setLayoutProperty('space-panned','visibility', e.target.checked ? 'visible' : 'none');
    }

    function checkedSpaceNotPanned(e){
        map.current.setLayoutProperty('space-not-panned-point','visibility', e.target.checked ? 'visible' : 'none');
        map.current.setLayoutProperty('space-not-panned-label','visibility', e.target.checked ? 'visible' : 'none');
        map.current.setLayoutProperty('space-not-panned','visibility', e.target.checked ? 'visible' : 'none');
    }

    function checkedReport(e){
        map.current.setLayoutProperty('clusters-report','visibility', e.target.checked ? 'visible' : 'none');
        map.current.setLayoutProperty('unclustered-report-point','visibility', e.target.checked ? 'visible' : 'none');
        map.current.setLayoutProperty('unclustered-report-point-label','visibility', e.target.checked ? 'visible' : 'none');
    }

    useEffect(()=>{
        // if(!mapContainerRef.current){return}
        map.current = new mapboxgl.Map({
            container: mapContainerRef.current,
            language:'vi',
            style: "mapbox://styles/mapbox/streets-v11",
            center: [106.707222, 10.752444], // Ho Chi Minh City
            zoom: 12,
        });
        addControls();

        onLoadMap();

        let lng;
        let lat;
        //Onclick space
        map.current.on('click', ['space-panned-point','space-not-panned-point'], async (e) => {

            const featuresLayerReport = map.current.queryRenderedFeatures(e.point, {
                layers: ['clusters-report','unclustered-report-point', 'unclustered-report-point-label']
            });

            if(featuresLayerReport.length > 0)
            {
                return;
            }

            const coordinates = e.features[0].geometry.coordinates.slice();
            const description = e.features[0].properties;

            await getSurfaceBySpace(description.id)
            
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }

            const space = spaces.find(t=>t.id === description.id);
            setLocation({
                lng: coordinates[0],
                lat: coordinates[1],
                ...space
            })
            
            setCollapsed(false)
            new mapboxgl.Popup()
            .setLngLat(coordinates)
            .setHTML(SpaceInfo(space))
            .addTo(map.current);
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
        //End

        //Onclick report
        map.current.on('click', 'unclustered-report-point', async (e) => {
            const coordinates = e.features[0].geometry.coordinates.slice();
            const description = e.features[0].properties;

            // await getSurfaceBySpace(description.id)

            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }
            const report = reports.find(t=>t.id === description.id);

            const popup = new mapboxgl.Popup()
            .setLngLat(coordinates)
            .setHTML(ReportInfo(report))
            .addTo(map.current);

            document.querySelector("#report-popup")?.addEventListener('click',(e)=>{
                onDetailReport(report)
                popup.remove();
            })
        });

        map.current.on('click', 'clusters-report-space', (e) => {
            const features = map.current.queryRenderedFeatures(e.point, {
                layers: ['clusters-report-space']
            });
            if(features.length > 0){
                const clusterId = features[0].properties.cluster_id;
                map.current.getSource('reports').getClusterExpansionZoom(
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
        //End

        //onclick mapp
        map.current.on("click", async (event) => {
            const featuresLayerClusters = map.current.queryRenderedFeatures(event.point, {
                layers: ['space-panned','space-not-panned', 'space-panned-point', 'space-not-panned-point','space-panned-label', 'space-not-panned-label',
                'clusters-report','unclustered-report-point', 'unclustered-report-point-label']
            });

            if(featuresLayerClusters.length > 0)
            {
                return;
            }

            lng = event.lngLat.lng.toFixed(6);
            lat = event.lngLat.lat.toFixed(6);
            const data = await reverseGeocoding(lat, lng);
            setLocation(data)
            setSurfaces([])
            setCollapsed(false)
            const point = pointInfo(data.diaChi);
            new mapboxgl.Popup({
                closeOnClick: true,
                closeButton: true,
            })
                .setLngLat(event.lngLat)
                .setHTML(point)
                .addTo(map.current);
        });
        // Clean up on unmount
        return () => map.current.remove();
    },[])
    

    function onLoadMap(){
        map.current.on("load", () => {
            
            //Handle point space panneds
            map.current.addSource("spacePanneds", {
                type: "geojson",
                data: SpaceToGeoJson(spaces,"DaQuyHoach"),
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
                data: SpaceToGeoJson(spaces,null),
                cluster: true,
                clusterMaxZoom: 14,
                clusterRadius: 50,
            });

            map.current.addLayer(LayerSpaceNotPanned);
    
            map.current.addLayer(LayerSpaceNotPannedPoint);
            
            map.current.addLayer(LayerSpaceNotPannedLabel);

            //End hanlde point space

            //Handle point report
            map.current.addSource("reports", {
                type: "geojson",
                data: ReportToGeoJson(spaces,reports),
                cluster: true,
                clusterMaxZoom: 14,
                clusterRadius: 50,
            });

            map.current.addLayer(cluterLayersReport);
    
            map.current.addLayer(unclusteredLayerReport);

            map.current.addLayer(unclusteredLabelLayerReport);
        });

        map.current.on('mouseenter', ['space-panned-point','space-not-panned-point', 'unclustered-report-point'], () => {
            map.current.getCanvas().style.cursor = 'pointer';
            });

        map.current.on('mouseleave', ['space-panned-point','space-not-panned-point', 'unclustered-report-point'], () => {
                map.current.getCanvas().style.cursor = '';
        });
    }

    function addControls(){
        //Add control
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
            marker: true, // Use the geocoder's default marker style
            bbox: [106.6297, 10.6958, 106.8413, 10.8765], // Set the bounding box coordinates
            placeholder: "Tìm kiếm địa điểm", // Placeholder text for the search bar,
            autocomplete: true,
            language:'vi'
        });
        map.current.addControl(geocoder, "top-left");
    }
    
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

    function getSurfaceBySpace(id){
        try {
            axios.get(`${process.env.REACT_APP_BASE_API}api/bangquangcao/diemdatquangcao/${id}`).then((response) => {
                if(response && response.status === 200)
                {
                    setSurfaces(response.data)
                }
            }).catch((e)=>{
                console.log(e)
            });
        } catch (error) {
        console.log(error);
        }
    }

    function onCreateReportClick(){
        if(location){
            const _root = renderModal(<ModalCreateReport onCancel={() => {
                console.log("cancel")
                _root?.unmount()
            }} location={location}/> );
        }
    }

    function onDetailReport(report){
        if(report){
            const _root = renderModal(<ModalDetailReport onCancel={() => {
                console.log("cancel")
                _root?.unmount()
            }} baoCaoViPham={report}/> );
        }
    }

    return (
        <Layout hasSider>
            <Content style={{height:'100vh', width:'100vw'}}>
            <Affix offsetTop={10} className='affix-antd-button'>
                <div className='affix-container'>
                    <strong>Điểm quảng cáo </strong>
                    <Checkbox className='space-panned' defaultChecked onChange={checkedSpacePanned}>Đã quy hoạch</Checkbox>
                    <Checkbox className='space-not-panned' defaultChecked onChange={checkedSpaceNotPanned}>Chưa quy hoạch</Checkbox>
                    <Checkbox className='space-panned' defaultChecked onChange={checkedReport}><strong>Báo cáo vi phạm </strong></Checkbox>
                    <Checkbox className='space-panned' defaultChecked={false} onChange={checkedReport}><strong>Báo cáo theo email: {email} </strong></Checkbox>
                </div>
            </Affix>
                <div id="map" ref={mapContainerRef}  />
            </Content>
            <Sider theme='light' 
                width={350}  
                style={{padding:'5px 5px 10px 10px', visibility: collapsed ? 'hidden' : 'visible',overflow: 'auto',
                        height: '100vh',
                        position: 'fixed',
                        right: 0,
                        top: 0,
                        bottom: 0,}} 
                collapsedWidth={0} collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
                <Button style={{float:'right', margin:'0 0 10px 5px'}} onClick={()=>setCollapsed(!collapsed)} icon={<CloseCircleOutlined />} danger type='primary'></Button>
                <Divider style={{marginTop:'5px', marginBottom:'5px'}}/> 
                {location && <CardPoint location={location} onCreateReportClick={onCreateReportClick}/> }
                <Divider style={{marginTop:'5px', marginBottom:'5px'}}/> 
                <CardSurface surfaces={surfaces} location={location}/>
            </Sider>
        </Layout>
    )
}
