import React from 'react';
import { Form, Input, Button } from 'antd';
import styled from 'styled-components';

interface LoginFormProps {
  onSubmit: (values: any) => void;
  [key: string]: any;
}

const InputStyle = styled(Input)`
  min-height: 40px;
`

const InputPasswordStyle = styled(Input.Password)`
  min-height: 40px;
`

const BtnSubmitStyle = styled(Button)`
  width: 100%;
  min-height: 38px;
  background-color: #1890ff;
  margin-top: 2rem;
`

const LoginForm: React.FC<LoginFormProps> = ({ onSubmit, ...props }) => {
  const onFinish = (values: any) => {
    onSubmit(values);
  };

  return (
    <div {...props}>
      <p className="text-4xl font-bold">Welcome back</p>
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
          <InputStyle placeholder="Email" />
        </Form.Item>

        <Form.Item
          name="password"
          dependencies={['password']}
          rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}
        >
          <InputPasswordStyle placeholder="Password" />
        </Form.Item>

        <div className='text-right'>
          <a className='inline text-[#1890ff]' href="/">Forgot password</a>
        </div>

        <Form.Item>
          <BtnSubmitStyle type="primary" htmlType="submit">
            Log in
          </BtnSubmitStyle>
          <div>
            Don't have an account <a className='inline text-[#1890ff]' href="/">sign up</a>
          </div>
        </Form.Item>
      </Form>
    </div>
  );
};

export default LoginForm;