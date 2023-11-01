import { Suspense, lazy } from "react";
import { Route, RouteProps, Routes } from "react-router-dom";
import { ConfigRoute } from "./ConfigRoute";
import NotFoundFeature from "../pages/notFound";

const routes: RouteProps[]=[
    {
        path:ConfigRoute.Home,
        Component:lazy(()=>import("../pages/home"))
    },
    {
        path:ConfigRoute.BangQuangCao,
        Component:lazy(()=>import("../pages/bangQuangCao"))
    }
]

export default function RouteSystem(): JSX.Element {
    return (
        <Suspense>
            <Routes>
                {routes.map((routeItem) => (
                    <Route key={routeItem.path} {...routeItem} />
                ))}
                <Route Component={NotFoundFeature} />
            </Routes>
        </Suspense>
    );
}