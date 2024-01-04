import { Suspense, useState,useEffect } from 'react';
import React from 'react';
import { PageLoading } from '@ant-design/pro-components';
import { Button, Col, Dropdown, Input, Row, Space, Spin, Table, TableColumnType } from 'antd';
import { BaoCaoViPhamModel, CapNhatBaoCaoViPhamModel } from '../../apis/baoCaoViPham/baoCaoViPhamModel';
import { EditOutlined, EllipsisOutlined, PlusOutlined } from '@ant-design/icons';
import { getDistrictWithCode, getWardByDistrictWithCode } from '../../utils/getWard';
import { baoCaoViPhamAPI } from '../../apis/baoCaoViPham';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';
import { useNavigate } from 'react-router-dom';
import { ConfigRoute } from '../../routes/ConfigRoute';

const { Search } = Input;

export const tinhTrangBaoCaoViPham = {
    ChuaXuLy: "Chưa xử lý",
    DaXyLy: "Đã xử lý"
}

export default function ListBaoCaoViPham(): JSX.Element {
    const navigate = useNavigate();
    const [baoCaoViPhams,setBaoCaoViPhams] = useState<BaoCaoViPhamModel[]>([]);
    const [loading,setLoading] = useState(false);
    const [user,setUser] = useState<UserStorage>();
    function onDetailClick(model) {
        // const _root = renderModal(<ModalDetailBaoCaoViPham onCancel={() => _root?.unmount()} baoCaoViPham={model} />);
    }

    function onUpdateClick(model: BaoCaoViPhamModel){
        const capNhatBaoCaoViPhamModel: CapNhatBaoCaoViPhamModel = {
            ...model
        };
        // const _root = renderModal(<ModalUpdateBaoCaoViPham onCancel={() => {
        //     _root?.unmount()
        //     getBaoCaoViPhams()
        // }} baoCaoViPham={capNhatBaoCaoViPhamModel} />);
    }

    function onCreateClick(){
        // const _root = renderModal(<ModalCreateBaoCaoViPham onCancel={() => {
        //     _root?.unmount()
        //     getBaoCaoViPhams()
        // }}/>);
    }

    function onDeleteClick(){

    }

    useEffect(() => {
        setLoading(true);
        getBaoCaoViPhams();
        const useInfo = UserInfoStorage.get();
        if(useInfo)
        {
            setUser(useInfo);
        }
        setLoading(false);
    }, [])
    

    async function getBaoCaoViPhams() {
        baoCaoViPhamAPI
            .DanhSach(user?.noiCongTac[0] || '',user?.noiCongTac[1] || '')
            .then((response) => {
                if(response && response.status === 200)
                {
                    setBaoCaoViPhams(response.data || []);
                }
            });
    }
    
    const columns: TableColumnType<BaoCaoViPhamModel>[] = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            width: 80,
            fixed: true,
            showSorterTooltip:false
        },
        {
            title: "Tên người gữi",
            width: 250,
            sorter: true,
            dataIndex: 'hoTen',
            key: 'hoTen'
        },
        {
            title: "Email",
            width: 250,
            sorter: true,
            dataIndex: 'email',
            key: 'email'
        },
        {
            title: "Số điện thoại",
            width: 250,
            sorter: true,
            dataIndex: 'soDienThoai',
            key: 'soDienThoai'
        },
        {
            title: "Tên hình thức quảng cáo",
            width: 250,
            sorter: true,
            dataIndex: 'tenHinhThucBaoCao',
            key: 'tenHinhThucBaoCao'
        },
        {
            title: "Địa chỉ",
            width: 250,
            sorter: true,
            dataIndex: 'diaChi',
            key: 'diaChi',
            ellipsis: true,
            showSorterTooltip:false
        },
        {
            title: "Phường",
            width: 200,
            sorter: true,
            dataIndex: 'phuong',
            key: 'phuong',            
            render:(value: string,record: BaoCaoViPhamModel) => {
                return "Phường " + getWardByDistrictWithCode(record.quan || '',record.phuong || '').name;
            },
            showSorterTooltip:false
        },
        {
            title: "Quận",
            width: 200,
            sorter: true,
            dataIndex: 'quan',
            key: 'quan',            
            render:(value: string,record: BaoCaoViPhamModel) => {
                return "Quận " +getDistrictWithCode(record.quan || '').name;
            },
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
                return tinhTrangBaoCaoViPham[value];
            },
        },
        {
            title: "Hành động",
            width: 80,
            key: 'function',
            fixed: 'right',
            render: (row:BaoCaoViPhamModel) => {
                return (
                <Dropdown
                destroyPopupOnHide
                overlayClassName='drop-down-button'
                menu={{ items: [
                    {
                        label: "Chi tiết",
                        key: "1",
                        icon: <EditOutlined />,
                        onClick: ()=>navigate(`${ConfigRoute.CanBoSo.BaoCaoViPham}/${row.id}`),
                    },
                    {
                        label: "Cập nhật",
                        key: "1",
                        icon: <EditOutlined />,
                        onClick: ()=>navigate(`${ConfigRoute.CanBoSo.BaoCaoViPham}/capnhat/${row.id}`),
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
            <Spin spinning={loading}>
                <Space direction='vertical'>
                <Space direction='vertical' size={0} className='layout-basic-page'>
                    <Row wrap={false} gutter={5}>
                    <Col flex='auto'>
                    <Search placeholder="Tìm kiếm..." enterButton="Search" size="large" />
                    </Col>
                    <Col flex='none'>
                        <Button shape='circle' size='large' type='primary' icon={<PlusOutlined />} onClick={onCreateClick} className={`btn-create`}></Button>
                    </Col>
                    </Row>
                    <Table columns={columns} dataSource={baoCaoViPhams}>
                    </Table>
                </Space>
                </Space>
            </Spin>
        </Suspense>
    );
}