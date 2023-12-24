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
import { ConfigUrlApi } from "./configs/configUrlApi";
import TokenStorage from "../storages/tokenStorage";
import RefreshTokenStorage from "../storages/refreshTokenStorage";
import { LoginResponse } from "./auth/authAPI";
import { MessageBox } from "../utils/messagebox";

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

interface ErrorInfo{
    errors?: {[key: string]: string[]};
    status: number;
    detail?: string;
    description?: string[];
}

export class BaseApi {
    private api: AxiosInstance;
    public isPublic_API: boolean = false;
    public constructor(config?: AxiosRequestConfig) {
        const _baseUrl = "https://localhost:44394/api";
        axios.defaults.headers.post["Content-Type"] = "application/json;charset=utf-8";
        axios.defaults.headers.baseURL = _baseUrl;
        axios.defaults.timeout = 10000;
        this.api = axios.create(config);
        this.handleRequest();
        this.handleResponse();
    }

    private handleRequest() {
        this.api.interceptors.request.use(
        async (config)=> {
            // const isLogin = config.url?.includes(ConfigUrlApi.Urls.User.Login);
            // if (isLogin || this.isPublic_API) {
            //     return config;
            // }

            // let token = TokenStorage.get();
            // if (token) {
            //     const checkToken = isTokenValid(token);
            //     if (!checkToken) {
            //         const refresh = RefreshTokenStorage.get();
            //         const dataRefresh: LoginResponse = {
            //             accessToken: token,
            //             refreshToken: refresh
            //         }
            //         token = (await axios.post<string>("https://localhost:44394/api"+ConfigUrlApi.Urls.User.RefreshToken, dataRefresh)).data;
            //         TokenStorage.set(token);
            //     }
            //     config.headers['Authorization'] = 'Bearer ' + token;
            // }
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
            console.log("AxiosResponse",response)
            if (response && response.status === 200) {
                if (typeof response.data === 'string' && response.data ) {
                    Notification.Success(response.data)
                }
                return response;
            } else {
                Notification.Fail(response.data);
                return Promise.reject(response);
            }
        },
        async(error: AxiosError<ErrorInfo>) => {
            if(error.message === 'Network Error')
            {
                MessageBox.Fail("Hệ thống đã xảy ra lỗi.")
                return;
            }
            MessageBox.Fail(error.message)
        }
        );
    }

    public get<T>(url: string, config?: AxiosRequestConfig) {
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

    public post(
        url: string,
        data?: object | string,
        config?: AxiosRequestConfig
    ) {
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
