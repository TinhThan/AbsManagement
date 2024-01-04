import { Suspense, useState,useEffect } from 'react';
import React from 'react';
import { PageLoading } from '@ant-design/pro-components';
import { Button, Table, TableColumnType } from 'antd';
import { BangQuangCaoModel } from '../../apis/bangQuangCao/bangQuangCaoModel';
import { bangQuangCaoAPI } from '../../apis/bangQuangCao/bangQuangCaoAPI';

export default function BangQuangCaoFeature(): JSX.Element {
    const [bangQuangCaos,setBangQuangCaos] = useState<BangQuangCaoModel[]>([]);
    const [loading,setLoading] = useState(false);

    useEffect(() => {
        getBangQuangCaos();
    }, [])
    

    async function getBangQuangCaos() {
      setLoading(true);
      try {
        const response = await bangQuangCaoAPI.DanhSach();
        setBangQuangCaos(response);
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
          width: 200,
          sorter: true,
          dataIndex: 'diaChi',
        },
        {
            title: "Phường",
            width: 200,
            sorter: true,
            dataIndex: 'phuong',
        },
        {
            title: "Quận",
            width: 200,
            sorter: true,
            dataIndex: 'quan',
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
            dataIndex: 'kichThuong',
        },
        {
            title: "Ngày hết hạn",
            width: 200,
            sorter: true,
            dataIndex: 'ngayHetHan',
        },
        {
            title: "Tình trạng",
            width: 200,
            sorter: true,
            dataIndex: 'idTinhTrang',
        }
    ];
  
    return (
        <Suspense fallback={<PageLoading/>}>
          <Table  columns={columns} dataSource={bangQuangCaos} />
        </Suspense>
    );
}