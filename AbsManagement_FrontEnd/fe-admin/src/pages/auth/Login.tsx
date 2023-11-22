import React, { Suspense } from 'react';
import LoginForm from '../../components/LoginForm';
import { message } from 'antd';
import { authAPI } from '../../apis/authAPI';
import TokenStorage from '../../apis/storages/tokenStorage';
import RefreshTokenStorage from '../../apis/storages/refreshTokenStorage';
import { useNavigate } from 'react-router-dom';
import { PageLoading } from '@ant-design/pro-components';

const Login: React.FC = () => {
  const navigate = useNavigate();
  const handleLogin = async (values: any) => {
    try {
      const response = await authAPI.Login(values);
      console.log(response);
      TokenStorage.set(response.accessToken);
      RefreshTokenStorage.set(response.refreshToken);
      navigate('/')
    } catch (error: any) {
      console.error('Login error:', error);
      message.error(error.message);
    }
  };

  return (
    <Suspense fallback={<PageLoading/>}>
      <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <LoginForm onSubmit={handleLogin} />
    </div>
    </Suspense>
  );
};

export default Login;
