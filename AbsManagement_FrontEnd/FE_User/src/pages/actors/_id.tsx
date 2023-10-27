import React, { useEffect, useState } from 'react';
import { Layout, Breadcrumb, message, Button } from 'antd';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { Actor } from '../../components/TableActor'
import TableActorDetail from '../../components/TableActorDetail';

import HeaderLayout from '../../layouts/Header';
import FooterLayout from '../../layouts/Footer';
import TokenStorage from '../../apis/storages/tokenStorage';
import { actorApi } from '../../apis/actorAPI';
const { Content } = Layout;

const ActorDetailPage: React.FC = () => {
  const navigate = useNavigate();
  const accessToken = TokenStorage.get();
  const { id } = useParams<{ id: string }>();
  const [actor, setActor] = useState<Actor | null>(null);
  const [loading, setLoading] = useState(false);

  const actorDetail = async () => {
    try {
      const response = await actorApi.ChiTiet(Number(id));
      setActor(response);
      setLoading(false);
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
  }, []);

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
          <h1>List Films for Actor</h1>
          <Button onClick={actorDetail}>Get Data</Button>
          {loading ? (
            <p>Loading...</p>
            ) : actor ? (
              <div>
              <h2>{actor?.firstName}</h2>
              <TableActorDetail data={actor.films} />
            </div>
          ) : (
            <p>Actor not found.</p>
          )}
        </div>
      </Content>
      <FooterLayout />
    </Layout>
  );
};

export default ActorDetailPage;