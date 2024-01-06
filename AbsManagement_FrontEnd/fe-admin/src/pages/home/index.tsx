import React, {Suspense, useEffect,useRef} from 'react'
import { Layout, Spin} from 'antd';
import { useState } from 'react';
import { DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel'
import { BangQuangCaoModel } from '../../apis/bangQuangCao/bangQuangCaoModel'
import { BaoCaoViPhamModel } from '../../apis/baoCaoViPham/baoCaoViPhamModel'
import { baoCaoViPhamAPI } from '../../apis/baoCaoViPham'
import { UserStorage } from '../../apis/auth/user'
import UserInfoStorage from '../../storages/user-info'
import { diemDatQuangCaoAPI } from '../../apis/diemDatQuangCao'
import { PageContainer, PageLoading } from '@ant-design/pro-components'
import MapComponent from '../../components/map'
import { getDistrictWithCode, getWardByDistrictWithCode } from '../../utils/getWard';
import { redirect } from 'react-router-dom';

export default function HomeFeature() {
    const [loading,setLoading] = useState(false)
    const [user,setUser] = useState<UserStorage>();
    const [spaces,setSpaces] = useState<DiemDatQuangCaoModel[]>([]); // điểm đặt quảng cáo đã quy hoạch
    const [reports,setReports] = useState<BaoCaoViPhamModel[]>([]);
    const [phuong, setPhuong] = useState<any>();
    const [quan, setQuan] = useState<any>();

    useEffect(() => {
        const useInfo = UserInfoStorage.get();
        if(!useInfo)
        {
            redirect('/login');
            return;
        }
        let quanUser;
        getSpaces(useInfo);
        getReports(useInfo);
        setUser(useInfo);
        if(useInfo.role === "CanBoQuan")
        {
            quanUser = getDistrictWithCode(useInfo?.noiCongTac[0] || '');
            setQuan(quanUser);
        }

        if(useInfo.role === "CanBoPhuong")
        {
            quanUser = getDistrictWithCode(useInfo.noiCongTac[0]);
            const phuongUser = getWardByDistrictWithCode(quanUser.postcode, useInfo?.noiCongTac[1]);
            setQuan(quanUser);
            setPhuong(phuongUser);
        }
    },[]);
    
    function getSpaces(userInfo:UserStorage){
        diemDatQuangCaoAPI
        .DanhSach(userInfo?.noiCongTac[0] || '', userInfo?.noiCongTac[1] || '')
        .then((response) => {
            if (response && response.status === 200) {
                const newData = response.data.map((item: any) => {
                    return {
                        ...item,
                        key: item.id
                    }
                })
                setSpaces(newData || []);
            }
        });
    }
    
    function getReports(userInfo:UserStorage){
        baoCaoViPhamAPI
        .DanhSach(userInfo?.noiCongTac[0] || '', userInfo?.noiCongTac[1] || '')
        .then((response) => {
            if (response && response.status === 200) {
                const newData = response.data.map((item: any) => {
                    return {
                        ...item,
                        key: item.id
                    }
                })
                setReports(newData || []);
            }
        });
    }

    return (
        <Suspense fallback={<PageLoading/>}>
            <Spin spinning={loading}>
            <PageContainer className='map-home'>
               {spaces && spaces.length > 0 && <MapComponent spaces={spaces} reports={reports} phuongUser={phuong} quanUser={quan}/>}
            </PageContainer>
        </Spin>
        </Suspense>
    )
}
