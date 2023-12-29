import { PageLoading } from "@ant-design/pro-components";
import { Suspense } from "react";

export default function UpdateBaoCaoViPham(): JSX.Element {

    return (
        <Suspense fallback={<PageLoading/>}>
           Update
        </Suspense>
    );
}