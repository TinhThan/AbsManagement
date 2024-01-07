import {  createBrowserRouter,redirect  } from "react-router-dom";
import { ConfigRoute } from "./ConfigRoute";
import App from "../pages/app";
import BangQuangCaoFeature from "../pages/bangQuangCao";
import Login from "../pages/auth/Login";
import ForgotPassword from "../pages/auth/ForgotPassword";
import NotFoundFeature from '../pages/notFound/index';
import CanBoFeature, { RoleCanBo } from "../pages/canBo";
import UserInfoStorage from "../storages/user-info";
import LoaiViTriFeature from "../pages/loaiViTri";
import LoaiBangQuangCaoFeature from "../pages/loaiBangQuangCao";
import HinhThucQuangCaoFeature from "../pages/hinhThucQuangCao";
import HinhThucBaoCaoFeature from "../pages/hinhThucBaoCao";
import ResetPassword from "../pages/auth/ResetPassword";
import HomeFeature from "../pages/home";
import BaoCaoViPhamFeature from "../pages/baoCaoViPham";
import ListBaoCaoViPham from "../pages/baoCaoViPham/list";
import DetailBaoCaoViPham from "../pages/baoCaoViPham/detail";
import UpdateBaoCaoViPham from "../pages/baoCaoViPham/update";
import ListDiemDatQuangCao from "../pages/diemDatQuangCao/list";
import { DetailDiemDatQuangCao } from "../pages/diemDatQuangCao/detail";
import DiemDatQuangCaoFeature from "../pages/diemDatQuangCao";
import { UpdateDiemDatQuangCao } from "../pages/diemDatQuangCao/update";
import { CreateDiemDatQuangCao } from "../pages/diemDatQuangCao/create";
import ListBangQuangCao from "../pages/bangQuangCao/list";
import { UpdateBangQuangCao } from "../pages/bangQuangCao/update";
import { DetailBangQuangCao } from "../pages/bangQuangCao/detail";
import { CreateBangQuangCao } from "../pages/bangQuangCao/create";
import ListFixLocation from "../pages/capphepsuaquangcao/listFixLocation";
import ListFixBoard from "../pages/capphepsuaquangcao/listFixBoard";
import DetailFixLocation from "../pages/capphepsuaquangcao/detailFixLocation";
import DetailFixBoard from "../pages/capphepsuaquangcao/detailFixBoard";
import ListAcceptAds from "../pages/capPhepQuangCao/list";
import DetailAcceptAds from "../pages/capPhepQuangCao/detail";

