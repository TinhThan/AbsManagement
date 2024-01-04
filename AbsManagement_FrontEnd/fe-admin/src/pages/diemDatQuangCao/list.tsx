import { Suspense, useState,useEffect } from 'react';
import React from 'react';
import { PageContainer, PageLoading } from '@ant-design/pro-components';
import { Button, Col, Dropdown, Input, Row, Space, Spin, Table, TableColumnType } from 'antd';
import { DiemDatQuangCaoModel, CapNhatDiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';
import { DeleteOutlined, EditOutlined, EllipsisOutlined, KeyOutlined, PlusOutlined } from '@ant-design/icons';
import { renderModal } from '../../utils/render-modal';
// import { ModalDetailDiemDatQuangCao } from './detail';
import { FormatTime, GetDateTimeByFormat } from '../../utils';
// import { ModalUpdateDiemDatQuangCao } from './update';
import dayjs from 'dayjs';
// import { ModalCreateDiemDatQuangCao } from './create';
import { getDistrict, getDistrictByCode, getWardByDistrict } from '../../utils/getWard';
import { diemDatQuangCaoAPI } from '../../apis/diemDatQuangCao';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';
import { ConfigRoute } from '../../routes/ConfigRoute';
import { useNavigate } from 'react-router-dom';

const { Search } = Input;

export const tinhTrangDiemDatQuangCao = {
    DaQuyHoach: "Đã quy hoạch",
    ChuaQuyHoach: "Chưa quy hoạch"
}

export default function ListDiemDatQuangCao(): JSX.Element {
    const navigate = useNavigate();
    const [diemDatQuangCaos,setDiemDatQuangCaos] = useState<DiemDatQuangCaoModel[]>([]);
    const [loading,setLoading] = useState(false);
    const [user,setUser] = useState<UserStorage>();

    function onCreateClick(){
        // const _root = renderModal(<ModalCreateDiemDatQuangCao onCancel={() => {
        //     _root?.unmount()
        //     getDiemDatQuangCaos()
        // }}/>);
    }

    function onDeleteClick(){

    }

    useEffect(() => {
        getDiemDatQuangCaos();
        const useInfo = UserInfoStorage.get();
        if(useInfo)
        {
            setUser(useInfo);
        }
    }, [])
    

    async function getDiemDatQuangCaos() {
        setLoading(true)
        diemDatQuangCaoAPI
            .DanhSach(user?.noiCongTac[0] || '',user?.noiCongTac[1] || '')
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
            width: 250,
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
                return "Phường " + getWardByDistrict(record.quan,record.phuong).name;
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
                return "Quận " +getDistrict(record.quan).name;
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
                        label: "Cập nhật",
                        key: "2",
                        icon: <EditOutlined />,
                        onClick: ()=>navigate(`${ConfigRoute.CanBoSo.DiemDatQuangCao}/capnhat/${row.id}`),
                    },
                    {
                        label: "Xóa",
                        key: "3",
                        icon: <DeleteOutlined />,
                        onClick: onDeleteClick,
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
                        <Col flex='auto'>
                        <Search placeholder="Tìm kiếm..." enterButton="Search" size="large" />
                        </Col>
                        <Col flex='none'>
                            <Button shape='circle' size='large' type='primary' icon={<PlusOutlined />} onClick={onCreateClick} className={`btn-create`}></Button>
                        </Col>
                    </Row>
                    <Table columns={columns} dataSource={diemDatQuangCaos} scroll={{ x: 'max-content' }}/>
                </Space>
            </Spin>
            </PageContainer>
        </Suspense>
    );
}