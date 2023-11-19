import React from 'react';
import { Layout, Menu, Button } from 'antd';
import type { MenuProps } from 'antd';
import { Link } from 'react-router-dom';

const items: MenuProps['items'] = [
  {
    label: (
      <Link to="/actors">Actor</Link>
    ),
    key: 'actor',
  },
]

const { Header } = Layout;

const HeaderLayout: React.FC = () => {
  return (
    <Header
      style={{
        position: 'sticky',
        top: 0,
        zIndex: 1,
        width: '100%',
        display: 'flex',
        justifyContent: 'space-between', // Đặt nút bên phải
        alignItems: 'center',
      }}
    >
      <Menu
        theme="dark"
        items={items}
      />
      <div style={{ display: 'flex', alignItems: 'center' }}>
        <Button type="primary" style={{ marginRight: '8px' }}>
          <Link to="/login">Login</Link>
        </Button>
        <Button type="primary">Register</Button>
      </div>
    </Header>
  );
};

export default HeaderLayout;