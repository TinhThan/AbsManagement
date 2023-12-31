import { Button, Card, Col, Form, Input, Row, Space, Image, Empty, Modal } from "antd";
import parse from 'html-react-parser';
import { tinhTrangBaoCaoViPham } from "./list";
import { Editor } from "@tinymce/tinymce-react";
import { useRef, useState } from "react";
import { Notification } from "../../utils";
import { baoCaoViPhamAPI } from "../../apis/baoCaoViPham";

export default function ModalDetailBaoCaoViPham({onCancel,baoCaoViPham}): JSX.Element {
    const [loading, setLoading] = useState(false);
    const editorRef = useRef<any>(null)
    
    async function handleOk(){
        if(!editorRef.current.getContent()){
            Notification.Warning("Nội dung xử lý không được rỗng.");
        }
        setLoading(true);
        const payload = {
            noiDungXyLy: editorRef.current.getContent(),
            idTinhTrang: 'DangXuLy'
        }

        await baoCaoViPhamAPI.CapNhat(baoCaoViPham.id, payload);
        setLoading(false);
    };
    return (
        <>
        { baoCaoViPham && <Modal
            getContainer={() => document.getElementById('modal-container') || document.body}
            title={(<>Báo cáo vi phạm 
            <p>{tinhTrangBaoCaoViPham[baoCaoViPham?.idTinhTrang]}</p></>)}
            keyboard={false}
            maskClosable={false}
            destroyOnClose
            open
            forceRender
            onCancel={()=>{
                onCancel();
            }}
            width={1000}
            footer={[
            <Button key='back' onClick={()=>{
                onCancel()
            }}>
                Đóng
            </Button>,
            <Button
            key='submit'
            type='primary'
            loading={loading}
            onClick={(e) => {
                e.preventDefault();
                handleOk();
            }}
            >
            Xử lý báo cáo
            </Button>,
            ]}
        >
            <Form
                    layout="vertical"
                >
                    <Row gutter={[5,5]}>
                        <Col span={12}>
                            <Space direction='vertical' style={{width:'100%'}}>
                                <Card
                                    title={<b>Thông tin người báo cáo</b>}
                                    bordered={false}
                                >
                                    <Form.Item label='Họ tên'>
                                        <Input value={baoCaoViPham?.hoTen} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Email'>
                                        <Input value={baoCaoViPham?.email} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Số điện thoại'>
                                        <Input value={baoCaoViPham?.soDienThoai} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Nội dung vi phạm'>
                                        <div>
                                            {parse(baoCaoViPham?.noiDung)}
                                        </div>
                                    </Form.Item>
                                    <Form.Item label="Danh sách hình ảnh">
                                        {baoCaoViPham && baoCaoViPham.danhSachHinhAnh && baoCaoViPham.danhSachHinhAnh.length > 0 ? 
                                            <Image.PreviewGroup>
                                                <Space direction='vertical' size={0} style={{marginBottom:'10px'}}>
                                                    {baoCaoViPham.danhSachHinhAnh.length > 1 ? (
                                                    <Space size={5}>
                                                        {baoCaoViPham.danhSachHinhAnh.map((t, index) => {
                                                            return <Image key={index.toString()} width={120} height={120}
                                                                    src={`${process.env.REACT_APP_BASE_API}Upload/image/${t}`} />;
                                                        })}
                                                    </Space>
                                                    ):<Image width={150} height={150}  src={`${process.env.REACT_APP_BASE_API}Upload/image/${baoCaoViPham.danhSachHinhAnh[0]}`} alt='avatar' />}
                                                </Space>
                                            </Image.PreviewGroup> :
                                            <Empty/>}
                                    </Form.Item>
                                </Card>
                            </Space>
                        </Col>
                        <Col span={12}>
                            <Space direction='vertical' style={{width:'100%'}}>
                                {baoCaoViPham?.idCanBoXuLy && baoCaoViPham.idCanBoXuLy > 0 ? 
                                <Card
                                    title={<b>Thông tin cán bộ xử lý</b>}
                                    bordered={false}
                                >
                                    
                                    <Form.Item label='Họ tên'>
                                        <Input value={baoCaoViPham?.hoTenCanBoXuLy} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Email'>
                                        <Input value={baoCaoViPham?.emailCanBoXuLy} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Số điện thoại'>
                                        <Input value={baoCaoViPham?.soDienThoaiCanBoXuLy} readOnly />
                                    </Form.Item>
                                    <Form.Item label='Nội dung xử lý'>
                                        <div>
                                            {parse(baoCaoViPham.noiDungXuLy)}
                                        </div>
                                    </Form.Item>
                                </Card> :
                                <Card title={<b>Nội dung xử lý</b>}
                                bordered={false}>
                                    <Editor
                                        onInit={(evt, editor) => editorRef.current = editor}
                                        initialValue="<p>Nhập nội dung xử lý tại đây</p>"
                                        init={{
                                        language:'vi_VN',
                                        height: 200,
                                        menubar: false,
                                        plugins: [
                                            'advlist autolink lists link image charmap print preview anchor',
                                            'searchreplace visualblocks code fullscreen',
                                            'insertdatetime media table paste code help wordcount'
                                        ],
                                        toolbar: 'undo redo | formatselect | ' +
                                        'bold italic backcolor | alignleft aligncenter ' +
                                        'alignright alignjustify | bullist numlist outdent indent | ' +
                                        'removeformat | help',
                                        content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }'
                                        }}
                                    />
                                </Card>}
                            </Space>
                        </Col>
                    </Row>
                </Form>
        </Modal>}</>
    );
}