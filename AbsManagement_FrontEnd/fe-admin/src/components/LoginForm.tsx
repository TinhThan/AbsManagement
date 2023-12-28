import React from 'react';
import { Form } from 'antd';
import CustomComponent from '../assets/themes/custom-components';
import { Link } from 'react-router-dom';

interface LoginFormProps {
  onSubmit: (values: any) => void;
  [key: string]: any;
}

const LoginForm: React.FC<LoginFormProps> = ({ onSubmit, ...props }) => {
  const onFinish = (values: any) => {
    onSubmit(values);
  };

  return (
    <div {...props}>
      <p className="text-3xl font-bold">Welcome AbsManagement</p>
      <p className='text-lg mb-6'>Login to the Dashboard</p>
      <Form
        name="login-form"
        initialValues={{ remember: true }}
        onFinish={onFinish}
      >
        <Form.Item
          name="email"
          dependencies={['email']}
          rules={[{ required: true, message: 'Vui lòng nhập mail!' }, { type: 'email', message: 'Email không hợp lệ!' }]}
        >
          <CustomComponent.InputStyle placeholder="Email" />
        </Form.Item>

        <Form.Item
          name="password"
          dependencies={['password']}
          rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}
        >
          <CustomComponent.InputPasswordStyle placeholder="Password" />
        </Form.Item>

        <div className='text-right'>
          <Link className='inline text-[#1890ff]' to="/forgot-password">Forgot password</Link>
        </div>

        <Form.Item>
          <CustomComponent.BtnSubmitStyle type="primary" htmlType="submit">
            Log in
          </CustomComponent.BtnSubmitStyle>
          <div>
            Don't have an account? <Link to="/" className='inline text-[#1890ff]'>Sign up</Link>
          </div>
        </Form.Item>
      </Form>
    </div>
  );
};

export default LoginForm;