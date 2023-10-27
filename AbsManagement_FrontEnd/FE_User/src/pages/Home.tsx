import React, { useEffect } from 'react';
import { Breadcrumb, Layout, theme, Space } from 'antd';
import HeaderLayout from '../layouts/Header';
import FooterLayout from '../layouts/Footer';
import { Link, Route, Routes } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import ActorList from '../pages/actors/index';
import ActorDetail from '../pages/actors/_id';
import TokenStorage from '../apis/storages/tokenStorage';

const { Content } = Layout;

const Home: React.FC = () => {
  const {
    token: { colorBgContainer },
  } = theme.useToken();
  const navigate = useNavigate();
  const accessToken = TokenStorage.get();

  useEffect(() => {
    if(!accessToken)
      {
        navigate('/login');
      }
  })
  

  return (
    <Layout>
      <HeaderLayout />
      <Content className="site-layout" style={{ padding: '0 50px' }}>
        <Breadcrumb style={{ margin: '16px 0' }}>
          <Breadcrumb.Item>
            <Link to="/">Home</Link>
          </Breadcrumb.Item>
        </Breadcrumb>
        <div style={{ padding: 24, minHeight: 380, background: colorBgContainer }}>
          <Space.Compact style={{ width: '100%' }}>
            <Routes>
              <Route path="/actors/:id" element={<ActorDetail />} />
              <Route path="/actors" element={<ActorList />} />
              <Route path="/" element={<h2>Welcome</h2>} />
            </Routes>
          </Space.Compact>
        </div>
      </Content>
      <FooterLayout />
    </Layout>
  );
};

export default Home;
