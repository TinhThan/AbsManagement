import React, { Suspense, useState } from 'react';
import LoginForm from '../../components/LoginForm';
import { Spin } from 'antd';
import { useNavigate } from 'react-router-dom';
import { PageLoading } from '@ant-design/pro-components';
import UserInfoStorage from '../../storages/user-info';
import { MessageBox } from '../../utils/messagebox';
import { authAPI } from '../../apis/auth/authAPI';

const Login: React.FC = () => {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  
  const handleLogin = async (values: any) => {
    setLoading(true);
    try {
      const response = await authAPI.Login(values);
      console.log("reospone ! 200",response)
      if(response && response.status === 200)
      {
        UserInfoStorage.set({...response.data});
        navigate('/')
      }
    } catch (error: any) {
      MessageBox.Fail(error.message);
    }
    setLoading(false)
  };

  return (
    <Suspense fallback={<PageLoading/>}>
      <Spin spinning={loading}>
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
            <LoginForm onSubmit={handleLogin} />
          </div>
      </Spin>
    </Suspense>
  );
};

export default Login;
