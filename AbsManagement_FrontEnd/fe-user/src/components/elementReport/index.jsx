import React from 'react'
import { Card } from 'antd'
import ReactDOM from 'react-dom'

function ElementReport(address,full_address) {
  const card = document.createElement('div');

  ReactDOM.render(
    <Card title="Thông tin địa điểm" style={{ width: 300 }}>
      <p>{address}</p>
      <p>{full_address}</p>
    </Card>,card
  );
  return card.innerHTML;
}

export default ElementReport
