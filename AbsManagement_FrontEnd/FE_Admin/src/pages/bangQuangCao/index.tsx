import { Route, Switch, useLocation } from 'react-router-dom';
import ListBangQuangCaoPage from './list';
import UpdateBangQuangCaoPage from './update';
import CreateBangQuangCaoPage from './create';
import DetailBangQuangCaoPage from './detail';

export default function BangQuangCaoFeature(): JSX.Element {
    const match = useLocation();
    console.log("match",match)
    return (
        <Switch>
            <Route path={match.pathname} component={ListBangQuangCaoPage} exact/>
            <Route path={`${match.pathname}/capnhat/:id`} component={UpdateBangQuangCaoPage} exact/>
            <Route path={`${match.pathname}/taomoi`} component={CreateBangQuangCaoPage} exact/>
            <Route path={`${match.pathname}/:id`} component={DetailBangQuangCaoPage} exact/>
            {/* <Route component={<NotFoundFeature/>} /> */}
        </Switch>
    );
}