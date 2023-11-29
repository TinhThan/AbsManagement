import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { CapNhatCanBoModel, ThemMoiCanBoModel } from "./canBoModel";
import { AxiosResponse } from "axios";

class CanBoAPI extends BaseApi {
    isPublic_API = true;
    async DanhSach() {
      return this.get(
      API_URL + ConfigUrlApi.Urls.CanBo.DanhSach
      );
    }

    async TaoMoi(model: ThemMoiCanBoModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.CanBo.TaoMoi,
        model
        );
    }

    async CapNhat(id:number, model: CapNhatCanBoModel) {
      console.log("id",id)
        return this.post(
        API_URL + ConfigUrlApi.Urls.CanBo.CapNhat + id,
        model
        );
    }
}

export interface ICanBoAPI {
  DanhSach(): Promise<AxiosResponse>;
  TaoMoi(model: ThemMoiCanBoModel): Promise<AxiosResponse>;
  CapNhat(id:number,model: CapNhatCanBoModel): Promise<AxiosResponse>;
}

export const canBoAPI: ICanBoAPI = new CanBoAPI();
