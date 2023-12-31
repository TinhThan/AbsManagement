import { BaseApi } from "../baseApi";
import { ConfigUrlApi } from "../configs/configUrlApi";
import { API_URL } from "../../constants/apiConfig";
import { CapNhatHinhThucBaoCaoModel, ThemMoiHinhThucBaoCaoModel } from "./model";
import { AxiosResponse } from "axios";

class HinhThucBaoCaoAPI extends BaseApi {
    isPublic_API = false;
    async DanhSach() {
        this.isPublic_API = true;
        return this.get(
            API_URL + ConfigUrlApi.Urls.HinhThucBaoCao.DanhSach
        );
    }

    async TaoMoi(model: ThemMoiHinhThucBaoCaoModel) {
        this.isPublic_API = false;
        return this.post(
        API_URL + ConfigUrlApi.Urls.HinhThucBaoCao.TaoMoi,
        model
        );
    }

    async CapNhat(id:number, model: CapNhatHinhThucBaoCaoModel) {
        this.isPublic_API = false;
        return this.post(
            API_URL + ConfigUrlApi.Urls.HinhThucBaoCao.CapNhat + id,
            model
        );
    }
}

export interface IHinhThucBaoCaoAPI {
    DanhSach(): Promise<AxiosResponse>;
    TaoMoi(model: ThemMoiHinhThucBaoCaoModel): Promise<AxiosResponse>;
    CapNhat(id:number,model: CapNhatHinhThucBaoCaoModel): Promise<AxiosResponse>;
}

export const hinhThucBaoCaoAPI: IHinhThucBaoCaoAPI = new HinhThucBaoCaoAPI();
