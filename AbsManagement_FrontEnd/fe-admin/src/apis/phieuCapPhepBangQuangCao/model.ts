import { BaseBangQuangCao, ThemMoiBangQuangCaoModel } from "../bangQuangCao/bangQuangCaoModel";
import { CanBoCapPhepBase, CanBoYeuCau } from "../phieuChinhSua/model";

export interface ThemPhieuCapPhepModel extends ThemMoiBangQuangCaoModel{
    TenCongTy: string;
    Email: string;
    SoDienThoai: string;
    DiaChi: string;
}

export interface DanhSachPhieuCapPhepModel extends CanBoYeuCau, CanBoCapPhepBase,  BaseBangQuangCao {
    id: number
}