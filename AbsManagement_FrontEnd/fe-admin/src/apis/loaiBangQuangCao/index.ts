import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { CapNhatLoaiBangQuangCaoModel, ThemMoiLoaiBangQuangCaoModel } from "./model";
import { AxiosResponse } from "axios";

class LoaiBangQuangCaoAPI extends BaseApi {
    isPublic_API = false;
    async DanhSach() {
        return this.get(
            API_URL + ConfigUrlApi.Urls.LoaiBangQuangCao.DanhSach
        );
    }

    async TaoMoi(model: ThemMoiLoaiBangQuangCaoModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.LoaiBangQuangCao.TaoMoi,
        model
        );
    }

    async CapNhat(id:number, model: CapNhatLoaiBangQuangCaoModel) {
        return this.post(
            API_URL + ConfigUrlApi.Urls.LoaiBangQuangCao.CapNhat + id,
            model
        );
    }
}

export interface ILoaiBangQuangCaoAPI {
    DanhSach(): Promise<AxiosResponse>;
    TaoMoi(model: ThemMoiLoaiBangQuangCaoModel): Promise<AxiosResponse>;
    CapNhat(id:number,model: CapNhatLoaiBangQuangCaoModel): Promise<AxiosResponse>;
}

export const loaiBangQuangCaoAPI: ILoaiBangQuangCaoAPI = new LoaiBangQuangCaoAPI();
