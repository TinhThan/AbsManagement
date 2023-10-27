import React, { useEffect, useState } from 'react';
import { Layout, Breadcrumb, message, Button } from 'antd';
import { Link, useNavigate } from 'react-router-dom';
import HeaderLayout from '../../layouts/Header';
import FooterLayout from '../../layouts/Footer';
import TableActor, { Actor } from '../../components/TableActor';
import TokenStorage from '../../apis/storages/tokenStorage';
import { actorApi } from '../../apis/actorAPI';

const { Content } = Layout;

const ActorList: React.FC = () => {
  const navigate = useNavigate();
  const accessToken = TokenStorage.get();
  const [actors, setActors] = useState<Actor[]>([]);

  const getDanhSachActor = async (soLuong: string) => {
    try {
      const response = await actorApi.DanhSach(soLuong);
      setActors(response);
    } catch (error: any) {
      message.error(error.message);
    }
  };

  useEffect(() => {
    if(!accessToken)
    {
      navigate('/login');
      return;
    }
  },[]);

  return (
    <Layout>
      <HeaderLayout />
      <Content className="site-layout" style={{ padding: '0 50px' }}>
        <Breadcrumb style={{ margin: '16px 0' }}>
          <Breadcrumb.Item>
            <Link to="/">Home</Link>
          </Breadcrumb.Item>
          <Breadcrumb.Item>
            <Link to="/actors">Actor</Link>
          </Breadcrumb.Item>
        </Breadcrumb>
        <div style={{ padding: 24, minHeight: 380 }}>
          <h2>List of Actors</h2>
          <Button onClick={()=>getDanhSachActor("10")}>Get Data</Button>
          <TableActor data={actors} />
        </div>
      </Content>
      <FooterLayout />
    </Layout>
  );
};

export default ActorList;