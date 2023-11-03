import { Suspense, lazy } from "react";
import { Route, Switch } from "react-router-dom";
import { ConfigRoute } from "./ConfigRoute";
import { PageLoading } from "@ant-design/pro-components";

export default function RouteAuth(): JSX.Element {
  return (
    <Suspense fallback={<PageLoading/>}>
      <Switch>
        <Route
          path={ConfigRoute.Login}
          Component={lazy(() => import("../pages/auth/Login"))}
        />
      </Switch>
    </Suspense>
  );
}
