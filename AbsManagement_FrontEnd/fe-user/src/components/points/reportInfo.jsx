import React from 'react'
import { Card } from 'antd'
import ReactDOM from 'react-dom'
import './style.scss'

function ReportInfo(report) {
  const card = document.createElement('div');

  ReactDOM.render(
    <Card title="Báo cáo vi phạm" style={{ width: 250 }} className='info-popup'>
        <p className='p-ant'><strong>Hình thức báo cáo: </strong>{report.tenHinhThucBaoCao}</p>
    </Card>,card
  );
  return card.innerHTML;
}

export default ReportInfo
