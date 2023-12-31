import { PageContainer, PageLoading } from "@ant-design/pro-components";
import { Col, Dropdown, Input, Popconfirm, Row, Space, Spin, Table, TableColumnType } from "antd";
import { FC, Suspense, useEffect, useState } from "react";
import { DanhSachPhieuCapPhepModel } from "../../apis/phieuCapPhepBangQuangCao/model";
import { EditOutlined, EllipsisOutlined } from '@ant-design/icons';
import { phieuCapPhepBangQuangCaoAPI } from "../../apis/phieuCapPhepBangQuangCao";
import moment from 'moment';
import { useNavigate, createSearchParams } from "react-router-dom";
import { ConfigRoute } from "../../routes/ConfigRoute";
import { MessageBox } from "../../utils/messagebox";

const { Search } = Input;

export const tinhTrangType = {
    ChoDuyet: "Chờ duyệt",
    DaDuyet: "Đã duyệt",
    DaHuy:"Đã hủy"
}

const ListAcceptAds : FC = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [data, setData] = useState<DanhSachPhieuCapPhepModel[]>([]);

    const handleOk = (idUpdate) => {
        phieuCapPhepBangQuangCaoAPI.Duyet(idUpdate)
            .then((res: any) => {
                if (res && res.status === 200) {
                    getDanhSachSuaDiemDat();
                }
            })
    }
    async function getDanhSachSuaDiemDat() {
        setLoading(true);
        phieuCapPhepBangQuangCaoAPI
            .DanhSach()
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
        getDanhSachSuaDiemDat();
    }, [])

    const columns: TableColumnType<DanhSachPhieuCapPhepModel>[] = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            width: 80,
            fixed: true
        },
        {
            title: "Tên cán bộ gửi",
            width: 150,
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
            title: "Tên loại bảng",
            width: 250,
            sorter: true,
            dataIndex: 'tenLoaiBangQuangCao',
            key: 'tenLoaiBangQuangCao'
        },
        {
            title: "Địa điểm",
            width: 250,
            sorter: true,
            dataIndex: 'diaChi',
            key: 'diaChi'
        },
        {
            title: "Tình trạng",
            width: 150,
            sorter: true,
            dataIndex: 'idTinhTrang',
            key: 'idTinhTrang',
            fixed: 'right',
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
            render: (row: DanhSachPhieuCapPhepModel) => {
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
                                        pathname: `${ConfigRoute.CanBoSo.DuyetCapPhepQuangCao}/chitiet`,
                                        search: `?${createSearchParams({
                                            id: row.id.toString()
                                        })}`
                                    })
                                },
                                (row.idTinhTrang != "DaDuyet") ?
                                    {
                                        label: "Duyệt phiếu",
                                        key: "2",
                                        icon: <EditOutlined />,
                                        onClick: () => MessageBox.Confirm({
                                            onOk:()=> handleOk(row.id),
                                            content:"Bạn có muốn duyệt phiếu này?"
                                        })
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
            <PageContainer title="Danh sách biển quảng cáo cần cấp phép">
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

export default ListAcceptAds;