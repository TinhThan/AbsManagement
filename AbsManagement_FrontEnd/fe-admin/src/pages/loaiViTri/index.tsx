import { Button, Col, Dropdown, Input, Row, Space, Table, TableColumnType } from "antd";
import React from 'react';
import { PageContainer, PageLoading } from '@ant-design/pro-components';
import { Suspense, useEffect, useState } from "react";
import { renderModal } from "../../utils/render-modal";
import { ModalDetailLoaiViTri } from "./detail";
import { LoaiViTriModel } from "../../apis/loaiViTri/model";
import { ModalUpdateLoaiViTri } from "./update";
import { ModalCreateLoaiViTri } from "./create";
import { loaiViTriAPI } from "../../apis/loaiViTri";
import { DeleteOutlined, EditOutlined, EllipsisOutlined, PlusOutlined } from "@ant-design/icons";

const { Search } = Input;

export default function LoaiViTriFeature(): JSX.Element {
    const [loaiViTris,setLoaiViTris] = useState<LoaiViTriModel[]>([]);
    
    function onDetailClick(model) {
        const _root = renderModal(<ModalDetailLoaiViTri onCancel={() => _root?.unmount()} loaiViTri={model} />);
    }

    function onUpdateClick(model: LoaiViTriModel){
        const _root = renderModal(<ModalUpdateLoaiViTri onCancel={() => {
            _root?.unmount()
            getLoaiViTris()
        }} loaiViTri={model} />);
    }

    function onCreateClick(){
        const _root = renderModal(<ModalCreateLoaiViTri onCancel={() => {
            _root?.unmount()
            getLoaiViTris()
        }}/>);
    }

    function onDeleteClick(){

    }


    useEffect(() => {
        getLoaiViTris();
    }, [])
    

    async function getLoaiViTris() {
        loaiViTriAPI
        .DanhSach()
        .then((response) => {
            if(response && response.status === 200)
            {
                setLoaiViTris(response.data || []);
            }
        });
    }
    
    const columns: TableColumnType<LoaiViTriModel>[] = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            width: 100,
            fixed: true,
            showSorterTooltip:false
        },
        {
            title: "Mã loại vị trí",
            width:700,
            sorter: true,
            dataIndex: 'ma',
            key: 'ma',
            render: (value, record: LoaiViTriModel) => {
                return (
                <b className='b-code' onClick={()=>onDetailClick(record)}>
                {value}
                </b>
                )
            },
            showSorterTooltip:false
        },
        {
            title: "Tên loại vị trí",
            width:700,
            sorter: true,
            dataIndex: 'ten',
            key: 'ten',
            showSorterTooltip:false
        },
        {
            title: "Hành động",
            width: 80,
            key: 'function',
            fixed: 'right',
            render: (row:LoaiViTriModel) => {
                return (
                <Dropdown
                destroyPopupOnHide
                overlayClassName='drop-down-button'
                menu={{ items: [
                    {
                    label: "Cập nhật",
                    key: "1",
                    icon: <EditOutlined />,
                    onClick: ()=>onUpdateClick(row),
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
            <PageContainer title="Danh sách loại vị trí">
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
                <Table columns={columns} dataSource={loaiViTris} />
                </Space>
                </Space>
            </PageContainer>
        </Suspense>
    );
}