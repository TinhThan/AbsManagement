import { Actor } from "../../components/TableActor";
import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import {
  BangQuangCaoModel,
  ThemMoiBangQuangCaoModel,
} from "./bangQuangCaoModel";

class BangQuangCaoAPI extends BaseApi {
    async ChiTiet(id: number) {
        return this.get(
        API_URL + ConfigUrlApi.Urls.BangQuangCao.ChiTiet + id
        );
    }

    async DanhSach() {
      return this.get(
      API_URL + ConfigUrlApi.Urls.BangQuangCao.DanhSach
      );
  }

    async TaoMoi(model: ThemMoiBangQuangCaoModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.BangQuangCao.TaoMoi,
        model
        );
    }
}

export interface IBangQuangCaoAPI {
  ChiTiet(id: number);
  DanhSach();
  TaoMoi(model: ThemMoiBangQuangCaoModel);
}

export const bangQuangCaoAPI: IBangQuangCaoAPI = new BangQuangCaoAPI();
