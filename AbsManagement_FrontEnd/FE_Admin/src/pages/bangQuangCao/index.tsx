import { Route, Routes, useLocation } from 'react-router-dom';
import ListBangQuangCaoPage from './list';
import UpdateBangQuangCaoPage from './update';
import CreateBangQuangCaoPage from './create';
import DetailBangQuangCaoPage from './detail';
import NotFoundFeature from '../notFound';

export default function BangQuangCaoFeature(): JSX.Element {
    const match = useLocation();
    return (
        <Routes>
            <Route path={match.pathname} Component={ListBangQuangCaoPage}/>
            <Route path={`${match.pathname}/capnhat/:id`} Component={UpdateBangQuangCaoPage} />
            <Route path={`${match.pathname}/taomoi`} Component={CreateBangQuangCaoPage} />
            <Route path={`${match.pathname}/:id`} Component={DetailBangQuangCaoPage} />
            <Route Component={NotFoundFeature} />
        </Routes>
    );
}