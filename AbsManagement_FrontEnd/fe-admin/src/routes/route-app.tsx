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
import DiemDatQuangCaoFeature from "../pages/diemDatQuangCao";
import BaoCaoViPhamFeature from "../pages/baoCaoViPham";
import ListBaoCaoViPham from "../pages/baoCaoViPham/list";
import DetailBaoCaoViPham from "../pages/baoCaoViPham/detail";
import UpdateBaoCaoViPham from "../pages/baoCaoViPham/update";

const router = createBrowserRouter([
  {
    id: "root",
    path: "/",
    Component: App,  
    errorElement: <NotFoundFeature/>,
    children: [
      {
        index: true,
        Component: HomeFeature,
        // loader:protectedLoader,
      },
      {
        path:"*",
        Component: NotFoundFeature,
      },
      {
        path: ConfigRoute.CanBoSo.BangQuangCao,
        // loader:protectedLoader,
        Component:BangQuangCaoFeature
      },
      {
        path: ConfigRoute.CanBoSo.DiemDatQuangCao,
        // loader:protectedLoader,
        Component:DiemDatQuangCaoFeature
      },
      {
        path: ConfigRoute.CanBoSo.BaoCaoViPham,
        // loader:protectedLoader,
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
