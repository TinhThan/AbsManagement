import { PageLoading } from '@ant-design/pro-components';
import React from 'react';
import { Suspense} from "react";
export default function HinhThucQuangCaoFeature(): JSX.Element {
    return (
        <Suspense fallback={<PageLoading/>}>
            Danh sách hình thức quảng cáo
        </Suspense>
    );
}