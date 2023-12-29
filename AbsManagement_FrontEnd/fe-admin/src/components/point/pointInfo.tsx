import React from 'react'
import { Card } from 'antd'
import ReactDOM from 'react-dom'

function PointInfo(full_address) {
  const card = document.createElement('div');

  ReactDOM.render(
    <Card title="Thông tin địa điểm" style={{ width: 250 }} className='info-popup'>
        <p className='p-ant'><strong>Địa điểm: </strong>{full_address}</p>
    </Card>,card
  );
  return card.innerHTML;
}

export default PointInfo
