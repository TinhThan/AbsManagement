import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { AxiosResponse } from "axios";
import { ThemPhieuChinhSuaModel } from "./model";

class PhieuChinhSuaAPI extends BaseApi {
    async DanhSachPhieuSuaDiemDat() {
        return this.get(API_URL + ConfigUrlApi.Urls.PhieuChinhSua.DanhSachDiemDatQuangCao);
    }

    async DanhSachPhieuSuaBangQuangCao() {
        return this.get(API_URL + ConfigUrlApi.Urls.PhieuChinhSua.DanhSachBangQuangCao);
    }

    async TaoMoi(model: ThemPhieuChinhSuaModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.PhieuChinhSua.TaoMoi,
        model
        );
    }

    async CapNhat(id: number) {
        return this.post(API_URL + ConfigUrlApi.Urls.PhieuChinhSua.CapNhat + id);
    }
}

export interface IPhieuChinhSuaAPI {
    DanhSachPhieuSuaDiemDat(): Promise<AxiosResponse>;
    DanhSachPhieuSuaBangQuangCao(): Promise<AxiosResponse>;
    TaoMoi(model: ThemPhieuChinhSuaModel): Promise<AxiosResponse>;
    CapNhat(id:number): Promise<AxiosResponse>;
}

export const phieuChinhSuaAPI: IPhieuChinhSuaAPI = new PhieuChinhSuaAPI();
