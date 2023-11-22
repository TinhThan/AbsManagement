import  { JwtPayload,jwtDecode  }  from "jwt-decode";
import axios, {
AxiosError,
AxiosInstance,
AxiosRequestConfig,
AxiosResponse,
} from "axios";
import {
Notification,
} from "../utils";
import { ErrorInfo } from "./models/base-api";
import { ConfigUrlApi } from "./configs/configUrlApi";
import TokenStorage from "./storages/tokenStorage";
import RefreshTokenStorage from "./storages/refreshTokenStorage";
import { LoginResponse } from "./authAPI";

const cancelToken = axios.CancelToken.source();

function isTokenValid(token : string) {
    const tokenInfo: JwtPayload = jwtDecode(token);
    if (tokenInfo?.exp) {
        const expirationTimeUTC = new Date(tokenInfo.exp * 1000 - 10000).toUTCString();


        const currentTimeUTC = new Date().toUTCString();
        return expirationTimeUTC > currentTimeUTC;
    }
    return false;
}

export class BaseApi {
    private api: AxiosInstance;
    public constructor(config?: AxiosRequestConfig) {
        const _baseUrl = "https://localhost:44394/api";
        axios.defaults.headers.post["Content-Type"] =
        "application/json;charset=utf-8";
        axios.defaults.headers.baseURL = _baseUrl;
        axios.defaults.timeout = 10000;
        this.api = axios.create(config);
        this.handleRequest();
        this.handleResponse();
    }

    private handleRequest() {
        this.api.interceptors.request.use(
        async (config)=> {
            const isLogin = config.url?.includes(ConfigUrlApi.Urls.User.Login);
            if (isLogin) {
                return config;
            }

            let token = TokenStorage.get();
            console.log("token",token)
            if (token) {
                const checkToken = isTokenValid(token);
                if (!checkToken) {
                    const refresh = RefreshTokenStorage.get();
                    const dataRefresh: LoginResponse = {
                        accessToken: token,
                        refreshToken: refresh
                    }
                    token = (await axios.post<string>("https://localhost:44394/api"+ConfigUrlApi.Urls.User.RefreshToken, dataRefresh)).data;
                    TokenStorage.set(token);
                }
                config.headers['Authorization'] = 'Bearer ' + token;
            }
            return config;
        },
        function (error: AxiosError) {
            return Promise.reject(error);
        }
        );
    }

    private handleResponse() {
        this.api.interceptors.response.use(
        (response: AxiosResponse) => {
            console.log("response",response)
            if (response.status === 200) {
            console.log("response",response)
            const isLogout = response.config.url === ConfigUrlApi.Urls.User.Login;
            const isLogIn = response.config.url === ConfigUrlApi.Urls.User.Login;

            if (response.config.responseType === "blob") {
                return response;
            }

            if (
                typeof response.data === "string" &&
                response.data &&
                !isLogIn &&
                !isLogout
            ) {
                Notification.Success(response.data);
            }

            if (!response.data) {
                return response;
            }

            if (!Array.isArray(response.data)) {
                return response.data;
            }

            if (!response.data.length) {
                return [];
            }

            return response.data;
            } else {
                Notification.Fail(response.data);
                return Promise.reject(response);
            }
        },
        async(error: AxiosError<ErrorInfo>) => {
            
            console.log("error",error)
            if (axios.isCancel(error)) {
            return Promise.reject(error);
            }

            // const isLogout = error.config.url === ConfigUrlApi.Urls.User.Logout;
            // const isLogIn = error.config.url === ConfigUrlApi.Urls.User.Login;

            // if (isLogIn) {
            // return Promise.reject(error.response?.data);
            // }
            // if (error.response) {
            // const status = error.response.status;
            // const data = error.response.data;

            // switch (status) {
            //     case 401:
            //         Notification.Warning(data.detail ?? "Xác thực người dùng thất bại.");
            //         return Promise.reject(error);
            //     case 400:
            //         Notification.Warning(data.detail ?? "Một số thông tin không hợp lệ");
            //     return Promise.reject(error);
            //     case 403:
            //     {
            //         const data = error.response.data;
            //         if (data.detail) {
            //         Notification.Warning(data.detail);
            //         }
            //     }
            //     return Promise.reject(error);
            //     case 500:
            //         Notification.Fail(Language.ServerError);
            //     return Promise.reject(error);
            //     default:
            //     return Promise.reject(error);
            // }
            // }
        }
        );
    }

    public get<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
        return this.api.get(url, {
        ...config,
        });
    }

    public put<T>(
        url: string,
        data?: object,
        config?: AxiosRequestConfig
    ): Promise<T> {
        return this.api.put(url, data, {
        ...config,
        cancelToken: cancelToken.token,
        });
    }

    public post<T>(
        url: string,
        data?: object | string,
        config?: AxiosRequestConfig
    ): Promise<T> {
        console.log(this.api);
        return this.api.post(url, data, {
        ...config,
        cancelToken: cancelToken.token,
        });
    }

    public postRefreshToken(): Promise<string> {
        const data:LoginResponse={
            accessToken: TokenStorage.get(),
            refreshToken: RefreshTokenStorage.get()
        }
        return this.api.post("https://localhost:44394/api"+ConfigUrlApi.Urls.User.RefreshToken, data);
    }

    public getReportPdfArrayBuffer<T, R = AxiosResponse<T>>(
        url: string
    ): Promise<T> {
        return this.api.get(url, {
        responseType: "blob",
        });
    }

    public getFileExportExcel<T>(url: string, data?: object): Promise<T> {
        return this.api.post(url, data, { responseType: "blob" });
    }

    public patch<T>(
        url: string,
        data?: object,
        config?: AxiosRequestConfig
    ): Promise<T> {
        return this.api.patch(url, data, {
        ...config,
        cancelToken: cancelToken.token,
        });
    }
}
