import {  Route, Switch,RouteProps } from "react-router-dom";
import { ConfigRoute } from "./ConfigRoute";
import NotFoundFeature from "../pages/notFound";
import { SmileFilled } from "@ant-design/icons";
import { messageRoute } from "../pages/constant/messageSystem";
import { Suspense, lazy } from "react";
import { PageLoading } from "@ant-design/pro-components";

const routeCanBoSos: RouteProps[] = [
    {
      path: ConfigRoute.CanBoSo.CanBo,
      exact: true,
      name: messageRoute.CanBo,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.Quan,
      exact: true,
      name: messageRoute.Quan,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/home'))
    },
    {
      path: ConfigRoute.CanBoSo.Phuong,
      exact: true,
      name: messageRoute.Phuong,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.HinhThucBaoCao,
      exact: true,
      name: messageRoute.HinhThucBaoCao,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.LoaiViTri,
      exact: true,
      name: messageRoute.LoaiViTri,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.LoaiBangQuangCao,
      exact: true,
      name: messageRoute.LoaiBangQuangCao,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.HinhThucBaoCao,
      exact: true,
      name: messageRoute.HinhThucBaoCao,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    //
    {
      path: ConfigRoute.CanBoSo.DiemDatQuangCao,
      exact: true,
      name: messageRoute.DiemDatQuangCao,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.BangQuangCao,
      exact: true,
      name: messageRoute.BangQuangCao,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.BaoCaoViPham,
      exact: true,
      name: messageRoute.BaoCaoViPham,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.DuyetDiemQuangCao,
      exact: true,
      name: messageRoute.DuyetDiemQuangCao,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.DuyetBangQuangCao,
      exact: true,
      name: messageRoute.DuyetBangQuangCao,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.DuyetCapPhepQuangCao,
      exact: true,
      name: messageRoute.DuyetCapPhepQuangCao,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.ThongKe_Phuong,
      exact: true,
      name: messageRoute.ThongKe_Phuong,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    },
    {
      path: ConfigRoute.CanBoSo.ThongKe_Quan,
      exact: true,
      name: messageRoute.ThongKe_Quan,
      icon: <SmileFilled rev={undefined} />,
      component:lazy(()=>import('../pages/bangQuangCao'))
    }
];

export default function RouteSystem(): JSX.Element {
    return (
      <Suspense fallback={<PageLoading />}>
        <Switch>
          {routeCanBoSos.map((routeItem) => (
            <Route key={routeItem.path} {...routeItem}/>
          ))}
          <Route component={NotFoundFeature} />
        </Switch>
      </Suspense>
    );
}
