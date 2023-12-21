import React from 'react'
import { Card, Typography } from 'antd'
import ReactDOM from 'react-dom'
const { Text } = Typography;

function SpaceInfo(space) {
  const card = document.createElement('div');

  ReactDOM.render(
    <Card title="Thông tin điểm đặt quảng cáo" style={{ width: 300 }}>
      <Text strong>{space.tenLoaiViTri}</Text>
      <Text strong>{space.tenHinhThucQuangCao}</Text>
      <p>{space.diaChi}, {space.phuong}, {space.quan}</p>
    </Card>,card
  );
  return card.innerHTML;
}

export default SpaceInfo
