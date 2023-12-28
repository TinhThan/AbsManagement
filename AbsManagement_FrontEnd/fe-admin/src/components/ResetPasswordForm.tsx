import { PageLoading } from "@ant-design/pro-components";
import { Form, Spin } from "antd";
import { FC, Suspense, useState } from "react"
import CustomComponent from "../assets/themes/custom-components";
import { authAPI } from "../apis/auth/authAPI";
import { MessageBox } from "../utils/messagebox";
import { useNavigate } from "react-router-dom";

interface ResetPasswordProps {
  emailData: string,
  [key: string]: any;
}

export const ResetPasswordForm: FC<ResetPasswordProps> = ({ emailData, ...props }) => {
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const [loading, setLoading] = useState<boolean>(false);

  const onFinish = async (values: any) => {
    if (values && emailData) {
      setLoading(true);
      try {
        const payload = {
          email: emailData,
          password: values.password
        }
        const response = await authAPI.ResetPassword(payload);
        if (response && response.status === 200) {
          navigate('/login')
        }
      } catch (error: any) {
        MessageBox.Fail(error.message);
      }
      setLoading(false)
    }
  };

  return (
    <Suspense fallback={<PageLoading />}>
      <Spin spinning={loading} className="w-full">
        <div {...props}>
          <p className="text-3xl font-bold mb-6">AbsManagement</p>
          <p className='text-lg mb-6'>
            Reset password
          </p>

          <Form
            layout="vertical"
            name="forgotpassword-form"
            form={form}
            onFinish={onFinish}
          >
            <Form.Item
              name="password"
              label="Password"
              rules={[
                {
                  required: true,
                  message: 'Vui lòng nhập password!',
                },
              ]}
              hasFeedback
            >
              <CustomComponent.InputPasswordStyle />
            </Form.Item>

            <Form.Item
              name="confirm"
              label="Confirm Password"
              dependencies={['password']}
              hasFeedback
              rules={[
                {
                  required: true,
                  message: 'Vui lòng nhập password confirm!',
                },
                ({ getFieldValue }) => ({
                  validator(_, value) {
                    if (!value || getFieldValue('password') === value) {
                      return Promise.resolve();
                    }
                    return Promise.reject(new Error('Password không khớp!'));
                  },
                }),
              ]}
            >
              <CustomComponent.InputPasswordStyle />
            </Form.Item>

            <Form.Item>
              <CustomComponent.BtnSubmitStyle type="primary" htmlType="submit">
                Submit
              </CustomComponent.BtnSubmitStyle>
            </Form.Item>
          </Form>
        </div>
      </Spin>
    </Suspense>
  )
}