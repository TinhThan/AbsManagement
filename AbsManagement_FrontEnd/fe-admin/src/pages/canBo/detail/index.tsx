import { Col, DatePicker, Form, Input, Row, Tooltip } from 'antd';
import { CanBoModel } from '../../../apis/canBo/canBoModel';
import { FormatTime, GetDateTimeByFormat } from '../../../utils';
import ModalDetail from '../../../components/Modal/modalDetail';
import dayjs from 'dayjs';

interface Props {
  onCancel: () => void;
  canBo?: CanBoModel;
  
}

export function ModalDetailCanBo(props: Props): JSX.Element {
  const { onCancel, canBo } = props;
  if (!canBo) {
    return <></>;
  }
  return (
    <ModalDetail
      width={400}
      title={
        <Row wrap={false}>
          <Col flex='auto'>Chi tiết cán bộ</Col>
        </Row>
      }
      modalCancel={onCancel}
    >
      <Col>
        <Form.Item label={"Email"}>
          <Input value={canBo.email} readOnly className='input-code' />
        </Form.Item>
        <Form.Item label={"Họ và tên"}>
          <Tooltip placement='top' style={{ width: '100%' }} title={canBo.hoTen}>
            <Input value={canBo.hoTen} readOnly style={{ textOverflow: 'ellipsis' }} />
          </Tooltip>
        </Form.Item>
        <Form.Item label={"Số điện thoại"}>
          <Input value={canBo.soDienThoai} readOnly />
        </Form.Item>
        <Form.Item label={"Ngày sinh"}>
          <DatePicker value={dayjs(canBo.ngaySinh,FormatTime.DDMMYYYY)} disabled/>
        </Form.Item>
        <Form.Item label={"Quyền"}>
          <Input value={canBo.role} readOnly />
        </Form.Item>
        <Form.Item label={"Nơi công tác"}>
          <Input value={canBo.noiCongTac.join(',')} readOnly />
        </Form.Item>
      </Col>
    </ModalDetail>
  );
}
