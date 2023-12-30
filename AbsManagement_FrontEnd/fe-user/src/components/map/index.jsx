import React, {useEffect,useRef} from 'react'
import mapboxgl from 'mapbox-gl'
import { CloseCircleOutlined} from '@ant-design/icons'
import MapboxGeocoder from "@mapbox/mapbox-gl-geocoder"
import pointInfo from '../points/pointInfo'
import PropTypes from 'prop-types'
import { Layout, Divider, Button} from 'antd';
import { useState } from 'react';
import SpaceInfo from '../points/spaceInfo'
import {ReportToGeoJson, SpaceToGeoJson} from '../../utils/spaceToGeoJson'
import { renderModal } from '../../utils/render-modal'
import ModalCreateReport from '../modal/createReport'
import axios from 'axios'
import './style.scss'
import CardPoint from '../card/point'
import CardSurface from '../card/surface'
import { cluterLayersSpace, unclusteredLabelLayerSpace, unclusteredLayerSpace,
        cluterLayersReport, unclusteredLabelLayerReport, unclusteredLayerReport } from './layers'
import ReportInfo from '../points/reportInfo'

const { Content, Sider } = Layout;


Map.propTypes = {
    setCollapsed: PropTypes.func,
}

export default function Map() {
    const [collapsed, setCollapsed] = useState(true);
    const [location,setLocation] = useState();
    const [spaces,setSpaces] = useState([]); // điểm đặt quảng cáo
    const [surfaces,setSurfaces] = useState([]); // bảng quảng cáo
    const [reports,setReports] = useState([]);

    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY;

    const mapContainerRef = useRef(null);

    useEffect(()=>{
        getSpaces();
        getReports();
    },[])

    useEffect(() => {
        const map = new mapboxgl.Map({
            container: mapContainerRef.current,
            language:'vi',
            style: "mapbox://styles/mapbox/streets-v11",
            center: [106.707222, 10.752444], // Ho Chi Minh City
            zoom: 12,
        });
        map.on("load", () => {
            
            //Handle point space
            map.addSource("points", {
                type: "geojson",
                data: SpaceToGeoJson(spaces),
                cluster: true,
                clusterMaxZoom: 14,
                clusterRadius: 50,
            });

            map.addLayer(cluterLayersSpace);
    
            map.addLayer(unclusteredLayerSpace);

            map.addLayer(unclusteredLabelLayerSpace);

            map.on('mouseenter', 'unclustered-space-point', () => {
                map.getCanvas().style.cursor = 'pointer';
                });

            map.on('mouseleave', 'unclustered-space-point', () => {
                map.getCanvas().style.cursor = '';
            });

            //End hanlde point space

            //Handle point report
            map.addSource("reports", {
                type: "geojson",
                data: ReportToGeoJson(reports),
                cluster: true,
                clusterMaxZoom: 14,
                clusterRadius: 50,
            });

            map.addLayer(cluterLayersReport);
    
            map.addLayer(unclusteredLayerReport);

            map.addLayer(unclusteredLabelLayerReport);

            map.on('mouseenter', 'unclustered-report-point', () => {
                map.getCanvas().style.cursor = 'pointer';
                });

            map.on('mouseleave', 'unclustered-report-point', () => {
            map.getCanvas().style.cursor = '';
            });
            //End handle report
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
                layers: ['clusters-space','unclustered-space-point','clusters-report','unclustered-report-point']
            });

            if(featuresLayerClusters.length > 0)
            {
                console.log("featuresLayerClusters",featuresLayerClusters)
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
                closeButton: false,
            })
                .setLngLat(event.lngLat)
                .setHTML(point)
                .addTo(map);
        });

        //Onclick space
        map.on('click', 'unclustered-space-point', async (e) => {
            const coordinates = e.features[0].geometry.coordinates.slice();
            const description = e.features[0].properties;

            await getSurfaceBySpace(description.id)
            
            // Ensure that if the map is zoomed out such that multiple
            // copies of the feature are visible, the popup appears
            // over the copy being pointed to.
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }
            const space = spaces.find(t=>t.id === description.id);
            setLocation({
                lng: coordinates[0],
                lat: coordinates[1],
                ...space
            })
            console.log("description",description)
            setCollapsed(false)
            new mapboxgl.Popup()
            .setLngLat(coordinates)
            .setHTML(SpaceInfo(space))
            .addTo(map);
        });

        map.on('click', 'clusters-space', (e) => {
            const features = map.queryRenderedFeatures(e.point, {
            layers: ['clusters-space']
            });
            const clusterId = features[0].properties.cluster_id;
            map.getSource('points').getClusterExpansionZoom(
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
        //End

        //Onclick report
        map.on('click', 'unclustered-report-point', async (e) => {
            const coordinates = e.features[0].geometry.coordinates.slice();
            const description = e.features[0].properties;

            // await getSurfaceBySpace(description.id)
            
            // Ensure that if the map is zoomed out such that multiple
            // copies of the feature are visible, the popup appears
            // over the copy being pointed to.
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }
            const report = reports.find(t=>t.id === description.id);
            setLocation({
                lng: coordinates[0],
                lat: coordinates[1],
                ...report
            })
            console.log("description",description)
            setCollapsed(false)
            new mapboxgl.Popup()
            .setLngLat(coordinates)
            .setHTML(ReportInfo(report))
            .addTo(map);
        });

        map.on('click', 'clusters-report-space', (e) => {
            const features = map.queryRenderedFeatures(e.point, {
            layers: ['clusters-report-space']
            });
            const clusterId = features[0].properties.cluster_id;
            map.getSource('points').getClusterExpansionZoom(
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
        //End

        // Clean up on unmount
        return () => map.remove();
    }, [spaces,reports]);

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

    async function getSpaces(){
        try {
            await axios.get(`${process.env.REACT_APP_BASE_API}api/diemdatquangcao`).then((response) => {
                console.log("resone",response)
                if(response && response.status === 200)
                {
                    setSpaces(response.data)
                }
            }).catch((e)=>{
                console.log("error1",e)
            });
        } catch (error) {
            console.log("error2",error);
        }
    }
    
    async function getReports(){
        try {
            await axios.get(`${process.env.REACT_APP_BASE_API}api/baocaovipham`).then((response) => {
                if(response && response.status === 200)
                {
                    setReports(response.data)
                }
            }).catch((e)=>{
                console.log(e)
            });
        } catch (error) {
        console.log(error);
        }
    }

    async function getSurfaceBySpace(id){
        try {
            await axios.get(`${process.env.REACT_APP_BASE_API}api/bangquangcao/diemdatquangcao/${id}`).then((response) => {
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

    return (
        <Layout hasSider>
            <Content style={{height:'100vh', width:'100vw'}}>
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
