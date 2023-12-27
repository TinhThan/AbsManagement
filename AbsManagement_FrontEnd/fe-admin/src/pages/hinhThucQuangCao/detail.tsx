import { Col, Form, Input, Row, Tooltip } from 'antd';
import ModalDetail from '../../components/Modal/modalDetail';
import React from 'react';
import { HinhThucQuangCaoModel } from '../../apis/hinhThucQuangCao/model';

interface Props {
    onCancel: () => void;
    hinhThucQuangCao?: HinhThucQuangCaoModel;
}

export function ModalDetailHinhThucQuangCao(props: Props): JSX.Element {
  const { onCancel, hinhThucQuangCao } = props;
  if (!hinhThucQuangCao) {
    return <></>;
  }
  return (
    <ModalDetail
      width={400}
      title={
        <Row wrap={false}>
          <Col flex='auto'>Chi tiết hình thức quảng cáo</Col>
        </Row>
      }
      modalCancel={onCancel}
    >
      <Col>
        <Form.Item label={"Mã hình thức quảng cáo"}>
          <Input value={hinhThucQuangCao.ma} readOnly className='input-code' />
        </Form.Item>
        <Form.Item label={"Tên hình thức quảng cáo"}>
          <Tooltip placement='top' style={{ width: '100%' }} title={hinhThucQuangCao.ten}>
            <Input value={hinhThucQuangCao.ten} readOnly style={{ textOverflow: 'ellipsis' }} />
          </Tooltip>
        </Form.Item>
      </Col>
    </ModalDetail>
  );
}
