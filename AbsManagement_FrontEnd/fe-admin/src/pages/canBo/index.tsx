import { Suspense, useState,useEffect } from 'react';
import React from 'react';
import { PageLoading } from '@ant-design/pro-components';
import { Button, Col, Dropdown, Input, Row, Space, Spin, Table, TableColumnType } from 'antd';
import { CanBoModel, CapNhatCanBoModel } from '../../apis/canBo/canBoModel';
import { canBoAPI } from '../../apis/canBo/canBoAPI';
import { DeleteOutlined, EditOutlined, EllipsisOutlined, KeyOutlined, PlusOutlined } from '@ant-design/icons';
import { renderModal } from '../../utils/render-modal';
import { ModalDetailCanBo } from './detail';
import { FormatTime, GetDateTimeByFormat } from '../../utils';
import { ModalUpdateCanBo } from './update';
import dayjs from 'dayjs';
import { ModalCreateCanBo } from './create';
import { getDistrict, getWardByDistrict } from '../../utils/getWard';

const { Search } = Input;

export const RoleCanBo = {
  Chung:"Chung",
  CanBoPhuong: "CanBoPhuong",
  CanBoQuan: "CanBoQuan",
  CanBoSo: "CanBoSo"
}

const roleCanBos = {
  Chung:"Lỗi",
  CanBoPhuong: "Cán bộ phường",
  CanBoQuan: "Cán bộ quận",
  CanBoSo: "Cán bộ sở"
}

export default function CanBoFeature(): JSX.Element {
    const [canBos,setCanBos] = useState<CanBoModel[]>([]);
    const [loading,setLoading] = useState(false);
    function onDetailClick(model) {
      const _root = renderModal(<ModalDetailCanBo onCancel={() => _root?.unmount()} canBo={model} />);
    }

    function onUpdateClick(model: CanBoModel){
      const capNhatCanBoModel: CapNhatCanBoModel = {
        ...model,
        phuong:model.noiCongTac[1],
        quan:model.noiCongTac[0],
        ngaySinh: dayjs(model.ngaySinh)
      };
      const _root = renderModal(<ModalUpdateCanBo onCancel={() => {
        _root?.unmount()
        getCanBos()
      }} canBo={capNhatCanBoModel} />);
    }

    function onCreateClick(){
      const _root = renderModal(<ModalCreateCanBo onCancel={() => {
        _root?.unmount()
        getCanBos()
      }}/>);
    }

    function onDeleteClick(){

    }

    function onChangePass(){

    }

    useEffect(() => {
       getCanBos();
    }, [])
    

    async function getCanBos() {
      setLoading(true)
      canBoAPI
        .DanhSach()
        .then((response) => {
            if(response && response.status === 200)
            {
              setCanBos(response.data || []);
            }
        });
      setLoading(false)
    }
    
    const columns: TableColumnType<CanBoModel>[] = [
        {
          title: 'Id',
          dataIndex: 'id',
          key: 'id',
          width: 100,
          fixed: true,
          showSorterTooltip:false
        },
        {
          title: "Email",
          width: 200,
          sorter: true,
          dataIndex: 'email',
          key: 'email',
          render: (value, record: CanBoModel) => {
            return (
              <b className='b-code' onClick={()=>onDetailClick(record)}>
              {value}
              </b>
            )
          },
          showSorterTooltip:false
        },
        {
            title: "Họ tên",
            width: 200,
            sorter: true,
            dataIndex: 'hoTen',
            key: 'hoTen',
            showSorterTooltip:false
        },
        {
            title: "Số điện thoại",
            width: 200,
            sorter: true,
            dataIndex: 'soDienThoai',
            key: 'soDienThoai',
            showSorterTooltip:false
        },
        {
            title: "Ngày sinh",
            width: 200,
            sorter: true,
            dataIndex: 'ngaySinh',
            key: 'ngaySinh',
            render:(value) =>{
              return GetDateTimeByFormat(value,FormatTime.DDMMYYYY)
            },
            showSorterTooltip:false
        },
        {
            title: "Quyền",
            width: 200,
            sorter: true,
            dataIndex: 'role',
            key: 'role',
            render: (value) => {
              return (
                <>{roleCanBos[value]}</>
              )
            },
        },
        {
            title: "Nơi công tác",
            width: 200,
            sorter: true,
            dataIndex: 'noiCongTac',
            key: 'noiCongTac',
            showSorterTooltip:false,
            render: (value:string[]) => {
              console.log("value",value)
              return (
                <>
                  <>{ value.length > 0 ? value.length > 1 ? `Phường ${getWardByDistrict(value[0],value[1]).name}, Quận ${getDistrict(value[0]).name}`: `Quận ${getDistrict(value[0]).name}` : ''}</>
                </>
              )
            },
        },
        {
          title: "Hành động",
          width: 80,
          key: 'function',
          fixed: 'right',
          render: (row:CanBoModel) => {
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
                  label: "Cập nhật mật khẩu",
                  key: "2",
                  icon: <KeyOutlined className='icon-changedPass' />,
                  onClick: onChangePass,
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
          <Spin spinning={loading}>
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
                <Table columns={columns} dataSource={canBos} >
                </Table>
              </Space>
            </Space>
          </Spin>
        </Suspense>
    );
}