import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { CapNhatLoaiViTriModel, ThemMoiLoaiViTriModel } from "./model";
import { AxiosResponse } from "axios";

class LoaiViTriAPI extends BaseApi {
    isPublic_API = false;
    async DanhSach() {
      return this.get(
      API_URL + ConfigUrlApi.Urls.LoaiViTri.DanhSach
      );
    }

    async TaoMoi(model: ThemMoiLoaiViTriModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.LoaiViTri.TaoMoi,
        model
        );
    }

    async CapNhat(id:number, model: CapNhatLoaiViTriModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.LoaiViTri.CapNhat + id,
        model
        );
    }
}

export interface ILoaiViTriAPI {
  DanhSach(): Promise<AxiosResponse>;
  TaoMoi(model: ThemMoiLoaiViTriModel): Promise<AxiosResponse>;
  CapNhat(id:number,model: CapNhatLoaiViTriModel): Promise<AxiosResponse>;
}

export const loaiViTriAPI: ILoaiViTriAPI = new LoaiViTriAPI();
