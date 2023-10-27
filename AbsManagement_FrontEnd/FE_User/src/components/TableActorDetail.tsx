import React from 'react';
import { Table } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import { film } from './TableActor';

const columns: ColumnsType<film> = [
  {
    title: 'Id',
    dataIndex: 'filmId',
    key: 'filmId',
  },
  {
    title: 'Title',
    dataIndex: 'title',
    key: 'title',
  },
  {
    title: 'Description',
    dataIndex: 'description',
    key: 'description',
  }
];

interface TableActorProps {
  data: film[];
}

const TableActorDetail: React.FC<TableActorProps> = ({ data }) => {
  return (
    <div>
      <Table
        rowKey={(row) => row.filmId}
        columns={columns}
        dataSource={data}
      />
    </div>
  );
};

export default TableActorDetail;
