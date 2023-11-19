import { Suspense } from 'react';
import ListBangQuangCaoPage from './list';
import { PageLoading } from '@ant-design/pro-components';

export default function BangQuangCaoFeature(): JSX.Element {
    return (
        <Suspense fallback={<PageLoading/>}>
            <ListBangQuangCaoPage/>
        </Suspense>
    );
}