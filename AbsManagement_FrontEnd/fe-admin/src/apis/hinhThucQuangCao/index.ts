import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { CapNhatHinhThucQuangCaoModel, ThemMoiHinhThucQuangCaoModel } from "./model";
import { AxiosResponse } from "axios";

class HinhThucQuangCaoAPI extends BaseApi {
    isPublic_API = false;
    async DanhSach() {
        return this.get(
            API_URL + ConfigUrlApi.Urls.HinhThucQuangCao.DanhSach
        );
    }

    async TaoMoi(model: ThemMoiHinhThucQuangCaoModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.HinhThucQuangCao.TaoMoi,
        model
        );
    }

    async CapNhat(id:number, model: CapNhatHinhThucQuangCaoModel) {
        return this.post(
            API_URL + ConfigUrlApi.Urls.HinhThucQuangCao.CapNhat + id,
            model
        );
    }
}

export interface IHinhThucQuangCaoAPI {
    DanhSach(): Promise<AxiosResponse>;
    TaoMoi(model: ThemMoiHinhThucQuangCaoModel): Promise<AxiosResponse>;
    CapNhat(id:number,model: CapNhatHinhThucQuangCaoModel): Promise<AxiosResponse>;
}

export const hinhThucQuangCaoAPI: IHinhThucQuangCaoAPI = new HinhThucQuangCaoAPI();
