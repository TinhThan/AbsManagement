import { ConfigRoute } from "../routes/ConfigRoute";

export function logOut(): void {
    localStorage.clear();
    window.location.href = ConfigRoute.Login;
}
