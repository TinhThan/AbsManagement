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

export interface ForgotPayload {
  email: string
}

export interface AuthBasePayload {
  email: string,
  password: string
}

export interface ValidOTPPayload {
  email: string,
  OTPCode: string
}

class AuthApi extends BaseApi {
  async Login(loginInfo: LoginInfo) {
    return this.post(API_URL + ConfigUrlApi.Urls.User.Login, loginInfo);
  }

  async RefreshToken(data: LoginResponse) {
    return this.post(API_URL + ConfigUrlApi.Urls.User.RefreshToken, data);
  }

  async ForgotPassword(data: ForgotPayload) {
    return this.post(API_URL + ConfigUrlApi.Urls.User.ForgotPassword, data);
  }

  async ValidationOTP(data: ValidOTPPayload) {
    return this.post(API_URL + ConfigUrlApi.Urls.User.ValidOTP, data);
  }

  async ResetPassword(data: AuthBasePayload) {
    return this.post(API_URL + ConfigUrlApi.Urls.User.ResetPassword, data);
  }
}

export interface IAuthApi {
  Login(loginInfo: LoginInfo);
  RefreshToken(data: LoginResponse);
  ForgotPassword(data: ForgotPayload);
  ValidationOTP(data: ValidOTPPayload);
  ResetPassword(data: AuthBasePayload)
}

export const authAPI: IAuthApi = new AuthApi();
