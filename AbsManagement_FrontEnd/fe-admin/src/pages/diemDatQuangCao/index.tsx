import { PageLoading } from "@ant-design/pro-components";
import { Suspense } from "react";
import { Outlet } from "react-router-dom";

export default function DiemDatQuangCaoFeature(): JSX.Element {

    return (
        <Suspense fallback={<PageLoading/>}>
            <Outlet/>
        </Suspense>
    );
}