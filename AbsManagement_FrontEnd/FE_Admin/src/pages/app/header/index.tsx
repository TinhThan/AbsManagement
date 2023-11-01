import { Col, Row, Space } from 'antd';
import { MenuFoldOutlined, MenuUnfoldOutlined } from '@ant-design/icons';
import { PopoverUserInfo } from './components/popoverUserInfo';
import { UserStorage } from '../../../apis/models/user';

interface Props {
    logOutClick: () => void;
    userInfo: UserStorage;
    collapse: boolean;
    onCollapse: () => void;
}
export function HeaderLayout(props: Props): JSX.Element {
    const { logOutClick,userInfo, collapse, onCollapse } = props;

    return (
        <div className='header-web-page'>
            <Col className='title-page-name' xs={{ span: 0 }} sm={{ span: 2 }} md={{ span: 2 }} lg={{ span: 2 }} xl={{ span: 2 }} xxl={{ span: 2 }}>
                {collapse ? (
                <span title={"Mở menu"}>
                    <MenuUnfoldOutlined
                    className='icon-menu-fold'
                    onClick={() => {
                        onCollapse();
                    }}
                    />
                </span>
                ) : (
                <span title={"Đóng menu"}>
                    <MenuFoldOutlined
                    className='icon-menu-fold'
                    onClick={() => {
                        onCollapse();
                    }}
                    />
                </span>
                )}
            </Col>
            {/* <Col className='title-page-name' xs={{ span: 24 }} sm={{ span: 10 }} md={{ span: 10 }} lg={{ span: 10 }} xl={{ span: 10 }} xxl={{ span: 10 }}>
                <MenuHierarchy />
            </Col> */}
            <Row align='middle' justify='end'>
                <Col>
                <Space direction='horizontal' size={20} align='center' className='space-header-layout'>
                    <Col xs={0} sm={0} md={0} lg={24} xl={24} xxl={24}>
                    <Space direction='vertical' className='space-title' align='start'>
                        <span>
                        Người dùng A
                        </span>
                    </Space>
                    </Col>
                    {/* <NotificationHeader /> */}
                    <PopoverUserInfo userInfo={userInfo} logOutClick={logOutClick} />
                </Space>
                </Col>
            </Row>
        </div>
    );
}
