import { API_URL } from '../../constants/apiConfig';
import { BaseApi } from '../baseApi';
import { ConfigUrlApi } from '../configs/configUrlApi';

export interface LoginInfo{
    username:string,
    password:string
}

export interface LoginResponse{
    accessToken:string,
    refreshToken:string
}

class AuthApi extends BaseApi {
  async Login(loginInfo: LoginInfo) {
    return this.post(API_URL + ConfigUrlApi.Urls.User.Login, loginInfo);
  }

  async RefreshToken(data: LoginResponse) {
    return this.post(API_URL + ConfigUrlApi.Urls.User.RefreshToken, data);
  }
}

export interface IAuthApi {
  Login(loginInfo: LoginInfo);
  RefreshToken(data: LoginResponse);
}

export const authAPI: IAuthApi = new AuthApi();
