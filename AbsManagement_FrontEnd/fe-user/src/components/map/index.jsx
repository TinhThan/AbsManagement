import React, {useEffect,useRef} from 'react'
import mapboxgl from 'mapbox-gl'
import {CloseCircleOutlined} from '@ant-design/icons'
import MapboxGeocoder from "@mapbox/mapbox-gl-geocoder"
import ElementReport from '../elementReport'
import { geoJson } from './geoJson'
import PropTypes from 'prop-types'
import { Layout, Card ,Divider,Button} from 'antd';
import { useState } from 'react';
import { Typography } from 'antd';

const { Title } = Typography;

const { Content, Sider } = Layout;


Map.propTypes = {
    setCollapsed: PropTypes.func,
}

export default function Map() {
    const [collapsed, setCollapsed] = useState(true);
    const [location,setLocation] = useState(null);

    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY;
    const mapContainerRef = useRef(null);
    useEffect(() => {
        const map = new mapboxgl.Map({
            container: mapContainerRef.current,
            style: "mapbox://styles/mapbox/streets-v11",
            center: [106.707222, 10.752444], // Ho Chi Minh City
            zoom: 12,
        });
        map.on("load", () => {
            map.addSource("points", {
                type: "geojson",
                data: geoJson,
                cluster: true, // Enables clustering
                clusterMaxZoom: 14, // Maximum zoom level at which clustering is enabled
                clusterRadius: 50, // The radius of each cluster when clustering points
            });
        
            map.addLayer({
                id: 'clusters',
                type: 'circle',
                source: 'points',
                filter: ['has', 'point_count'],
                paint: {
                'circle-color': [
                    'step',
                    ['get', 'point_count'],
                    '#51bbd6',
                    100,
                    '#f1f075',
                    750,
                    '#f28cb1'
                ],
                'circle-radius': [
                    'step',
                    ['get', 'point_count'],
                    20,
                    100,
                    30,
                    750,
                    40
                ]
                }
            });
    
            map.addLayer({
                id: "unclustered-point",
                type: "circle",
                source: "points",
                filter: ["!", ["has", "point_count"]], // Filter out clustered points
                paint: {
                "circle-color": "#11b4da",
                "circle-radius": 15,
                "circle-stroke-width": 1,
                "circle-stroke-color": "#fff",
                },
            });
            map.addLayer({
                id: 'unclustered-point-label',
                type: 'symbol',
                source: 'points',
                filter: ['!', ['has', 'point_count']], // Use the same filter as your circle layer
                layout: {
                    'text-field': 'QC', // This is the text that will be displayed
                    'text-font': ['Open Sans Semibold', 'Arial Unicode MS Bold'], // Set the text font
                    'text-size': 12 // Set the text size
                },
                paint: {
                    'text-color': '#ffffff' // Set the text color
                }
            });
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
                marker: true, // Use the geocoder's default marker style
                bbox: [106.6297, 10.6958, 106.8413, 10.8765], // Set the bounding box coordinates
                placeholder: "Tìm kiếm địa điểm", // Placeholder text for the search bar,
                autocomplete: true,
            });
            let lng;
            let lat;
            map.addControl(geocoder, "top-left");

            map.on("click", async (event) => {
                lng = event.lngLat.lng.toFixed(6);
                lat = event.lngLat.lat.toFixed(6);
                const data = await reverseGeocoding(lat, lng);
                let features = data.features;
                let address = features[0].text; // địa danh: "Bách hoá xanh, chợ, trường, công ty,..."
                let  ward = features[1].text;
                let district = features[3].text;
                let city = features[4].text;
                const full_address = `${address}, ${ward}, ${district}, ${city}`;
                const elementReport = ElementReport(address, full_address);
                setLocation({
                    address: address,
                    full_address: full_address,
                    ward: ward,
                    district: district,
                    city: city,
                })
                new mapboxgl.Popup({
                    closeOnClick: true,
                    closeButton: false,
                })
                    .setLngLat(event.lngLat)
                    .setHTML(elementReport)
                    .addTo(map);
                setCollapsed(false);
            });
    //         Clean up on unmount
            return () => map.remove();
    }, []);

    async function reverseGeocoding(lat,long){
        try {
            const response = await fetch(
                `https://api.mapbox.com/geocoding/v5/mapbox.places/${long},${lat}.json?access_token=${process.env.REACT_APP_MAP_BOX_KEY}`
            );
            const data = await response.json();
            console.log(data)
            return data;
        } catch (error) {
        console.log(error);
        }
    }

    return (
        <Layout style={{height:'100vh', width:'100vw'}}>
            <Content>
                <div id="map" ref={mapContainerRef}  />
            </Content>
            <Sider theme='light' width={350}  style={{padding:'10px'}} collapsedWidth={0} collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
                <Button style={{float:'right', margin:'0 0 10px 5px'}} onClick={()=>setCollapsed(!collapsed)} icon={<CloseCircleOutlined />} danger type='primary'></Button>
                <Divider/>
                {location && <Card title="Thông tin địa điểm" bordered={true}>
                    <p>{location.address}</p>
                    <p>{location.full_address}</p>
                </Card>}
            </Sider>
        </Layout>
    )
}
