import React from 'react';
import LoginForm from '../../components/LoginForm';
import { message } from 'antd';
import { authAPI } from '../../apis/authAPI';
import TokenStorage from '../../apis/storages/tokenStorage';
import RefreshTokenStorage from '../../apis/storages/refreshTokenStorage';
import { useGoRoute } from '../../hooks/useGoRoute';

const Login: React.FC = () => {
  const { goRoute } = useGoRoute();
  const handleLogin = async (values: any) => {
    try {
      const response = await authAPI.Login(values);
      TokenStorage.set(response.accessToken);
      RefreshTokenStorage.set(response.refreshToken);
      goRoute('Home')
    } catch (error: any) {
      console.error('Login error:', error);
      message.error(error.message);
    }
  };

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <LoginForm onSubmit={handleLogin} />
    </div>
  );
};

export default Login;
