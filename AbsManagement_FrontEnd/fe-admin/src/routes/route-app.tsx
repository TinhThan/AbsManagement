import {  createBrowserRouter,redirect  } from "react-router-dom";
import { ConfigRoute } from "./ConfigRoute";
import React from 'react';
import App from "../pages/app";
import TokenStorage from "../storages/tokenStorage";
import BangQuangCaoFeature from "../pages/bangQuangCao";
import Home from "../pages/home";
import Login from "../pages/auth/Login";
import NotFoundFeature from '../pages/notFound/index';
import CanBoFeature, { RoleCanBo } from "../pages/canBo";
import UserInfoStorage from "../storages/user-info";
import LoaiViTriFeature from "../pages/loaiViTri";
import LoaiBangQuangCaoFeature from "../pages/loaiBangQuangCao";
import HinhThucQuangCaoFeature from "../pages/hinhThucQuangCao";
import HinhThucBaoCaoFeature from "../pages/hinhThucBaoCao";

const router = createBrowserRouter([
  {
    id: "root",
    path: "/",
    Component: App,  
    errorElement: <NotFoundFeature/>,
    children: [
      {
        index: true,
        element: <h1>Trang chủ</h1>,
      },
      {
        path:"*",
        Component: NotFoundFeature,
      },
      {
        path: ConfigRoute.CanBoSo.BangQuangCao,
        loader:protectedLoader,
        Component:BangQuangCaoFeature
      },
      {
        path: ConfigRoute.CanBoSo.DiemDatQuangCao,
        loader:protectedLoader,
        Component:Home
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
  }
]);

export default router;

async function loginLoader() {
  const accessToken = TokenStorage.get();
  const userInfo = UserInfoStorage.get();
  if (accessToken && userInfo) {
    return redirect("/");
  }
  return null;
}

function protectedLoader() {
  const accessToken = TokenStorage.get();
  const userInfo = UserInfoStorage.get();
  console.log("user",userInfo)
  if (!accessToken || !userInfo) {
    console.log("redirect login")
    return redirect("/login");
  }
  return null;
}

function protectedCanBoLoader() {
  const accessToken = TokenStorage.get();
  const userInfo = UserInfoStorage.get();
  if (!accessToken || !userInfo) {
    return redirect("/login");
  }
  console.log("user",userInfo)
  if(userInfo.role !== RoleCanBo.CanBoSo)
    return redirect('/');
  return null;
}
