import { Actor } from "../../components/TableActor";
import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import {
  CapNhatBangQuangCaoModel,
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

    async CapNhat(id: number, model: CapNhatBangQuangCaoModel) {
      return this.post(
      API_URL + ConfigUrlApi.Urls.BangQuangCao.CapNhat + id,
      model
      );
    }

    async Xoa(id: number) {
      return this.post(
      API_URL + ConfigUrlApi.Urls.BangQuangCao.Xoa + id
      );
    }
}

export interface IBangQuangCaoAPI {
  ChiTiet(id: number);
  DanhSach();
  TaoMoi(model: ThemMoiBangQuangCaoModel);
  CapNhat(id: number, model: ThemMoiBangQuangCaoModel);
  Xoa(id: number);
}

export const bangQuangCaoAPI: IBangQuangCaoAPI = new BangQuangCaoAPI();
