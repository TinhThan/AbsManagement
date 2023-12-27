import React from 'react';
import { Form, Input, Button } from 'antd';

interface LoginFormProps {
  onSubmit: (values: any) => void;
}

const LoginForm: React.FC<LoginFormProps> = ({ onSubmit }) => {
  const onFinish = (values: any) => {
    onSubmit(values);
  };

  return (
    <div
      style={{
        background: '#f0f2f5',
        width: '300px',
        margin: '0 auto',
        padding: '20px',
        borderRadius: '4px',
        boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.2)',
      }}
    >
      <h1
        style={{
          textAlign: 'center',
          marginBottom: '20px',
          marginTop: '0',
          color: '#1890ff',
        }}
      >
        Login
      </h1>
      <Form
        name="login-form"
        initialValues={{ remember: true }}
        onFinish={onFinish}
      >
        <Form.Item
          name="email"
          dependencies={['email']}
          rules={[{ required: true, message: 'Vui lòng nhập mail!' },{ type:'email', message: 'Email không hợp lệ!' }]}
        >
          <Input
            placeholder="Email"
            style={{
              backgroundColor: 'white',
              border: '1px solid #d9d9d9',
              borderRadius: '4px',
              marginBottom: '10px',
            }}
          />
        </Form.Item>

        <Form.Item
          name="password"
          dependencies={['password']}
          rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}
        >
          <Input.Password
            placeholder="Password"
            style={{
              backgroundColor: 'white',
              border: '1px solid #d9d9d9',
              borderRadius: '4px',
              marginBottom: '10px',
            }}
          />
        </Form.Item>

        <Form.Item>
          <Button
            type="primary"
            htmlType="submit"
            style={{
              backgroundColor: '#1890ff',
              borderColor: '#1890ff',
              color: 'white',
              borderRadius: '4px',
            }}
          >
            Log in
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default LoginForm;