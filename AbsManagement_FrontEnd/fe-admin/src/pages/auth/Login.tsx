import React, { Suspense } from 'react';
import LoginForm from '../../components/LoginForm';
import { message } from 'antd';
// import { authAPI } from '../../apis/auth/authAPI';
import TokenStorage from '../../storages/tokenStorage';
import RefreshTokenStorage from '../../storages/refreshTokenStorage';
import { useNavigate } from 'react-router-dom';
import { PageLoading } from '@ant-design/pro-components';
import UserInfoStorage from '../../storages/user-info';
import { RoleCanBo } from '../canBo';
import { MessageBox } from '../../utils/messagebox';

const Login: React.FC = () => {
  const navigate = useNavigate();
  const handleLogin = async (values: any) => {
    try {
      // const response = await authAPI.Login(values);
      // TokenStorage.set(response.data.accessToken);
      // RefreshTokenStorage.set(response.data.refreshToken);
      TokenStorage.set("1");
      RefreshTokenStorage.set("2");
      UserInfoStorage.set({
        taiKhoan: '1',
        noiCongTac: [],
        email: '1',
        hoTen: '1',
        role:RoleCanBo.CanBoSo
      });
      navigate('/')
    } catch (error: any) {
      console.log("error",error)
      MessageBox.Fail(error.message);
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
