import React from 'react'
import { Card , Button, Image, Space, Typography, Divider} from 'antd';
import { ExclamationCircleFilled} from '@ant-design/icons'
import { renderModal } from '../../utils/render-modal'
// import ModalCreateReport from '../modal/createReport'
const { Text , Title} = Typography; 

function CardSurface({surfaces, location}) {
    function onCreateReportClick(idBangQuangCao){
        if(location){
            // const _root = renderModal(<ModalCreateReport onCancel={() => {
            //     console.log("cancel")
            //     _root?.unmount()
            // }} location={location} idBangQuangCao={idBangQuangCao}/> );
        }
    }

  return (
    <Card title="Thông tin bảng quảng cáo" bordered={true} className='info-surface'>
                {
                    surfaces.length > 0 ? 
                    surfaces.map((surface,index) =>
                        <>
                            <Title level={5} style={{marginTop:'5px'}}>Bảng báo cáo {index +1}</Title>
                            <p className='p-ant'><strong>Loại bảng quảng cáo: </strong>{surface.tenLoaiBangQuangCao}</p>
                            <p className='p-ant'><strong>Kích thước: </strong>{surface.kichThuoc}</p>
                            {surface.danhSachHinhAnh && surface.danhSachHinhAnh.length > 0 && 
                            <Image.PreviewGroup>
                                <Space direction='vertical' size={0} style={{marginBottom:'10px'}}>
                                    {surface.danhSachHinhAnh.length > 1 ? (
                                    <Space size={5}>
                                        {surface.danhSachHinhAnh.map((t, index) => {
                                            return <Image key={index.toString()} width={120} height={120} src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} alt={index.toString()} />;
                                        })}
                                    </Space>
                                    ) :                                     
                                    <Image width={150} height={150}  src={`${process.env.REACT_APP_BASE_API}Upload/image/${surface.danhSachHinhAnh[0]}`} alt='avatar' />}
                                </Space>
                            </Image.PreviewGroup> }
                            <Button icon={<ExclamationCircleFilled />} danger onClick={()=>onCreateReportClick(surface.id)}>Báo cáo vi phạm</Button>
                            <Divider style={{marginTop:'5px', marginBottom:'5px'}}/> 
                        </>
                    ) :
                    <>
                        <Text strong>Chưa có dữ liệu.</Text>
                        <Text>Vui lòng chọn điểm trên bảng đồ để xem.</Text>
                    </> 
                }
                </Card>
  );
}

export default CardSurface
