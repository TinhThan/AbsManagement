import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { AxiosResponse } from "axios";
import { ThemPhieuChinhSuaModel } from "./model";

class PhieuChinhSuaAPI extends BaseApi {
    async TaoMoi(model: ThemPhieuChinhSuaModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.PhieuChinhSua.TaoMoi,
        model
        );
    }
}

export interface IPhieuChinhSuaAPI {
    TaoMoi(model: ThemPhieuChinhSuaModel): Promise<AxiosResponse>;
}

export const phieuChinhSuaAPI: IPhieuChinhSuaAPI = new PhieuChinhSuaAPI();
