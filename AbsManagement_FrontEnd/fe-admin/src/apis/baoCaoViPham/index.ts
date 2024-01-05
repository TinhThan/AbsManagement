import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { AxiosResponse } from "axios";
import { CapNhatBaoCaoViPhamModel, ThemMoiBaoCaoViPhamModel } from "./baoCaoViPhamModel";

class BaoCaoViPhamAPI extends BaseApi {
    isPublic_API = true;
    async DanhSach(quan:string | null,phuong:string | null) {
      return this.get(
      API_URL + ConfigUrlApi.Urls.BaoCaoViPham.DanhSach + `quan=${quan || ''}&phuong=${phuong || ''}`
      );
    }

    async ChiTiet(id:number | null) {
      return this.get(
      API_URL + ConfigUrlApi.Urls.BaoCaoViPham.ChiTiet + id
      );
    }

    async TaoMoi(model: ThemMoiBaoCaoViPhamModel) {
        return this.post(
        API_URL + ConfigUrlApi.Urls.BaoCaoViPham.TaoMoi,
        model
        );
    }

    async CapNhat(id:number, model: CapNhatBaoCaoViPhamModel) {
      console.log("id",id)
      this.isPublic_API = false;
        return this.post(
        API_URL + ConfigUrlApi.Urls.BaoCaoViPham.CapNhat + id,
        model
        );
    }
}

export interface IBaoCaoViPhamAPI {
  DanhSach(quan:string | null,phuong:string | null): Promise<AxiosResponse>;
  ChiTiet(id:number | null): Promise<AxiosResponse>;
  TaoMoi(model: ThemMoiBaoCaoViPhamModel): Promise<AxiosResponse>;
  CapNhat(id:number,model: CapNhatBaoCaoViPhamModel): Promise<AxiosResponse>;
}

export const baoCaoViPhamAPI: IBaoCaoViPhamAPI = new BaoCaoViPhamAPI();
