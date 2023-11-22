import { Suspense, useState,useEffect } from 'react';
import { PageLoading } from '@ant-design/pro-components';
import { Button, Table, TableColumnType } from 'antd';
import { DiemDatQuangCaoModel } from '../../apis/models/diemDatQuangCaoModel';
import { BangQuangCaoModel } from '../../apis/models/bangQuangCaoModel';
import { bangQuangCaoAPI } from '../../apis/bangQuangCaoAPI';
import BangQuangCaoForm from './addBangQuangCao';

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

    const handleLogin = async (values: any) => {
     console.log("bang quang cao form", values)
    };

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
          <BangQuangCaoForm onSubmit={handleLogin}/>
        </Suspense>
    );
}