const router = createBrowserRouter([
  {
    id: "root",
    path: "/",
    Component: App,  
    // errorElement: <div>Error</div>,
    children: [
      {
        index: true,
        Component: HomeFeature,
        loader:protectedLoader,
      },
      {
        path:"*",
        Component: NotFoundFeature,
      },
      {
        path: ConfigRoute.CanBoSo.BangQuangCao,
        loader:protectedLoader,
        Component:BangQuangCaoFeature,
        children: [
          {
            path:ConfigRoute.CanBoSo.BangQuangCao,
            Component: ListBangQuangCao
          },
          {
            path:`${ConfigRoute.CanBoSo.BangQuangCao}/:id`,
            Component: DetailBangQuangCao
          },
          {
            path:`${ConfigRoute.CanBoSo.BangQuangCao}/capnhat/:id`,
            Component: UpdateBangQuangCao
          },
          {
            path:`${ConfigRoute.CanBoSo.BangQuangCao}/capphep`,
            Component: CreateBangQuangCao
          },
          {
            path:"*",
            Component: NotFoundFeature
          }
        ]
      },
      {
        path: ConfigRoute.CanBoSo.DiemDatQuangCao,
        loader:protectedLoader,
        Component: DiemDatQuangCaoFeature,
        children: [
          {
            path:ConfigRoute.CanBoSo.DiemDatQuangCao,
            Component: ListDiemDatQuangCao
          },
          {
            path:`${ConfigRoute.CanBoSo.DiemDatQuangCao}/:id`,
            Component: DetailDiemDatQuangCao
          },
          {
            path:`${ConfigRoute.CanBoSo.DiemDatQuangCao}/capnhat/:id`,
            Component: UpdateDiemDatQuangCao
          },
          {
            path:`${ConfigRoute.CanBoSo.DiemDatQuangCao}/taomoi`,
            loader:protectedCanBoLoader,
            Component: CreateDiemDatQuangCao
          },
          {
            path:"*",
            Component: NotFoundFeature
          }
        ]
      },
      {
        path: ConfigRoute.CanBoSo.BaoCaoViPham,
        loader:protectedLoader,
        Component:BaoCaoViPhamFeature,
        children: [
          {
            path:ConfigRoute.CanBoSo.BaoCaoViPham,
            Component: ListBaoCaoViPham
          },
          {
            path:`${ConfigRoute.CanBoSo.BaoCaoViPham}/:id`,
            Component: DetailBaoCaoViPham
          },
          {
            path:`${ConfigRoute.CanBoSo.BaoCaoViPham}/capnhat/:id`,
            Component: UpdateBaoCaoViPham
          },
          {
            path:"*",
            Component: NotFoundFeature
          }
        ]
      },
      //Route cán bộ sở
      {
        path: ConfigRoute.CanBoSo.CanBo,
        loader:protectedCanBoLoader,
        Component:CanBoFeature
      },
      {
        path: ConfigRoute.CanBoSo.LoaiViTri,
        loader:protectedCanBoLoader,
        Component:LoaiViTriFeature
      },
      {
        path: ConfigRoute.CanBoSo.LoaiBangQuangCao,
        loader:protectedCanBoLoader,
        Component:LoaiBangQuangCaoFeature
      },
      {
        path: ConfigRoute.CanBoSo.HinhThucQuangCao,
        loader:protectedCanBoLoader,
        Component:HinhThucQuangCaoFeature
      },
      {
        path: ConfigRoute.CanBoSo.HinhThucBaoCao,
        loader:protectedCanBoLoader,
        Component:HinhThucBaoCaoFeature
      },
      {
        path: ConfigRoute.CanBoSo.DuyetCapPhepQuangCao,
        loader:protectedCanBoLoader,
        children: [
          {
            path: ConfigRoute.CanBoSo.DuyetCapPhepQuangCao,
            Component: ListAcceptAds
          },
          {
            path: `${ConfigRoute.CanBoSo.DuyetCapPhepQuangCao}/:id`,
            Component: DetailAcceptAds
          },
        ]
      },
      {
        path: ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao,
        loader:protectedCanBoLoader,
        children: [
          {
            path: `${ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao}/diem-dat-quang-cao`,
            Component: ListFixLocation,
          },
          {
            path: `${ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao}/diem-dat-quang-cao/:id`,
            Component: DetailFixLocation
          },
          {
            path: `${ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao}/bang-quang-cao`,
            Component: ListFixBoard
          },
          {
            path: `${ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao}/bang-quang-cao/:id`,
            Component: DetailFixBoard
          },
          {
            path: `${ConfigRoute.CanBoSo.DuyetCapPhepSuaQuangCao}/capnhat/:id`,
            Component: UpdateBaoCaoViPham
          },
          {
            path:"*",
            Component: NotFoundFeature
          }
        ]
      }
    ],
  },
  {
    path: ConfigRoute.Login,
    loader: loginLoader,
    Component: Login
  },
  {
    path: ConfigRoute.ForgotPassword,
    loader: loginLoader,
    Component: ForgotPassword
  },
  {
    path: ConfigRoute.ResetPassword,
    loader: loginLoader,
    Component: ResetPassword
  }
]);

export default router;

async function loginLoader() {
  const userInfo = UserInfoStorage.get();
  if (userInfo) {
    return redirect("/");
  }
  return null;
}

function protectedLoader() {
  const userInfo = UserInfoStorage.get();
  console.log("user",userInfo)
  if (!userInfo) {
    return redirect("/login");
  }
  return null;
}

function protectedCanBoLoader() {
  const userInfo = UserInfoStorage.get();
  if (!userInfo) {
    return redirect("/login");
  }
  if(userInfo.role !== RoleCanBo.CanBoSo)
    return redirect('/');
  return null;
}
