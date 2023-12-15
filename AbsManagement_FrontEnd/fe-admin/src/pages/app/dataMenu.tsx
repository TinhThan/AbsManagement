import { MenuProps } from "antd";
import React from 'react';
import { Link } from "react-router-dom";
import { ConfigRoute } from "../../routes/ConfigRoute";

export const menuCanBos: MenuProps['items'] = [
    {
      label: (
        <Link to="/">Trang chủ</Link>
      ),
      key: '1',
    },
    {
      label:"Hệ thống",
      key:'2',
      children:[
        {
          label: (
            <Link to={ConfigRoute.CanBoSo.LoaiBangQuangCao}>Loại bảng quảng cáo</Link>
          ),
          key: '3'
        },
        {
          label: (
            <Link to={ConfigRoute.CanBoSo.LoaiViTri}>Loại vị trí</Link>
          ),
          key: '4',
        },
        {
          label: (
            <Link to={ConfigRoute.CanBoSo.HinhThucQuangCao}>Hình thức quảng cáo</Link>
          ),
          key: '5',
        },
        {
          label: (
            <Link to={ConfigRoute.CanBoSo.HinhThucBaoCao}>Hình thức báo cáo</Link>
          ),
          key: '6',
        }
      ]
    },
    {
      label: (
        <Link to={ConfigRoute.CanBoSo.DiemDatQuangCao}>Điểm đặt quảng cáo</Link>
      ),
      key: '7',
    },
    {
      label: (
        <Link to={ConfigRoute.CanBoSo.BangQuangCao}>Bảng quảng cáo</Link>
      ),
      key: '8',
    },
    {
      label: (<Link to={ConfigRoute.CanBoSo.CanBo}>Cán bộ</Link>),
      key: '9'
    }
]