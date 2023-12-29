import React from 'react'
import { Card , Button, Image, Space} from 'antd';
import { ExclamationCircleFilled} from '@ant-design/icons'

function CardPoint({location, onCreateReportClick}) {
    console.log("location point",location)
  return (
    <Card title="Thông tin địa điểm" bordered={true} className='info-location'>
        {location.tenLoaiViTri && <p className='p-ant'><strong>Loại vị trí: </strong>{location.tenLoaiViTri}</p>}
        {location.tenHinhThucQuangCao && <p className='p-ant'><strong>Hình thức quảng cáo: </strong>{location.tenHinhThucQuangCao}</p>}
        <p className='p-ant'><strong>Địa điểm: </strong>{location.diaChi}</p>
        {location.danhSachHinhAnh && location.danhSachHinhAnh.length > 0 && 
            <Image.PreviewGroup>
                <Space direction='vertical' size={0} style={{marginBottom:'10px'}}>
                    {location.danhSachHinhAnh.length > 1 ? (
                    <Space size={5}>
                        {location.danhSachHinhAnh.map((t, index) => {
                            return <Image key={index.toString()} width={120} height={120} src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} alt={index.toString()} />;
                        })}
                    </Space>
                    ):<Image width={150} height={150}  src={`${process.env.REACT_APP_BASE_API}Upload/image/${location.danhSachHinhAnh[0]}`} alt='avatar' />}
                </Space>
            </Image.PreviewGroup> }
        <Button icon={<ExclamationCircleFilled />} onClick={onCreateReportClick} danger>Báo cáo vi phạm</Button>
    </Card>
  );
}

export default CardPoint
