import { Divider, Menu, MenuProps, Space, Spin, Tooltip } from "antd";
import { useLocation,NavLink } from 'react-router-dom';
import { useEffect, useState } from "react";
import { menuCanBoSos, menuDefaults } from "./menuData";
import { messageSystem } from "../../../constants/messageSystem";
import { RenderTitle } from "./renderTitle";

type MenuItem = Required<MenuProps>['items'][number];

function getItem(
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  children?: MenuItem[],
  type?: 'group',
  title?: string,
  disabled?: boolean,
): MenuItem {
  return {
    key,
    icon,
    children,
    label,
    type,
    title,
    disabled,
  } as MenuItem;
}

export interface MenuTree {
    id: string;
    ten: string;
    idMenuCha: string;
    thuTu: number;
    ghiChu: string;
    url: string;
    trangThai: StatusMenu;
    icon: string;
    children: MenuTree[];
}

export enum StatusMenu {
    NOTACCESSPERMISSION,
    ACCESS,
}

interface Props {
    collapse: boolean;
}

export default function MenuLayout(props: Props): JSX.Element {
    const { collapse } = props;
    const [openKeys, setOpenKeys] = useState<Array<string>>([]);
    const location = useLocation();
    const [selectedKeys, setSelectedKeys] = useState<Array<string>>([]);
  
    useEffect(() => {
      const menus = menuCanBoSos;
      if (location.pathname === '/') {
        setOpenKeys([]);
        setSelectedKeys([]);
      }
      for (const subParent of menus) {
        for (const sub of subParent.children) {
          for (const child of sub.children) {
            if (child.url === location.pathname.split('/')[1]) {
              updateSelectKey(sub.id, child.id);
              return;
            }
          }
        }
      }
    }, [location.pathname]);
  
    //#endregion
  
    function updateSelectKey(subId: string, childId: string) {
      if (!collapse) {
        setOpenKeys([subId]);
      }
      setSelectedKeys([childId]);
    }
    
      // Sự kiển mở submenu
    const onOpenChange = (keys: string[]) => {
      setOpenKeys([keys[keys.length - 1]]);
    };
    
    const RenderTooltip = (status: StatusMenu) => {
      if (status === StatusMenu.NOTACCESSPERMISSION) {
        return messageSystem.NotPermission;
      } else {
        return '';
      }
    };
    
    function menuItems(menu: MenuTree) {
      const items: MenuProps['items'] = [
        {
          label: <b className='b-menu-title-parent'>{menu.ten}</b>,
          key: menu.id,
          icon: collapse && <i className={menu.icon}></i>,
          className: 'menu-parent-title'
        },
      ];
      menu.children.sort((a, b) => a.thuTu - b.thuTu);
      const itemSub: MenuProps['items'] = menu.children.map((sub) => {
        sub.children.sort((a, b) => a.thuTu - b.thuTu);
  
        return getItem(
          <RenderTitle name={sub.ten} status={sub.trangThai} />,
          sub.id,
          <i className={sub.icon}></i>,
          sub.children.map((child) => {
            return getItem(
              <Tooltip title={RenderTooltip(child.trangThai)} placement='right'>
                <div>
                  <NavLink
                    className='link'
                    title={child.id}
                    to={{
                      pathname: '/' + (child.url || 'not-found'),
                    }}
                    onClick={()=> console.log('/' + (child.url || 'not-found'))}
                    style={
                      child.trangThai === StatusMenu.NOTACCESSPERMISSION
                        ? {
                            pointerEvents: 'none',
                            cursor: 'not-allowed',
                          }
                        : {}
                    }
                  >
                    <RenderTitle name={child.ten} status={child.trangThai} />
                  </NavLink>
                </div>
              </Tooltip>,
              child.id,
              undefined,
              undefined,
              undefined,
              undefined,
              false
            );
          })
        );
      });
      return [...items, ...itemSub];
    }
    return (
      <Spin spinning={false} size='large' style={{ minHeight: 100 }}>
        <Space className='space-menu' direction='vertical' size={0} split={<Divider type='horizontal' className='divider-space'></Divider>}>
          {menuCanBoSos.map((menu) => (
            <Menu
              className='menu-system'
              items={menuItems(menu)}
              inlineCollapsed={collapse}
              openKeys={openKeys}
              onOpenChange={onOpenChange}
              key={menu.id}
              mode='inline'
              selectedKeys={selectedKeys}
            />
          ))}
        </Space>
      </Spin>
    );
}