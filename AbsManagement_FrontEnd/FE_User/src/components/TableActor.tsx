import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Table } from 'antd';
import type { ColumnsType } from 'antd/es/table';

export interface Actor {
  id: number;
  firstName: string;
  lastName: string;
  films: film[];
}

export interface film {
  filmId: number;
  title: string;
  description: string;
}

const columns: ColumnsType<Actor> = [
  {
    title: 'Id',
    dataIndex: 'id',
    key: 'id',
  },
  {
    title: 'FirstName',
    dataIndex: 'firstName',
    key: 'firstName',
  },
  {
    title: 'LastName',
    dataIndex: 'lastName',
    key: 'lastName',
  },
];

interface TableActorProps {
  data: Actor[];
}

const TableActor: React.FC<TableActorProps> = ({ data }) => {
  const navigate = useNavigate();
  const handleRowSelection = (record: Actor, index: number) => {
    navigate(`/actors/${record.id}`);
  };

  return (
    <div>
      <Table
        columns={columns}
        rowKey={(row) => row.id}
        dataSource={data}
        onRow={(record, index) => ({
          onClick: () => handleRowSelection(record, index as number),
        })}
      />
    </div>
  );
};

export default TableActor;
