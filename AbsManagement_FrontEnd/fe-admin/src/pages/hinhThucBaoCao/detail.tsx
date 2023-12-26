import { Col, Form, Input, Row, Tooltip } from 'antd';
import ModalDetail from '../../components/Modal/modalDetail';
import React from 'react';
import { HinhThucBaoCaoModel } from '../../apis/hinhThucBaoCao/model';

interface Props {
    onCancel: () => void;
    HinhThucBaoCao?: HinhThucBaoCaoModel;
}

export function ModalDetailHinhThucBaoCao(props: Props): JSX.Element {
  const { onCancel, HinhThucBaoCao } = props;
  if (!HinhThucBaoCao) {
    return <></>;
  }
  return (
    <ModalDetail
      width={400}
      title={
        <Row wrap={false}>
          <Col flex='auto'>Chi tiết hình thức báo cáo</Col>
        </Row>
      }
      modalCancel={onCancel}
    >
      <Col>
        <Form.Item label={"Mã hình thức báo cáo"}>
          <Input value={HinhThucBaoCao.ma} readOnly className='input-code' />
        </Form.Item>
        <Form.Item label={"Tên hình thức báo cáo"}>
          <Tooltip placement='top' style={{ width: '100%' }} title={HinhThucBaoCao.ten}>
            <Input value={HinhThucBaoCao.ten} readOnly style={{ textOverflow: 'ellipsis' }} />
          </Tooltip>
        </Form.Item>
      </Col>
    </ModalDetail>
  );
}
