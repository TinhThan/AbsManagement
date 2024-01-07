import React from 'react'
import { Button, Card } from 'antd'
import ReactDOM from 'react-dom'
import './style.scss'
import { tinhTrangReports } from '../map';

function ReportInfo(report) {
  const card = document.createElement('div');
  console.log("tinh trnạg",tinhTrangReports[report.idTinhTrang])
  ReactDOM.render(
    <Card title="Báo cáo vi phạm" style={{ width: 250 }} className='info-popup'>
        <p className='p-ant'><strong>Hình thức báo cáo: </strong>{report.tenHinhThucBaoCao}</p>
        <p className='p-ant'><strong>Tình trạng: </strong>{tinhTrangReports[report.idTinhTrang]}</p>
        <Button type='primary'  id='report-popup'>Chi tiết</Button>
    </Card>,card
  );
  return card.innerHTML;
}

export default ReportInfo
