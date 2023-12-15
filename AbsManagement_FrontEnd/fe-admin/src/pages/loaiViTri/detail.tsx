import { Col, Form, Input, Row, Tooltip } from 'antd';
import { LoaiViTriModel } from '../../apis/loaiViTri/model';
import ModalDetail from '../../components/Modal/modalDetail';
import React from 'react';

interface Props {
    onCancel: () => void;
    loaiViTri?: LoaiViTriModel;
}

export function ModalDetailLoaiViTri(props: Props): JSX.Element {
  const { onCancel, loaiViTri } = props;
  if (!loaiViTri) {
    return <></>;
  }
  return (
    <ModalDetail
      width={400}
      title={
        <Row wrap={false}>
          <Col flex='auto'>Chi tiết loại vị trí</Col>
        </Row>
      }
      modalCancel={onCancel}
    >
      <Col>
        <Form.Item label={"Mã loại vị trí"}>
          <Input value={loaiViTri.ma} readOnly className='input-code' />
        </Form.Item>
        <Form.Item label={"Tên loại ví"}>
          <Tooltip placement='top' style={{ width: '100%' }} title={loaiViTri.ten}>
            <Input value={loaiViTri.ten} readOnly style={{ textOverflow: 'ellipsis' }} />
          </Tooltip>
        </Form.Item>
      </Col>
    </ModalDetail>
  );
}
