import { Actor } from "../components/TableActor";
import { BaseApi } from "./baseApi";
import { ConfigUrlApi } from "./configs/configUrlApi";
import { API_URL } from "../constants/apiConfig";
import {
  BangQuangCaoModel,
  ThemMoiBangQuangCaoModel,
} from "./models/bangQuangCaoModel";

class BangQuangCaoAPI extends BaseApi {
    async ChiTiet(id: number) {
        return this.get<BangQuangCaoModel>(
        API_URL + ConfigUrlApi.Urls.BangQuangCao.ChiTiet + id
        );
    }

    async TaoMoi(model: ThemMoiBangQuangCaoModel) {
        return this.post<string>(
        API_URL + ConfigUrlApi.Urls.BangQuangCao.TaoMoi,
        model
        );
    }
}

export interface IBangQuangCaoAPI {
  ChiTiet(id: number): Promise<BangQuangCaoModel>;
  TaoMoi(model: ThemMoiBangQuangCaoModel): Promise<string>;
}

export const bangQuangCaoAPI: IBangQuangCaoAPI = new BangQuangCaoAPI();
