import { Form, Spin } from "antd";
import { FC, Suspense, useState } from "react";
import CustomComponent from "../assets/themes/custom-components";
import ModalOTPInput from "./Modal/modalOTPInput";
import { PageLoading } from "@ant-design/pro-components";
import { MessageBox } from "../utils/messagebox";
import { ValidOTPPayload, authAPI } from "../apis/auth/authAPI";
import { Link, createSearchParams, useNavigate } from "react-router-dom";

interface ForgotPasswordProps {
  [key: string]: any;
}

export const ForgotPasswordForm: FC<ForgotPasswordProps> = ({ onSubmit, ...props }) => {
  const [form] = Form.useForm();
  const [loading, setLoading] = useState<boolean>(false);
  const [openModal, setOpenModal] = useState<boolean>(false);
  const [confirmLoading, setConfirmLoading] = useState<boolean>(false);
  const [statusOTP, setStatusOTP] = useState<string>('');
  const navigate = useNavigate();

  const onFinish = async (values: any) => {
    if (values) {
      setLoading(true);
      try {
        const response = await authAPI.ForgotPassword(values);
        if (response && response.status === 200) {
          setOpenModal(true)
        }
      } catch (error: any) {
        MessageBox.Fail(error.message);
      }
      setLoading(false)
    }
  };
  const handleCancelModal = (status: boolean) => {
    setOpenModal(status)
  }
  const handleFinishOTP = async (value: string) => {
    if (value) {
      setConfirmLoading(true);
      try {
        const field = form.getFieldsValue(['email']);
        const payload: ValidOTPPayload = {
          email: field.email,
          OTPCode: value
        }
        const response = await authAPI.ValidationOTP(payload);
        if (response && response.status === 200) {
          setStatusOTP('')
          setOpenModal(false);
          navigate({
            pathname: "/reset-password",
            search: `?${createSearchParams({
              email: field.email
            })}`
          })
        }
      } catch (error: any) {
        MessageBox.Fail(error.message);
        setOpenModal(false);
        setStatusOTP(error.message);
      }
      setConfirmLoading(false)
    }
  }

  return (
    <Suspense fallback={<PageLoading />}>
      <Spin spinning={loading}>
        <div {...props}>
          <p className="text-3xl font-bold mb-6">AbsManagement</p>
          <p className='text-lg mb-6'>
            Enter the email address associated with your account and we'll send you a link to reset your password.
          </p>

          <Form
            name="forgotpassword-form"
            form={form}
            onFinish={onFinish}
          >
            <Form.Item
              name="email"
              dependencies={['email']}
              rules={[{ required: true, message: 'Vui lòng nhập mail!' }, { type: 'email', message: 'Email không hợp lệ!' }]}
            >
              <CustomComponent.InputStyle placeholder="Email" />
            </Form.Item>

            <Form.Item>
              <CustomComponent.BtnSubmitStyle type="primary" htmlType="submit">
                Continue
              </CustomComponent.BtnSubmitStyle>
            </Form.Item>
          </Form>

          <ModalOTPInput
            dataEmail={form.getFieldsValue(['email'])}
            openStatus={openModal}
            confirmLoading={confirmLoading}
            statusOTPValid={statusOTP}
            handleCancel={handleCancelModal}
            handleFinishOTP={handleFinishOTP}
          />
        </div>
      </Spin>
    </Suspense>
  )
}