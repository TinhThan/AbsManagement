import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { AxiosResponse } from "axios";
import { ThemPhieuChinhSuaModel } from "./model";

class PhieuChinhSuaAPI extends BaseApi {
    async DanhSachPhieuSuaDiemDat() {
        return this.get(API_URL + ConfigUrlApi.Urls.PhieuChinhSua.DanhSachDiemDatQuangCao);
    }

    async ChiTietPhieuSuaDiemDat(id: number | null) {
        return this.get(
            API_URL + ConfigUrlApi.Urls.PhieuChinhSua.ChiTietDiemDatQuangCao + id
        );
    }

    async DanhSachPhieuSuaBangQuangCao() {
        return this.get(API_URL + ConfigUrlApi.Urls.PhieuChinhSua.DanhSachBangQuangCao);
    }

    async ChiTietPhieuSuaBangQuangCao(id: number | null) {
        return this.get(
            API_URL + ConfigUrlApi.Urls.PhieuChinhSua.ChiTietBangQuangCao + id
        );
    }

    async TaoMoi(model: ThemPhieuChinhSuaModel) {
        return this.post(
            API_URL + ConfigUrlApi.Urls.PhieuChinhSua.TaoMoi,
            model
        );
    }

    async CapNhat(id: number, payload: any) {
        return this.post(API_URL + ConfigUrlApi.Urls.PhieuChinhSua.CapNhat + id, payload);
    }
}

export interface IPhieuChinhSuaAPI {
    DanhSachPhieuSuaDiemDat(): Promise<AxiosResponse>;
    DanhSachPhieuSuaBangQuangCao(): Promise<AxiosResponse>;
    ChiTietPhieuSuaDiemDat(id:number | null): Promise<AxiosResponse>;
    ChiTietPhieuSuaBangQuangCao(id:number | null): Promise<AxiosResponse>;
    TaoMoi(model: ThemPhieuChinhSuaModel): Promise<AxiosResponse>;
    CapNhat(id: number, payload: any): Promise<AxiosResponse>;

}

export const phieuChinhSuaAPI: IPhieuChinhSuaAPI = new PhieuChinhSuaAPI();
