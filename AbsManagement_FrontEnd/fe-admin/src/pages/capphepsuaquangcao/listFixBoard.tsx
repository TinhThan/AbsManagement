import { PageContainer, PageLoading } from "@ant-design/pro-components";
import { Col, Dropdown, Input, Popconfirm, Row, Space, Spin, Table, TableColumnType } from "antd";
import { FC, Suspense, useState } from "react";
import { createSearchParams, useNavigate } from "react-router-dom";
import { DanhSachPhieuCapPhepSuaBangQuangCao } from "../../apis/phieuChinhSua/model";
import { ConfigRoute } from "../../routes/ConfigRoute";
import { EditOutlined, EllipsisOutlined } from '@ant-design/icons';

const { Search } = Input;

const ListFixBoard : FC = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [data, setData] = useState<DanhSachPhieuCapPhepSuaBangQuangCao[]>([]);

    const handleOk = () => {

    }

    const handleCancel = () => {
        setOpen(false);
    };

    const updateReportStatus = (id: number) => {
        setOpen(true);
    }

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
                                        pathname: `${ConfigRoute.CanBoSo.BaoCaoViPham}/chitiet`,
                                        search: `?${createSearchParams({
                                            id: row.id.toString()
                                        })}`
                                    })
                                },
                                (row.idTinhTrang != "DaXuLy") ?
                                    {
                                        label: "Cập nhật trạng thái",
                                        key: "2",
                                        icon: <EditOutlined />,
                                        onClick: () => updateReportStatus(row.id)
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

export default ListFixBoard