import React from 'react'
import { Card } from 'antd'
import ReactDOM from 'react-dom'
import './style.scss'
import { DiemDatQuangCaoModel } from '../../apis/diemDatQuangCao/diemDatQuangCaoModel';

function SpaceInfo(space: DiemDatQuangCaoModel) {
  const card = document.createElement('div');

  ReactDOM.render(
    <Card title="Điểm đặt quảng cáo" style={{ width: 250 }} className='info-popup'>
        <p className='p-ant'><strong>Loại vị trí: </strong>{space.tenLoaiViTri}</p>
        <p className='p-ant'><strong>Hình thức quảng cáo: </strong>{space.tenHinhThucQuangCao}</p>
        <p className='p-ant'><strong>Địa điểm: </strong>{space.diaChi}</p>
    </Card>,card
  );
  return card.innerHTML;
}

export default SpaceInfo
