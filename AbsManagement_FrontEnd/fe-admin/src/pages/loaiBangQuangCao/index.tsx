import { PageContainer, PageLoading } from '@ant-design/pro-components';
import React, { useEffect, useState } from 'react';
import { Suspense} from "react";
import { renderModal } from '../../utils/render-modal';
import { Button, Col, Dropdown, Input, Row, Space, Table, TableColumnType } from 'antd';
import { DeleteOutlined, EditOutlined, EllipsisOutlined, PlusOutlined } from '@ant-design/icons';
import { LoaiBangQuangCaoModel } from '../../apis/loaiBangQuangCao/model';
import { loaiBangQuangCaoAPI } from '../../apis/loaiBangQuangCao';
import { ModalDetailLoaiBangQuangCao } from './detail';
import { ModalUpdateLoaiBangQuangCao } from './update';
import { ModalCreateLoaiBangQuangCao } from './create';

export default function LoaiBangQuangCaoFeature(): JSX.Element {
    const [loaiBangQuangCaos,setLoaiBangQuangCaos] = useState<LoaiBangQuangCaoModel[]>([]);
    
    function onDetailClick(model) {
        const _root = renderModal(<ModalDetailLoaiBangQuangCao onCancel={() => _root?.unmount()} loaiBangQuangCao={model} />);
    }

    function onUpdateClick(model: LoaiBangQuangCaoModel){
        const _root = renderModal(<ModalUpdateLoaiBangQuangCao onCancel={() => {
            _root?.unmount()
            getLoaiBangQuangCaos()
        }} loaiBangQuangCao={model} />);
    }

    function onCreateClick(){
        const _root = renderModal(<ModalCreateLoaiBangQuangCao onCancel={() => {
            _root?.unmount()
            getLoaiBangQuangCaos()
        }}/>);
    }

    function onDeleteClick(){

    }


    useEffect(() => {
        getLoaiBangQuangCaos();
    }, [])
    

    async function getLoaiBangQuangCaos() {
        loaiBangQuangCaoAPI
        .DanhSach()
        .then((response) => {
            if(response && response.status === 200)
            {
                setLoaiBangQuangCaos(response.data || []);
            }
        });
    }
    
    const columns: TableColumnType<LoaiBangQuangCaoModel>[] = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            width: 100,
            fixed: true,
            showSorterTooltip:false
        },
        {
            title: "Mã loại bảng quảng cáo",
            width:700,
            sorter: true,
            dataIndex: 'ma',
            key: 'ma',
            render: (value, record: LoaiBangQuangCaoModel) => {
                return (
                <b className='b-code' onClick={()=>onDetailClick(record)}>
                {value}
                </b>
                )
            },
            showSorterTooltip:false
        },
        {
            title: "Tên loại bảng quảng cáo",
            sorter: true,
            width:700,
            dataIndex: 'ten',
            key: 'ten',
            showSorterTooltip:false
        },
        {
            title: "Hành động",
            width: 80,
            key: 'function',
            fixed: 'right',
            render: (row:LoaiBangQuangCaoModel) => {
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
            <PageContainer title="Danh sách loại bảng quảng cáo">
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
                <Table columns={columns} dataSource={loaiBangQuangCaos} />
                </Space>
                </Space>
            </PageContainer>
        </Suspense>
    );
}