import { ThemMoiBangQuangCaoModel } from "../bangQuangCao/bangQuangCaoModel";

export interface ThemPhieuCapPhepModel extends ThemMoiBangQuangCaoModel{
    TenCongTy: string;
    Email: string;
    SoDienThoai: string;
    DiaChi: string;
}