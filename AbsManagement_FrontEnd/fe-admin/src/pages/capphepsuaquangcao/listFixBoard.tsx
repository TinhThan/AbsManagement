import { PageContainer, PageLoading } from "@ant-design/pro-components";
import { Col, Dropdown, Input, Popconfirm, Row, Space, Spin, Table, TableColumnType } from "antd";
import { FC, Suspense, useEffect, useState } from "react";
import { DanhSachPhieuCapPhepSuaBangQuangCao } from "../../apis/phieuChinhSua/model";
import { EditOutlined, EllipsisOutlined } from '@ant-design/icons';
import { phieuChinhSuaAPI } from "../../apis/phieuChinhSua";
import moment from 'moment';
import { tinhTrangType } from "./listFixLocation";
import { useNavigate, createSearchParams } from "react-router-dom";
import { ConfigRoute } from "../../routes/ConfigRoute";

const { Search } = Input;

const tinhTrangDiemDatQuangCao = {
    DaQuyHoach: "Đã quy hoạch",
    ChuaQuyHoach: "Chưa quy hoạch",
    ChoDuyet: "Chờ duyệt",
    DaDuyet: "Đã duyệt",
    DaHuy: "Đã hủy"
}

const ListFixBoard : FC = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [data, setData] = useState<DanhSachPhieuCapPhepSuaBangQuangCao[]>([]);

    const onCancel = (id:number) => {
        phieuChinhSuaAPI.CapNhat(id, {
            tinhTrang: 'DaHuy',
        })
            .then((res: any) => {
                if (res && res.status === 200) {
                    getDanhSachSuaBang();
                }
            })
    }

    const onApprove = (id:number) => {
        phieuChinhSuaAPI.CapNhat(id, {
            tinhTrang: 'DaDuyet',
        })
            .then((res: any) => {
                if (res && res.status === 200) {
                    getDanhSachSuaBang();
                }
            })
    }

    async function getDanhSachSuaBang() {
        setLoading(true);
        phieuChinhSuaAPI
            .DanhSachPhieuSuaBangQuangCao()
            .then((response) => {
                if (response && response.status === 200) {
                    const newData = response.data.map((item: any) => {
                        return {
                            ...item,
                            key: item.id
                        }
                    })
                    setData(newData || []);
                }
            });

        setLoading(false);
    }

    useEffect(() => {
        getDanhSachSuaBang();
    }, [])

    const columns: TableColumnType<DanhSachPhieuCapPhepSuaBangQuangCao>[] = [
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
            dataIndex: 'tenCanBoGui',
            key: 'tenCanBoGui'
        },
        {
            title: "Ngày gửi",
            width: 150,
            sorter: true,
            dataIndex: 'ngayGui',
            key: 'ngayGui',
            render: (value: string) => moment(value).format('DD-MM-YYYY HH:mm')
        },
        {
            title: "Địa điểm",
            width: 150,
            sorter: true,
            dataIndex: 'bangQuangCao',
            key: 'diaChi',
            render: (value: any) => value?.diaChiCongTy || 'N/A',
        },
        {
            title: "Kích thước",
            width: 150,
            sorter: true,
            dataIndex: 'bangQuangCao',
            key: 'kichthuoc',
            render: (value: any) => value?.kichThuoc || 'N/A',
        },
        {
            title: "Tình trạng bảng quảng cáo",
            width: 150,
            sorter: true,
            dataIndex: 'bangQuangCao',
            key: 'idTinhTrang',
            render: (value: any) => tinhTrangDiemDatQuangCao[value?.idTinhTrang] || 'N/A',
        },
        {
            title: "Tình trạng",
            width: 150,
            sorter: true,
            dataIndex: 'tinhTrang',
            key: 'tinhTrang',
            showSorterTooltip: false,
            render: (value: string) => {
                return tinhTrangType[value];
            },
        },
        {
            title: "Hành động",
            width: 80,
            key: 'function',
            fixed: 'right',
            render: (row: DanhSachPhieuCapPhepSuaBangQuangCao) => {
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
                                    onClick: () => navigate({
                                        pathname: `${ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao}/bang-quang-cao/chitiet`,
                                        search: `?${createSearchParams({
                                            id: row.id.toString()
                                        })}`
                                    })
                                },
                                (row.tinhTrang !== "DaDuyet" && row.tinhTrang !== "DaHuy") ?
                                    {
                                        label: "Duyệt",
                                        key: "2",
                                        icon: <EditOutlined />,
                                        onClick: () => onApprove(row.id)
                                    } : null,
                                (row.tinhTrang != "DaDuyet" && row.tinhTrang !== "DaHuy") ?
                                {
                                    label: "Hủy",
                                    key: "3",
                                    icon: <EditOutlined />,
                                    onClick: () => onCancel(row.id)
                                } : null
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
            <PageContainer title="Danh sách bảng quảng cáo cần sửa chửa">
                <Spin spinning={loading}>
                    <Space direction='vertical' size={0} className='layout-basic-page'>
                        <Row wrap={false} gutter={[5, 5]} style={{ marginBottom: '10px' }}>
                            <Col flex='auto'>
                                <Search placeholder="Tìm kiếm..." enterButton="Search" size="large" />
                            </Col>
                        </Row>
                        <Table columns={columns} dataSource={data} scroll={{ x: 'max-content' }} />
                    </Space>
                </Spin>
            </PageContainer>
        </Suspense>
    )
}

export default ListFixBoard