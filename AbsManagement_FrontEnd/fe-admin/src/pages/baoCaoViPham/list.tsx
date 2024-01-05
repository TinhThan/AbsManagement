import { Suspense, useState, useEffect } from 'react';
import React from 'react';
import { PageContainer, PageLoading } from '@ant-design/pro-components';
import { Button, Col, Dropdown, Input, Row, Space, Spin, Table, TableColumnType } from 'antd';
import { BaoCaoViPhamModel } from '../../apis/baoCaoViPham/baoCaoViPhamModel';
import { EditOutlined, EllipsisOutlined, PlusOutlined } from '@ant-design/icons';
import { getDistrictWithCode, getWardByDistrictWithCode } from '../../utils/getWard';
import { baoCaoViPhamAPI } from '../../apis/baoCaoViPham';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';
import { createSearchParams, useNavigate } from 'react-router-dom';
import { ConfigRoute } from '../../routes/ConfigRoute';

const { Search } = Input;

export const tinhTrangBaoCaoViPham = {
    ChuaXuLy: "Chưa xử lý",
    DaXyLy: "Đã xử lý"
}

export default function ListBaoCaoViPham(): JSX.Element {
    const navigate = useNavigate();
    const [baoCaoViPhams, setBaoCaoViPhams] = useState<BaoCaoViPhamModel[]>([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        setLoading(true);
        const useInfo = UserInfoStorage.get();
        getBaoCaoViPhams(useInfo);
        setLoading(false);
    }, [])


    async function getBaoCaoViPhams(useInfo) {
        baoCaoViPhamAPI
            .DanhSach(useInfo?.noiCongTac[0] || '', useInfo?.noiCongTac[1] || '')
            .then((response) => {
                if (response && response.status === 200) {
                    const newData = response.data.map((item: any) => {
                        return {
                            ...item,
                            key: item.id
                        }
                    })
                    setBaoCaoViPhams(newData || []);
                }
            });
    }

    const columns: TableColumnType<BaoCaoViPhamModel>[] = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            width: 80,
            fixed: true
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
            width: 350,
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
            render:(value: string,record: BaoCaoViPhamModel) => {
                return "Phường " + getWardByDistrictWithCode(record.quan || '',record.phuong || '').name;
            },
            showSorterTooltip: false
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
            showSorterTooltip: false
        },
        {
            title: "Tình trạng",
            width: 200,
            sorter: true,
            dataIndex: 'idTinhTrang',
            key: 'idTinhTrang',
            showSorterTooltip: false,
            render: (value: string) => {
                return tinhTrangBaoCaoViPham[value];
            },
        },
        {
            title: "Hành động",
            width: 80,
            key: 'function',
            fixed: 'right',
            render: (row: BaoCaoViPhamModel) => {
                return (
                    <Dropdown
                        destroyPopupOnHide
                        overlayClassName='drop-down-button'
                        menu={{
                            items: [
                                {
                                    label: "Chi tiết",
                                    key: "1",
                                    icon: <EditOutlined />,
                                    // onClick: () => navigate(`${ConfigRoute.CanBoSo.BaoCaoViPham}/${row.id}`),
                                    onClick: () => navigate({
                                        pathname: `${ConfigRoute.CanBoSo.BaoCaoViPham}/chitiet`,
                                        search: `?${createSearchParams({
                                            id: row.id.toString()
                                        })}`
                                    })
                                }
                                // {
                                //     label: "Cập nhật",
                                //     key: "2",
                                //     icon: <EditOutlined />,
                                //     // onClick: () => navigate(`${ConfigRoute.CanBoSo.BaoCaoViPham}/capnhat/${row.id}`),
                                //     onClick: () => navigate({
                                //         pathname: `${ConfigRoute.CanBoSo.BaoCaoViPham}/capnhat`,
                                //         search: `?${createSearchParams({
                                //           id: row.id.toString()
                                //         })}`
                                //     })
                                // }
                            ]
                        }}
                        trigger={['click']}
                    >
                        <EllipsisOutlined />
                    </Dropdown>
                );
            }
        }
    ];

    return (
        <Suspense fallback={<PageLoading />}>
            <PageContainer title="Danh sách báo cáo vi phạm">
                <Spin spinning={loading}>
                    <Space direction='vertical' size={0} className='layout-basic-page'>
                        <Row wrap={false} gutter={[5,5]} style={{marginBottom:'10px'}}>
                            <Col flex='auto'>
                                <Search placeholder="Tìm kiếm..." enterButton="Search" size="large" />
                            </Col>
                        </Row>
                        <Table columns={columns} dataSource={baoCaoViPhams} scroll={{ x: 'max-content' }}/>
                    </Space>
                </Spin>
            </PageContainer>
        </Suspense>
    );
}