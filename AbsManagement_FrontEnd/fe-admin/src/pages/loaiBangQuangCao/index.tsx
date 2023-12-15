import { PageLoading } from '@ant-design/pro-components';
import React from 'react';
import { Suspense} from "react";
export default function LoaiBangQuangCaoFeature(): JSX.Element {
    return (
        <Suspense fallback={<PageLoading/>}>
            Danh sách bảng quảng cáo
        </Suspense>
    );
}