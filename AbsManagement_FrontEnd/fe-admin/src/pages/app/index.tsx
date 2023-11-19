import { ProLayout } from "@ant-design/pro-components";
import { useEffect, useState } from "react";
import TokenStorage from "../../apis/storages/tokenStorage";
import MenuLayout from "./menu";
import UserInfoStorage from "../../apis/storages/user-info";
import { HeaderLayout } from "./header";
import { Button, Space } from "antd";
import { useResponsive } from "../../hooks/useResponsive";
import { Outlet, Navigate, redirect, useNavigate } from 'react-router-dom';
import './styles.scss';

export default function App(): JSX.Element {
    const [collapse, setCollapse] = useState(false);
    const navigate = useNavigate();
    const screens = useResponsive();

    const accessToken = TokenStorage.get();

    async function logOutClick() {
        // await authApi
        //   .LogOut()
        //   .then((response) => Notification.Success(t(response)))
        //   .finally(() => {
        //     setIsLogout(true);
        // });
        localStorage.clear();
        navigate('/login');
      }

    function renderHeader() {
        return (
          <HeaderLayout
            collapse={collapse}
            onCollapse={() => {
              setCollapse(!collapse);
            }}
            logOutClick={logOutClick}
            userInfo={UserInfoStorage.get()}
          />
        );
      }

    function renderMenuExtraRender(collapsed?: boolean) {
    if (collapsed) {
        return <></>;
    }
    return (
        <Space direction='vertical' size={10} align='center'>
        <Button
            onClick={() => {
              redirect('Home');
            }}
            style={{ cursor: 'pointer' }}
        />
        </Space>
        
    );
    }

    function renderMenu() {
        return <MenuLayout collapse={collapse}/>;
      }

    function renderMenuHeaderRender(collapsed?: boolean) {
        if (!(screens.xs || screens.md) && collapsed) {
          return (
            <Button
              onClick={() => {
                navigate('/');
              }}
            />
          );
        }
        return <></>;
      }

    return (
            <ProLayout        
            collapsed={collapse}
            fixSiderbar
            collapsedButtonRender={screens.xs ? undefined : false}
            layout="mix"
            location={{
                pathname: '/',
            }}
            onCollapse={() => {
              setCollapse(!collapse);
            }}
            fixedHeader
            headerContentRender={renderHeader}
            menuExtraRender={({collapsed}) => renderMenuExtraRender(collapsed)}
            menuHeaderRender={()=> renderMenuHeaderRender(Boolean(collapse))}
            menuContentRender={renderMenu}
            >
            {accessToken ? <Outlet/> : <Navigate to='login'/>}
            </ProLayout>
    );
}
