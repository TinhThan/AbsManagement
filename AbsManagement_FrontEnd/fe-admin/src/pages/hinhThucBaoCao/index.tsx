import { PageContainer, PageLoading } from '@ant-design/pro-components';
import React, { useEffect, useState } from 'react';
import { Suspense} from "react";
import { HinhThucBaoCaoModel } from '../../apis/hinhThucBaoCao/model';
import { renderModal } from '../../utils/render-modal';
import { hinhThucBaoCaoAPI } from '../../apis/hinhThucBaoCao';
import { Button, Col, Dropdown, Input, Row, Space, Table, TableColumnType } from 'antd';
import { DeleteOutlined, EditOutlined, EllipsisOutlined, PlusOutlined } from '@ant-design/icons';
import { ModalDetailHinhThucBaoCao } from './detail';
import { ModalUpdateHinhThucBaoCao } from './update';
import { ModalCreateHinhThucBaoCao } from './create';

export default function HinhThucBaoCaoFeature(): JSX.Element {
    const [HinhThucBaoCaos,setHinhThucBaoCaos] = useState<HinhThucBaoCaoModel[]>([]);
    
    function onDetailClick(model) {
        const _root = renderModal(<ModalDetailHinhThucBaoCao onCancel={() => _root?.unmount()} HinhThucBaoCao={model} />);
    }

    function onUpdateClick(model: HinhThucBaoCaoModel){
        const _root = renderModal(<ModalUpdateHinhThucBaoCao onCancel={() => {
            _root?.unmount()
            getHinhThucBaoCaos()
        }} HinhThucBaoCao={model} />);
    }

    function onCreateClick(){
        const _root = renderModal(<ModalCreateHinhThucBaoCao onCancel={() => {
            _root?.unmount()
            getHinhThucBaoCaos()
        }}/>);
    }

    function onDeleteClick(){

    }


    useEffect(() => {
        getHinhThucBaoCaos();
    }, [])
    

    async function getHinhThucBaoCaos() {
        hinhThucBaoCaoAPI
        .DanhSach()
        .then((response) => {
            if(response && response.status === 200)
            {
                setHinhThucBaoCaos(response.data || []);
            }
        });
    }
    
    const columns: TableColumnType<HinhThucBaoCaoModel>[] = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            width: 100,
            fixed: true,
            showSorterTooltip:false
        },
        {
            title: "Mã hình thức báo cáo",
            width:700,
            sorter: true,
            dataIndex: 'ma',
            key: 'ma',
            render: (value, record: HinhThucBaoCaoModel) => {
                return (
                <b className='b-code' onClick={()=>onDetailClick(record)}>
                {value}
                </b>
                )
            },
            showSorterTooltip:false
        },
        {
            title: "Tên hình thức báo cáo",
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
            render: (row:HinhThucBaoCaoModel) => {
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
            <PageContainer title={"Danh sách hình thức báo cáo"}>
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
                <Table columns={columns} dataSource={HinhThucBaoCaos} />
                </Space>
                </Space>
            </PageContainer>
        </Suspense>
    );
}