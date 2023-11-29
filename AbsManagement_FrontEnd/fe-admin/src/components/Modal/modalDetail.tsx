import { Button, Form, Modal } from 'antd';
import './styles.scss';

interface Props {
  title?: JSX.Element;
  children?: JSX.Element;
  modalCancel?: () => void;
  className?: string;
  width?: number;
}
export default function ModalDetail({ title, children, modalCancel, className, width }: Props): JSX.Element {
  return (
    <Modal
      destroyOnClose
      title={title}
      open
      centered
      forceRender
      onCancel={modalCancel}
      className={`modal-detail ${className}`}
      width={width ? width : 520}
      footer={[
        <Button key='back' onClick={modalCancel}>
          Đóng
        </Button>,
      ]}
    >
      <Form layout='vertical' colon={false}>
        {children}
      </Form>
    </Modal>
  );
}
