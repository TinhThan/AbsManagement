import { Menu, MenuProps } from 'antd';
import React, { useEffect, useState } from 'react'
import { RoleCanBo } from '../../canBo';
import { HomeOutlined, SettingOutlined } from '@ant-design/icons';
import { ConfigRoute } from '../../../routes/ConfigRoute';
import { Link, useNavigate } from 'react-router-dom';

type MenuItem = Required<MenuProps>['items'][number];

function getItem(
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  children?: MenuItem[],
  type?: 'group',
  title?: string,
  disabled?: boolean
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

const itemDefaults: MenuItem[] = [
  getItem(<Link to={ConfigRoute.CanBoSo.DiemDatQuangCao}>Điểm đặt quảng cáo</Link>, ConfigRoute.CanBoSo.DiemDatQuangCao, undefined, undefined, undefined),
  getItem(<Link to={ConfigRoute.CanBoSo.BangQuangCao}>Bảng quảng cáo</Link>, ConfigRoute.CanBoSo.BangQuangCao, undefined, undefined, undefined),
  getItem(<Link to={ConfigRoute.CanBoSo.BaoCaoViPham}>Báo cáo vi phạm</Link>, ConfigRoute.CanBoSo.BaoCaoViPham, undefined, undefined, undefined),
];

interface Props {
  role: string;
}

export default function MenuLayout(props: Props): JSX.Element {
  const { role } = props;
  const [menus, setMenus] = useState<MenuItem[]>(itemDefaults);

  useEffect(() => {
    if (role === RoleCanBo.CanBoSo) {
      setMenus([
        getItem(<Link to={ConfigRoute.CanBoSo.CanBo}>Cán bộ</Link>, ConfigRoute.CanBoSo.CanBo),
        getItem(
          'Hệ thống',
          'heThong',
          <SettingOutlined />,
          [
            getItem(<Link to={ConfigRoute.CanBoSo.LoaiBangQuangCao}>Loại bảng quảng cáo</Link>, ConfigRoute.CanBoSo.LoaiBangQuangCao),
            getItem(<Link to={ConfigRoute.CanBoSo.LoaiViTri}>Loại vị trí</Link>, ConfigRoute.CanBoSo.LoaiViTri),
            getItem(<Link to={ConfigRoute.CanBoSo.HinhThucQuangCao}>Hình thức quảng cáo</Link>, ConfigRoute.CanBoSo.HinhThucQuangCao),
            getItem(<Link to={ConfigRoute.CanBoSo.HinhThucBaoCao}>Hình thức báo cáo</Link>, ConfigRoute.CanBoSo.HinhThucBaoCao),
          ],
          'group'
        ),
        getItem(
          'Phiếu cấp phép sửa quảng cáo',
          'phieucapphepsuaquangcao',
          <SettingOutlined />,
          [
            getItem(<Link to={`${ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao}/diem-dat-quang-cao`}>Sửa điểm đặt</Link>, `${ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao}/diem-dat-quang-cao`),
            getItem(<Link to={`${ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao}/bang-quang-cao`}>Sửa bảng quảng cáo</Link>, `${ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao}/bang-quang-cao`),
          ],
          'group'
        ),
        getItem(<Link to={ConfigRoute.CanBoSo.DuyetCapPhepQuangCao}>Phiếu cấp phép quảng cáo</Link>, ConfigRoute.CanBoSo.DuyetCapPhepQuangCao)
        , ...itemDefaults])
    }
  }, [role])

  return (
    <Menu
      items={menus}
    />
  )
}