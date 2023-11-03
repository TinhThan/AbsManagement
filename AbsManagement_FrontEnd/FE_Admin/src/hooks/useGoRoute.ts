import { useHistory } from 'react-router-dom';
import { ConfigRoute } from '../routes/ConfigRoute';

export const useGoRoute = (): { goRoute: (url: Array<keyof typeof ConfigRoute>[0]) => void; history: H.History } => {
    const history = useHistory();

    function goRoute(url: Array<keyof typeof ConfigRoute>[0]) {
        const _url = ConfigRoute[url];
        history.push(_url);
    }
    return { goRoute, history };
};
