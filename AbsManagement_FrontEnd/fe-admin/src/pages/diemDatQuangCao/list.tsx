import { Suspense, useState,useEffect } from 'react';
import React from 'react';
import { PageContainer, PageLoading } from '@ant-design/pro-components';
import { Button, Col, Dropdown, Input, Row, Select, Space, Spin, Table, TableColumnType } from 'antd';
import { DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import { DeleteOutlined, EditOutlined, EllipsisOutlined, PlusOutlined } from '@ant-design/icons';
import { getDistrictWithCode, getWardByDistrictWithCode } from '../../utils/getWard';
import { diemDatQuangCaoAPI } from '../../apis/diemDatQuangCao';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';
import { ConfigRoute } from '../../routes/ConfigRoute';
import { useNavigate } from 'react-router-dom';
import dataHCM from '../../assets/new-dataHCM.json';

const { Search } = Input;

export const tinhTrangDiemDatQuangCao = {
    DaQuyHoach: "Đã quy hoạch",
    ChuaQuyHoach: "Chưa quy hoạch",
    ChoDuyet: "Chờ duyệt chỉnh sửa"
}

export default function ListDiemDatQuangCao(): JSX.Element {
    const navigate = useNavigate();
    const [diemDatQuangCaos,setDiemDatQuangCaos] = useState<DiemDatQuangCaoModel[]>([]);
    const [loading,setLoading] = useState(false);
    const [user,setUser] = useState<UserStorage>();
    const [phuong, setPhuong] = useState<any>();
    const [quan, setQuan] = useState<any>();

    useEffect(() => {
        const useInfo = UserInfoStorage.get();
        if(useInfo)
        {
            setUser(useInfo);
        }
        if(useInfo?.role === 'CanBoQuan'){
            setQuan(getDistrictWithCode(useInfo.noiCongTac[0]))
        }
        if(useInfo?.role === 'CanBoPhuong'){
            setQuan(getDistrictWithCode(useInfo.noiCongTac[0]))
            setPhuong(getWardByDistrictWithCode(useInfo.noiCongTac[0],useInfo.noiCongTac[1]))
        }
        getDiemDatQuangCaos(useInfo?.noiCongTac[0] || '',useInfo?.noiCongTac[1] || '');
    }, [])
    

    async function getDiemDatQuangCaos(quan,phuong) {
        setLoading(true)
        diemDatQuangCaoAPI
            .DanhSach(quan,phuong)
            .then((response) => {
                if(response && response.status === 200)
                {
                    setDiemDatQuangCaos(response.data || []);
                }
            });
        setLoading(false)
    }
    
    const columns: TableColumnType<DiemDatQuangCaoModel>[] = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            width: 80,
            fixed: true,
            showSorterTooltip:false
        },
        {
            title: "Địa chỉ",
            width: 400,
            sorter: true,
            dataIndex: 'diaChi',
            key: 'diaChi'
        },
        {
            title: "Phường",
            width: 200,
            sorter: true,
            dataIndex: 'phuong',
            key: 'phuong',            
            render:(value: string,record: DiemDatQuangCaoModel) => {
                return "Phường " + getWardByDistrictWithCode(record.quan,record.phuong).name;
            },
            showSorterTooltip:false
        },
        {
            title: "Quận",
            width: 200,
            sorter: true,
            dataIndex: 'quan',
            key: 'quan',            
            render:(value: string,record: DiemDatQuangCaoModel) => {
                return "Quận " +getDistrictWithCode(record.quan).name;
            },
            showSorterTooltip:false
        },
        {
            title: "Tên loại vị trí",
            width: 200,
            sorter: true,
            dataIndex: 'tenLoaiViTri',
            key: 'tenLoaiViTri',
            showSorterTooltip:false
        },
        {
            title: "Tên hình thức quảng cáo",
            width: 200,
            sorter: true,
            dataIndex: 'tenHinhThucQuangCao',
            key: 'tenHinhThucQuangCao',
            showSorterTooltip:false
        },
        {
            title: "Tình trạng",
            width: 200,
            sorter: true,
            dataIndex: 'idTinhTrang',
            key: 'idTinhTrang',
            showSorterTooltip:false,
            render: (value:string) => {
                return tinhTrangDiemDatQuangCao[value];
            },
        },
        {
            title: "Hành động",
            width: 80,
            key: 'function',
            fixed: 'right',
            render: (row:DiemDatQuangCaoModel) => {
                return (
                <Dropdown
                destroyPopupOnHide
                overlayClassName='drop-down-button'
                menu={{ items: [
                    {
                        label: "Chi tiết",
                        key: "1",
                        icon: <EditOutlined />,
                        onClick: ()=>navigate(`${ConfigRoute.CanBoSo.DiemDatQuangCao}/${row.id}`),
                    },
                    {
                        label: user?.role === 'CanBoSo' ? "Cập nhật" : "Tạo phiếu chỉnh sửa",
                        key: "2",
                        icon: <EditOutlined />,
                        onClick: ()=>navigate(`${ConfigRoute.CanBoSo.DiemDatQuangCao}/capnhat/${row.id}`),
                    }
                ]}}
                trigger={['click']}
                >
                <EllipsisOutlined />
                </Dropdown>
                );
            }
        }
    ];

    return (
        <Suspense fallback={<PageLoading/>}>
            <PageContainer title="Danh sách điểm đặt quảng cáo">
                <Spin spinning={loading}>
                    <Space direction='vertical' size={0} className='layout-basic-page'>
                        <Row wrap={false} gutter={[5,5]} style={{marginBottom:'10px'}}>
                            <Col>
                                <Select placeholder="Lọc theo quận" style={{width:'200px'}} value={quan?.postcode || undefined} 
                                    onChange={(value)=>{
                                        const newQuan : any = getDistrictWithCode(value);
                                        setQuan(newQuan)
                                        setPhuong(undefined)
                                        getDiemDatQuangCaos(newQuan.postcode,'')
                                    }}
                                    disabled={user?.role !== "CanBoSo"}>
                                        {dataHCM[0].districts.map((option) => (
                                            <Select.Option key={option.postcode} value={option.postcode}>Quận {option.name}</Select.Option>
                                        ))}
                                </Select>
                            </Col>
                            <Col>
                                <Select placeholder="Lọc theo phường quận" style={{width:'300px'}}
                                            onChange={(value)=>{
                                                const newPhuong:any = getWardByDistrictWithCode(quan.postcode,value);
                                                setPhuong(newPhuong)
                                                getDiemDatQuangCaos(quan.postcode,newPhuong.postcode)
                                            }}
                                            value={phuong?.postcode || undefined}
                                            disabled={user?.role === "CanBoPhuong"}>
                                            {quan && quan.ward.map((option) => {
                                                return (
                                                    <Select.Option key={option.postcode} value={option.postcode}>Phường {option.name}</Select.Option>
                                                )
                                            })}
                                </Select>
                            </Col>
                        </Row>
                        <Row wrap={false} gutter={[5,5]} style={{marginBottom:'10px'}}>
                            <Col flex='auto'>
                            <Search placeholder="Tìm kiếm..." enterButton="Search" size="large" />
                            </Col>
                            {user?.role === 'CanBoSo' &&  <Col flex='none'>
                                <Button shape='circle' size='large' type='primary' icon={<PlusOutlined />} 
                                onClick={()=>navigate(`${ConfigRoute.CanBoSo.DiemDatQuangCao}/taomoi`)} className={`btn-create`}></Button>
                            </Col>}
                        </Row>
                        <Table columns={columns} dataSource={diemDatQuangCaos} scroll={{ x: 'max-content' }}/>
                    </Space>
                </Spin>
            </PageContainer>
        </Suspense>
    );
}