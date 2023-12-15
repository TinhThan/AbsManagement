import { PageLoading } from '@ant-design/pro-components';
import React from 'react';
import { Suspense} from "react";
export default function HinhThucBaoCaoFeature(): JSX.Element {
    return (
        <Suspense fallback={<PageLoading/>}>
            Danh sách hình thức báo cáo
        </Suspense>
    );
}