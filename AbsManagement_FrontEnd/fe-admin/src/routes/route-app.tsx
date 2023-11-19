import {  createBrowserRouter,redirect, useNavigate  } from "react-router-dom";
import { ConfigRoute } from "./ConfigRoute";
import { lazy } from "react";
import App from "../pages/app";
import TokenStorage from "../apis/storages/tokenStorage";
const router = createBrowserRouter([
  {
    id: "root",
    path: "/",
    Component: App,  
    children: [
      {
        index: true,
        element: <h1>Trang chủ</h1>,
      },
      {
        path: ConfigRoute.CanBoSo.BangQuangCao,
        loader:protectedLoader,
        Component:lazy(()=>import('../pages/bangQuangCao'))
      },
      {
        path: ConfigRoute.CanBoSo.DiemDatQuangCao,
        loader:protectedLoader,
        Component:lazy(()=>import('../pages/home'))
      }
    ],
  },
  {
    path: ConfigRoute.Login,
    loader: loginLoader,
    Component: lazy(()=> import('../pages/auth/Login')),
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
