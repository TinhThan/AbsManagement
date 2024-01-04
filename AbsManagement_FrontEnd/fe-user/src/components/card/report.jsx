import React from 'react'
import { Card , Button, Image, Space} from 'antd';
import { ExclamationCircleFilled} from '@ant-design/icons'

function CardReport({report}) {
    return (
        // <Card title="Thông tin báo cáo vi phạm" bordered={true} className='info-location'>
        //     <p className='p-ant'><strong>Hình thức báo cáo: </strong>{report.tenHinhThucBaoCao}</p>
        //     <p className='p-ant'><strong>Tên người tố cáo: </strong>{report.hoTen}</p>
        //     <p className='p-ant'><strong>Số điện thoại: </strong>{report.soDienThoai}</p>
        //     <p className='p-ant'><strong>Email: </strong>{report.email}</p>
        //     <p className='p-ant'><strong>Địa điểm: </strong>{report.diaChi}</p>
        //     {report.danhSachHinhAnh && report.danhSachHinhAnh.length > 0 && 
        //         <Image.PreviewGroup>
        //             <Space direction='vertical' size={0} style={{marginBottom:'10px'}}>
        //                 {report.danhSachHinhAnh.length > 1 ? (
        //                 <Space size={5}>
        //                     {report.danhSachHinhAnh.map((t, index) => {
        //                         return <Image key={index.toString()} width={120} height={120} src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} alt={index.toString()} />;
        //                     })}
        //                 </Space>
        //                 ):<Image width={150} height={150}  src={`${process.env.REACT_APP_BASE_API}Upload/image/${report.danhSachHinhAnh[0]}`} alt='avatar' />}
        //             </Space>
        //         </Image.PreviewGroup> }
        //     <Button icon={<ExclamationCircleFilled />} onClick={onCreateReportClick} danger>Báo cáo vi phạm</Button>
        // </Card>
        <></>
    );
}

export default CardReport
