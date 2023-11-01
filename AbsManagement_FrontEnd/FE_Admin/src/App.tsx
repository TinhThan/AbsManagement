import { PageContainer, ProLayout } from '@ant-design/pro-layout';
import { Button, Switch } from 'antd';
import React, { useRef, useState } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import RouteSystem from './routes/route-app';
import { ConfigRoute } from './routes/ConfigRoute';

const data = [
  {
    path: '/',
    name: '欢迎',
    routes: [
      {
        path: '/welcome',
        name: 'one',
        routes: [
          {
            path: '/welcome/welcome',
            name: 'two',
            exact: true,
          },
        ],
      },
    ],
  },
  {
    path: ConfigRoute.BangQuangCao,
    name: 'Bảng quảng cáo',
  },
];

let serviceData = data;
const App: React.FC = () => {
  const actionRef = useRef<{
    reload: () => void;
  }>();
  const [toggle, setToggle] = useState(false);
  return (
    <>
      <ProLayout
        style={{
          height: '100vh',
        }}
        actionRef={actionRef}
        // suppressSiderWhenMenuEmpty={toggle}
        menu={{
          request: async () => {
            return serviceData;
          },
        }}
        location={{
          pathname: '/home',
        }}
      >
        <PageContainer content="Test">
          <RouteSystem/>
        </PageContainer>
      </ProLayout>
    </>
  );
};

export default App;