import { Col, Form, Input, Row, Tooltip } from 'antd';
import ModalDetail from '../../components/Modal/modalDetail';
import React from 'react';
import { LoaiBangQuangCaoModel } from '../../apis/loaiBangQuangCao/model';

interface Props {
    onCancel: () => void;
    loaiBangQuangCao?: LoaiBangQuangCaoModel;
}

export function ModalDetailLoaiBangQuangCao(props: Props): JSX.Element {
  const { onCancel, loaiBangQuangCao } = props;
  if (!loaiBangQuangCao) {
    return <></>;
  }
  return (
    <ModalDetail
      width={400}
      title={
        <Row wrap={false}>
          <Col flex='auto'>Chi tiết loại bảng quảng cáo</Col>
        </Row>
      }
      modalCancel={onCancel}
    >
      <Col>
        <Form.Item label={"Mã loại bảng quảng cáo"}>
          <Input value={loaiBangQuangCao.ma} readOnly className='input-code' />
        </Form.Item>
        <Form.Item label={"Tên loại bảng quảng cáo"}>
          <Tooltip placement='top' style={{ width: '100%' }} title={loaiBangQuangCao.ten}>
            <Input value={loaiBangQuangCao.ten} readOnly style={{ textOverflow: 'ellipsis' }} />
          </Tooltip>
        </Form.Item>
      </Col>
    </ModalDetail>
  );
}
