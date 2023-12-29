import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { AxiosResponse } from "axios";
import { CapNhatDiemDatQuangCaoModel, ThemDiemDatQuangCaoModel } from "./diemDatQuangCaoModel";

class DiemDatQuangCaoAPI extends BaseApi {
    isPublic_API = true;
    async DanhSach(quan:string | null,phuong:string | null) {
        return this.get(
        API_URL + ConfigUrlApi.Urls.DiemDatQuangCao.DanhSach + `quan=${quan || ''}&phuong=${phuong || ''}`
        );
    }

    async TaoMoi(model: ThemDiemDatQuangCaoModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.DiemDatQuangCao.TaoMoi,
        model
        );
    }

    async CapNhat(id:number, model: CapNhatDiemDatQuangCaoModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.DiemDatQuangCao.CapNhat + id,
        model
        );
    }
}

export interface IDiemDatQuangCaoAPI {
    DanhSach(quan:string | null,phuong:string | null): Promise<AxiosResponse>;
    TaoMoi(model: ThemDiemDatQuangCaoModel): Promise<AxiosResponse>;
    CapNhat(id:number,model: CapNhatDiemDatQuangCaoModel): Promise<AxiosResponse>;
}

export const diemDatQuangCaoAPI: IDiemDatQuangCaoAPI = new DiemDatQuangCaoAPI();
