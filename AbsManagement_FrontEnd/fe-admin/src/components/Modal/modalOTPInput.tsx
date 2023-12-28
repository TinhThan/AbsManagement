import { Modal } from "antd";
import { InputOTP } from "antd-input-otp";
import { FC, useEffect, useState } from "react"

interface ModalOTPInputProps {
  dataEmail: {
    email: string
  },
  openStatus: boolean,
  confirmLoading: boolean,
  statusOTPValid: string,
  handleCancel: (status: boolean) => void,
  handleFinishOTP: (value: string) => void
}

const ModalOTPInput: FC<ModalOTPInputProps> = ({ dataEmail, openStatus, confirmLoading, statusOTPValid, handleCancel, handleFinishOTP }) => {
  const [value, setValue] = useState<string[]>([]);
  const [open, setOpen] = useState<boolean>(false);
  const [statusOTP, setStatusOTP] = useState<string>('');

  const handleFinish = (otp) => {
    if (otp) {
      handleFinishOTP(otp.join('').toString())
    }
  }

  useEffect(() => {
    setOpen(openStatus)
    setValue([])
  }, [openStatus])

  useEffect(() => {
    setStatusOTP(statusOTPValid)
  }, [statusOTPValid])

  return (
    <Modal
      title="AbsManagement OTP"
      open={open}
      confirmLoading={confirmLoading}
      onCancel={() => handleCancel(false)}
      footer={false}
    >
      <p className="text-center mt-8">6 degit code sent to <strong>{ dataEmail.email }</strong></p>
      <InputOTP onChange={setValue} value={value} autoSubmit={handleFinish} length={6} inputClassName="mt-2"/>
      <p>{statusOTP}</p>
    </Modal>
  );
}

export default ModalOTPInput