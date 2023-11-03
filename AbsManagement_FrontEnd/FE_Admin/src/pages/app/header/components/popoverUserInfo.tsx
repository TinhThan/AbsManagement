import { useState } from 'react';
import { Avatar, Col, Popover, Space, Typography } from 'antd';
import { AppstoreAddOutlined, LogoutOutlined, ProfileOutlined, UserSwitchOutlined } from '@ant-design/icons';
import avatar from 'assets/images/avatar.webp';
import { UserStorage } from '../../../../apis/models/user';
import { useResponsive } from '../../../../hooks/useResponsive';
import { MessageBox } from '../../../../utils/messagebox';
import { ConfigRoute } from '../../../../routes/ConfigRoute';
import { useGoRoute } from '../../../../hooks/useGoRoute';
import { messageSystem } from '../../../constant/messageSystem';


interface Props {
  userInfo: UserStorage;
  logOutClick: () => void;
}
export function PopoverUserInfo(props: Props): JSX.Element {
  const { userInfo, logOutClick } = props;
  const  {goRoute} = useGoRoute();
  const [isVisible, setIsVisible] = useState(false);
  const screens = useResponsive();

  function onLogOut() {
      MessageBox.Confirm({
        content: "Bạn có muốn đăng xuất khỏi hệ thống không?",
        onOk() {
          logOutClick();
        },
      });
    setIsVisible(false);
  }
  return (
    <Popover
      placement='bottom'
      overlayClassName='ant-popover-custom'
      title={
        <Space direction='vertical' align='start' style={{ width: '100%' }}>
          <b>Thông tin cá nhân</b>
          <Typography.Text ellipsis>{`Xin chào ${userInfo.tenNhanVien}!`}</Typography.Text>
          {/* <Col xs={24} sm={24} md={24} lg={0} xl={0} xxl={0}>
            <Space direction='vertical' size={0}>
              <TitleActiveComponent title={userInfo.tenGoiLicense} />
              <span style={{ color: ConfigSystem.ThemeColor }}>
                <RenderTimeLicense ngayHetHan={userInfo.ngayHetHan} />
              </span>
            </Space>
          </Col> */}
        </Space>
      }
      content={
        <>
          <Space direction='vertical' className='popover-info-user'>
            <span
              onClick={() => {
                goRoute("ThongTinCaNhan");
                setIsVisible(false);
              }}
              className='btn-info'
            >
              <ProfileOutlined rev={undefined} /> {messageSystem.ThongTinCaNhan}
            </span>
            <span role='button' tabIndex={0} onClick={onLogOut} className='btn-info' aria-hidden='true'>
              <LogoutOutlined rev={undefined} /> {messageSystem.DangXuat}
            </span>
          </Space>
        </>
      }
      trigger='click'
      open={isVisible}
      onOpenChange={setIsVisible}
    >
      <Space direction='horizontal' align='center' className='space-user'>
        <Avatar src={<img width='100%' height='100%' srcSet={"linkavatar"} alt='avatar' />} />
        {!screens.xs && <span className='title-user-name'>{userInfo.tenNhanVien}</span>}
      </Space>
    </Popover>
  );
}
