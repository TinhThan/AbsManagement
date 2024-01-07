import { Suspense, useState,useEffect } from 'react';
import React from 'react';
import { PageContainer, PageLoading } from '@ant-design/pro-components';
import { Button, Col, Dropdown, Input, Row, Space, Spin, Table, TableColumnType } from 'antd';
import { BangQuangCaoModel } from '../../apis/bangQuangCao/bangQuangCaoModel';
import { bangQuangCaoAPI } from '../../apis/bangQuangCao/bangQuangCaoAPI';
import { useNavigate } from 'react-router-dom';
import { EditOutlined, EllipsisOutlined, PlusOutlined, SendOutlined } from '@ant-design/icons';
import { ConfigRoute } from '../../routes/ConfigRoute';
import UserInfoStorage from '../../storages/user-info';
import { UserStorage } from '../../apis/auth/user';
import { tinhTrangBangQuangCao } from './create';
import { FormatTime, GetDateTimeByFormat } from '../../utils';
import { getDistrictWithCode, getWardByDistrictWithCode } from '../../utils/getWard';

const { Search } = Input;

const tinhTrangBangQuangCaoList = {
    ChuaQuyHoach: "Chưa quy hoạch",
    ChoCapPhep :"Chờ cấp phép",
    ChoDuyet:"Chờ duyệt chỉnh sửa",
    DaQuyHoach: "Đã quy hoạch"
}

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
        getBangQuangCaos(useInfo);
    }, [])
    

    async function getBangQuangCaos(useInfo) {
        setLoading(true);
        try {
            const response = await bangQuangCaoAPI.DanhSach(useInfo?.noiCongTac[0] || '',useInfo?.noiCongTac[1] || '');
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

    async function guiDuyetBangQuangCao(id:number) {
        setLoading(true);
        try {
            await bangQuangCaoAPI.GuiDuyet(id);
            getBangQuangCaos(user)
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
            key: 'phuong',            
            render:(value: string,record: BangQuangCaoModel) => {
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
            render:(value: string,record: BangQuangCaoModel) => {
                return "Quận " +getDistrictWithCode(record.quan).name;
            },
            showSorterTooltip:false
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
            dataIndex: 'kichThuoc',
        },
        {
            title: "Ngày bắt đầu",
            width: 200,
            sorter: true,
            dataIndex: 'ngayBatDau',
            render:(value) =>{
                return GetDateTimeByFormat(value,FormatTime.DDMMYYYY)
              },
        },
        {
            title: "Ngày hết hạn",
            width: 200,
            sorter: true,
            dataIndex: 'ngayHetHan',
            render:(value) =>{
                return GetDateTimeByFormat(value,FormatTime.DDMMYYYY)
              },
        },
        {
            title: "Tình trạng",
            width: 200,
            sorter: true,
            dataIndex: 'idTinhTrang',
            fixed: 'right',
            render: (value:string) => {
                return tinhTrangBangQuangCaoList[value];
            },
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
                    }, 
                    {
                        label: "Gữi phiếu cấp phép",
                        key: "3",
                        disabled: row.idTinhTrang !== 'ChuaQuyHoach',
                        icon: <SendOutlined />,
                        onClick: ()=>guiDuyetBangQuangCao(row.id),
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