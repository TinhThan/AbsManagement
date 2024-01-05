import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { AxiosResponse } from "axios";
import { ThemPhieuCapPhepModel } from "./model";

class PhieuCapPhepBangQuangCaoAPI extends BaseApi {
    async TaoMoi(model: ThemPhieuCapPhepModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.PhieuCapPhep.TaoMoi,
        model
        );
    }
}

export interface IPhieuCapPhepBangQuangCaoAPI {
    TaoMoi(model: ThemPhieuCapPhepModel): Promise<AxiosResponse>;
}

export const phieuCapPhepBangQuangCaoAPI: IPhieuCapPhepBangQuangCaoAPI = new PhieuCapPhepBangQuangCaoAPI();
