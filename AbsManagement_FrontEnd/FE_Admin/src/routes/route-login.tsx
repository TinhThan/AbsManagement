import { Suspense, lazy } from "react";
import { Route, Routes } from "react-router-dom";
import { ConfigRoute } from "./ConfigRoute";

export default function RouteAuth(): JSX.Element {
  return (
    <Suspense>
      <Routes>
        <Route
          path={ConfigRoute.Login}
          Component={lazy(() => import("../pages/auth/Login"))}
        />
      </Routes>
    </Suspense>
  );
}
