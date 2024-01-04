import { PageContainer, PageLoading } from '@ant-design/pro-components';
import React, { useEffect, useState } from 'react';
import { Suspense} from "react";
import { renderModal } from '../../utils/render-modal';
import { Button, Col, Dropdown, Input, Row, Space, Table, TableColumnType } from 'antd';
import { DeleteOutlined, EditOutlined, EllipsisOutlined, PlusOutlined } from '@ant-design/icons';
import { HinhThucQuangCaoModel } from '../../apis/hinhThucQuangCao/model';
import { hinhThucQuangCaoAPI } from '../../apis/hinhThucQuangCao';
import { ModalDetailHinhThucQuangCao } from './detail';
import { ModalUpdateHinhThucQuangCao } from './update';
import { ModalCreateHinhThucQuangCao } from './create';

export default function HinhThucQuangCaoFeature(): JSX.Element {
    const [hinhThucQuangCaos,setHinhThucQuangCaos] = useState<HinhThucQuangCaoModel[]>([]);
    
    function onDetailClick(model) {
        const _root = renderModal(<ModalDetailHinhThucQuangCao onCancel={() => _root?.unmount()} hinhThucQuangCao={model} />);
    }

    function onUpdateClick(model: HinhThucQuangCaoModel){
        const _root = renderModal(<ModalUpdateHinhThucQuangCao onCancel={() => {
            _root?.unmount()
            getHinhThucQuangCaos()
        }} hinhThucQuangCao={model} />);
    }

    function onCreateClick(){
        const _root = renderModal(<ModalCreateHinhThucQuangCao onCancel={() => {
            _root?.unmount()
            getHinhThucQuangCaos()
        }}/>);
    }

    function onDeleteClick(){

    }


    useEffect(() => {
        getHinhThucQuangCaos();
    }, [])
    

    async function getHinhThucQuangCaos() {
        hinhThucQuangCaoAPI
        .DanhSach()
        .then((response) => {
            if(response && response.status === 200)
            {
                const newData = response.data.map((item: any) => {
                    return {
                        ...item,
                        key: item.id
                    }
                })
                setHinhThucQuangCaos(newData || []);
            }
        });
    }
    
    const columns: TableColumnType<HinhThucQuangCaoModel>[] = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            width: 100,
            fixed: true,
            showSorterTooltip:false
        },
        {
            title: "Mã hình thức quảng cáo",
            width:700,
            sorter: true,
            dataIndex: 'ma',
            key: 'ma',
            render: (value, record: HinhThucQuangCaoModel) => {
                return (
                <b className='b-code' onClick={()=>onDetailClick(record)}>
                {value}
                </b>
                )
            },
            showSorterTooltip:false
        },
        {
            title: "Tên hình thức quảng cáo",
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
            render: (row:HinhThucQuangCaoModel) => {
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
            <PageContainer title={"Danh sách hình thức quảng cáo"}>
                <Space direction='vertical'>
                <Space direction='vertical' size={0} className='layout-basic-page'>
                <Row wrap={false} gutter={5}>
                    <Col flex='auto'>
                    <Input.Search placeholder="Tìm kiếm..." enterButton="Search" size="large" />
                    </Col>
                    <Col flex='none'>
                    <Button shape='circle' size='large' type='primary' icon={<PlusOutlined />} onClick={onCreateClick} className={`btn-create`}></Button>
                    </Col>
                </Row>
                <Table columns={columns} dataSource={hinhThucQuangCaos} />
                </Space>
                </Space>
            </PageContainer>
        </Suspense>
    );
}