import {  createBrowserRouter,redirect, useNavigate  } from "react-router-dom";
import { ConfigRoute } from "./ConfigRoute";
import { lazy } from "react";
import App from "../pages/app";
import TokenStorage from "../storages/tokenStorage";
import BangQuangCaoFeature from "../pages/bangQuangCao";
import Home from "../pages/home";
import Login from "../pages/auth/Login";
import NotFoundFeature from '../pages/notFound/index';
import CanBoFeature from "../pages/canBo";

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
      {
        path: ConfigRoute.CanBoSo.CanBo,
        loader:protectedLoader,
        Component:CanBoFeature
      }
    ],
  },
  {
    path: ConfigRoute.Login,
    loader: loginLoader,
    Component: Login,
  }
]);

export default router;

async function loginLoader() {
  const accessToken = TokenStorage.get();
  if (accessToken) {
    return redirect("/");
  }
  return null;
}

function protectedLoader() {
  const accessToken = TokenStorage.get();
  if (!accessToken) {
    return redirect("/login");
  }
  return null;
}
