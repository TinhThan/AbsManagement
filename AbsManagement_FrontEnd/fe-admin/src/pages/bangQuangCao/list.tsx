import { Suspense, useState,useEffect } from 'react';
import React from 'react';
import { PageContainer, PageLoading } from '@ant-design/pro-components';
import { Button, Col, Dropdown, Input, Row, Space, Spin, Table, TableColumnType } from 'antd';
import { BangQuangCaoModel } from '../../apis/bangQuangCao/bangQuangCaoModel';
import { bangQuangCaoAPI } from '../../apis/bangQuangCao/bangQuangCaoAPI';
import { useNavigate } from 'react-router-dom';
import { EditOutlined, EllipsisOutlined, PlusOutlined } from '@ant-design/icons';
import { ConfigRoute } from '../../routes/ConfigRoute';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';

const { Search } = Input;

export default function ListBangQuangCao(): JSX.Element {
    const navigate = useNavigate();
    const [bangQuangCaos,setBangQuangCaos] = useState<BangQuangCaoModel[]>([]);
    const [loading,setLoading] = useState(false);
    const [user,setUser] = useState<UserStorage>();

    useEffect(() => {        
        const useInfo = UserInfoStorage.get();
        if(useInfo)
        {
            setUser(useInfo);
        }
            getBangQuangCaos();
    }, [])
    

    async function getBangQuangCaos() {
        setLoading(true);
        try {
            const response = await bangQuangCaoAPI.DanhSach();
            const newData = response.data.map((item: any) => {
            return {
                ...item,
                key: item.id
            }
        })
            setBangQuangCaos(newData);
        } finally {
            setLoading(false);
        }
    }

    const columns: TableColumnType<BangQuangCaoModel>[] = [
        {
            title: 'Id',
            dataIndex: 'id',
            width: 100,
            fixed: true
        },
        {
            title: "Địa chỉ",
            width: 350,
            sorter: true,
            dataIndex: 'diaChi',
        },
        {
            title: "Phường",
            width: 200,
            sorter: true,
            dataIndex: 'phuong',
        },
        {
            title: "Quận",
            width: 200,
            sorter: true,
            dataIndex: 'quan',
        },
        {
            title: "Loại bảng quảng cáo",
            width: 200,
            sorter: true,
            dataIndex: 'tenLoaiBangQuangCao',
        },
        {
            title: "Kích thước",
            width: 200,
            sorter: true,
            dataIndex: 'kichThuong',
        },
        {
            title: "Ngày hết hạn",
            width: 200,
            sorter: true,
            dataIndex: 'ngayHetHan',
        },
        {
            title: "Tình trạng",
            width: 200,
            sorter: true,
            dataIndex: 'idTinhTrang',
        },
        {
            title: "Hành động",
            width: 80,
            key: 'function',
            fixed: 'right',
            render: (row:BangQuangCaoModel) => {
                return (
                <Dropdown
                destroyPopupOnHide
                overlayClassName='drop-down-button'
                menu={{ items: [
                    {
                        label: "Chi tiết",
                        key: "1",
                        icon: <EditOutlined />,
                        onClick: ()=>navigate(`${ConfigRoute.CanBoSo.BangQuangCao}/${row.id}`),
                    },
                    {
                        label: user?.role === 'CanBoSo' ? "Cập nhật" : "Tạo phiếu chỉnh sửa",
                        key: "2",
                        icon: <EditOutlined />,
                        onClick: ()=>navigate(`${ConfigRoute.CanBoSo.BangQuangCao}/capnhat/${row.id}`),
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
            <PageContainer title="Danh sách bảng quảng cáo">
                <Spin spinning={loading}>
                    <Space direction='vertical' size={0} className='layout-basic-page'>
                        <Row wrap={false} gutter={[5,5]} style={{marginBottom:'10px'}}>
                            <Col flex='auto'>
                            <Search placeholder="Tìm kiếm..." enterButton="Search" size="large" />
                            </Col>
                            <Col flex='none'>
                                <Button shape='circle' size='large' type='primary' icon={<PlusOutlined />}
                                onClick={()=>navigate(`${ConfigRoute.CanBoSo.BangQuangCao}/capphep`)} className={`btn-create`}></Button>
                            </Col>
                        </Row>
                        <Table columns={columns} dataSource={bangQuangCaos} scroll={{ x: 'max-content' }}/>
                    </Space>
                </Spin>
            </PageContainer>
        </Suspense>
    );
}