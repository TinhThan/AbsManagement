import React, {useEffect,useRef} from 'react'
import mapboxgl from 'mapbox-gl'
import { CloseCircleOutlined} from '@ant-design/icons'
import MapboxGeocoder from "@mapbox/mapbox-gl-geocoder"
import PropTypes from 'prop-types'
import { Layout, Divider, Button} from 'antd';
import { useState } from 'react';
import axios from 'axios'
import './style.scss'
import { cluterLayers, unclusteredLabelLayer, unclusteredLayer } from './layers'
import { DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel'
import { BangQuangCaoModel } from '../../apis/bangQuangCao/bangQuangCaoModel'
import { SpaceToGeoJson } from '../../utils/anyToGeoJson'
import pointInfo from '../point/pointInfo'
import SpaceInfo from '../point/spaceInfo'

const { Content, Sider } = Layout;


Map.propTypes = {
    setCollapsed: PropTypes.func,
}

export interface Location{
    diaChi: string,
    phuong: string,
    quan: string,
    lng: number,
    lat: number
}

export default function Map() {
    const [collapsed, setCollapsed] = useState(true);
    const [location,setLocation] = useState<Location>();
    const [spaces,setSpaces] = useState<DiemDatQuangCaoModel[]>([]); // điểm đặt quảng cáo
    const [surfaces,setSurfaces] = useState<BangQuangCaoModel[]>([]); // bảng quảng cáo

    mapboxgl.accessToken = process.env.REACT_APP_MAP_BOX_KEY || '';

    const mapContainerRef = useRef(null);

    useEffect(()=>{
        getSpaces();
    },[])

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
                data: SpaceToGeoJson(spaces,'DaQuyHoach') || undefined,
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
            setLocation(data)
            setSurfaces([])
            setCollapsed(false)
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
            const description = e.features[0].properties;

            await getSurfaceBySpace(description.id)
            
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }
            const space = spaces.find(t=>t.id === description.id);
            if(space){
                console.log("space",space)
                setLocation({
                    lng: coordinates[0],
                    lat: coordinates[1],
                    diaChi: space.diaChi,
                    phuong: space.phuong,
                    quan: space.quan
                })
                console.log("description",description)
                setCollapsed(false)
                new mapboxgl.Popup()
                .setLngLat(coordinates)
                .setHTML(SpaceInfo(space))
                .addTo(map);
            }
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
    }, [spaces]);

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
                if(response && response.status === 200)
                {
                    setSpaces(response.data)
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

    // function onCreateReportClick(){
    //     if(location){
    //         const _root = renderModal(<ModalCreateReport onCancel={() => {
    //             console.log("cancel")
    //             _root?.unmount()
    //         }} location={location}/> );
    //     }
    // }

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
                {/* {location && <CardPoint location={location} onCreateReportClick={onCreateReportClick}/> }
                <Divider style={{marginTop:'5px', marginBottom:'5px'}}/> 
                <CardSurface surfaces={surfaces} location={location}/> */}
            </Sider>
        </Layout>
    )
}
