import { PageContainer, PageLoading } from "@ant-design/pro-components";
import { Col, Dropdown, Input, Popconfirm, Row, Space, Spin, Table, TableColumnType } from "antd";
import { FC, Suspense, useEffect, useState } from "react";
import { createSearchParams, useNavigate } from "react-router-dom";
import { DanhSachPhieuCapPhepSuaDiemDat } from "../../apis/phieuChinhSua/model";
import { ConfigRoute } from "../../routes/ConfigRoute";
import { EditOutlined, EllipsisOutlined } from '@ant-design/icons';
import { phieuChinhSuaAPI } from "../../apis/phieuChinhSua";
import moment from 'moment';

const { Search } = Input;

export const tinhTrangType = {
    ChoDuyet: "Chờ duyệt",
    DaDuyet: "Đã duyệt"
}

const ListFixLocation: FC = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [data, setData] = useState<DanhSachPhieuCapPhepSuaDiemDat[]>([]);
    const [idUpdate, setIdUpdate] = useState<number>(0);

    const handleOk = () => {
        setConfirmLoading(true);
        const payload = {
            tinhTrang: 'DaDuyet',
        }

        phieuChinhSuaAPI.CapNhat(idUpdate, payload)
            .then((res: any) => {
                if (res && res.status === 200) {
                    getDanhSachSuaDiemDat();
                }
            })

        setOpen(false);
        setConfirmLoading(false);
    }

    const handleCancel = () => {
        setOpen(false);
    };

    const updateFixLocationStatus = (id: number) => {
        setOpen(true);
        setIdUpdate(id);
    }

    async function getDanhSachSuaDiemDat() {
        setLoading(true);
        phieuChinhSuaAPI
            .DanhSachPhieuSuaDiemDat()
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

    const columns: TableColumnType<DanhSachPhieuCapPhepSuaDiemDat>[] = [
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
            title: "Địa điểm",
            width: 250,
            sorter: true,
            dataIndex: 'diemDatQuangCao',
            key: 'diaChi',
            render: (value: any) => value?.diaChi || 'N/A',
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
            render: (row: DanhSachPhieuCapPhepSuaDiemDat) => {
                return (
                    <Dropdown
                        destroyPopupOnHide
                        overlayClassName='drop-down-button'
                        menu={{
                            items: [
                                (row.tinhTrang != "DaDuyet") ?
                                    {
                                        label: "Cập nhật trạng thái",
                                        key: "1",
                                        icon: <EditOutlined />,
                                        onClick: () => updateFixLocationStatus(row.id)
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
            <PageContainer title="Danh sách điểm đặt quảng cáo cần sửa chửa">
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

            <Popconfirm
                title=" "
                description="Xác nhận cập nhật thông tin!"
                open={open}
                onConfirm={handleOk}
                okButtonProps={{ loading: confirmLoading }}
                onCancel={handleCancel}
            />
        </Suspense>
    )
}

export default ListFixLocation