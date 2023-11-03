import type { ProLayoutProps } from "@ant-design/pro-components";
import { ProLayout } from "@ant-design/pro-components";
import { useEffect, useState } from "react";
import RouteSystem from "../../routes/route-app";
import TokenStorage from "../../apis/storages/tokenStorage";
import RouteAuth from "../../routes/route-login";
import { useGoRoute } from "../../hooks/useGoRoute";
import MenuLayout from "./menu";
import UserInfoStorage from "../../apis/storages/user-info";
import { HeaderLayout } from "./header";
import { Button, Input, Space } from "antd";
import { messageSystem } from "../constant/messageSystem";
import { useResponsive } from "../../hooks/useResponsive";
import './styles.scss';
import { RenderTitle } from "./menu/renderTitle";

export default function App(): JSX.Element {
    const { goRoute } = useGoRoute();
    const [collapse, setCollapse] = useState(false);
    const screens = useResponsive();

    const accessToken = TokenStorage.get();

    useEffect(() => {
        if (!accessToken) {
            goRoute("Login");
        }
    });

    async function logOutClick() {
        // await authApi
        //   .LogOut()
        //   .then((response) => Notification.Success(t(response)))
        //   .finally(() => {
        //     setIsLogout(true);
        // });
        localStorage.clear();
        goRoute('Login');
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
            goRoute('Home');
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
                goRoute('Home');
              }}
            />
          );
        }
        return <></>;
      }

    return (
        <>
        {!accessToken ? (
            <RouteAuth />
        ) : (
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
            <RouteSystem />
            </ProLayout>
        )}
        </>
    );
}
