import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { AxiosResponse } from "axios";
import { ThemPhieuCapPhepModel } from "./model";

class PhieuCapPhepBangQuangCaoAPI extends BaseApi {
    async DanhSach() {
        return this.get(API_URL + ConfigUrlApi.Urls.PhieuCapPhep.DanhSach);
    }

    async TaoMoi(model: ThemPhieuCapPhepModel) {
        return this.post(
            API_URL + ConfigUrlApi.Urls.PhieuCapPhep.TaoMoi,
            model
        );
    }

    async ChiTiet(id: number | null) {
        return this.get(
            API_URL + ConfigUrlApi.Urls.PhieuCapPhep.ChiTiet + id
        );
    }

    async Duyet(id: number) {
        return this.post(API_URL + ConfigUrlApi.Urls.PhieuCapPhep.Duyet + id);
    }
}

export interface IPhieuCapPhepBangQuangCaoAPI {
    DanhSach(): Promise<AxiosResponse>;
    TaoMoi(model: ThemPhieuCapPhepModel): Promise<AxiosResponse>;
    ChiTiet(id:number | null): Promise<AxiosResponse>;
    Duyet(id:number): Promise<AxiosResponse>;
}

export const phieuCapPhepBangQuangCaoAPI: IPhieuCapPhepBangQuangCaoAPI = new PhieuCapPhepBangQuangCaoAPI();
