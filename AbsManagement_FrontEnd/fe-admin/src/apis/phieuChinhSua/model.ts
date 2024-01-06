import { CapNhatBangQuangCaoModel, BaseBangQuangCao } from '../bangQuangCao/bangQuangCaoModel';
import { CapNhatDiemDatQuangCaoModel, BaseDiemDatQuangCao } from './../diemDatQuangCao/diemDatQuangCaoModel';

export interface ThemPhieuChinhSuaModel{
    idDiemDat: number | null;
    idBangQuangCao: number | null;
    capNhatDiemQuangCao: CapNhatDiemDatQuangCaoModel | null;
    capNhatBangQuangCao: CapNhatBangQuangCaoModel | null;
}

export interface CanBoYeuCau {
    tenCanBoDuyet: string;
    emailCanBoDuyet: string
}

export interface CanBoCapPhepBase {
    tenCanBoGui: string;
    emailCanBoGui: string;
}

export interface DanhSachPhieuCapPhepSuaBase extends CanBoYeuCau, CanBoCapPhepBase {
    id: number,
    idDiemDat: number,
    idBangQuangCao: number,
    ngayGui: string,
    ngayDuyet: string,
    tinhTrang: string
}

export interface DanhSachPhieuCapPhepSuaDiemDat extends DanhSachPhieuCapPhepSuaBase, BaseDiemDatQuangCao {}
export interface DanhSachPhieuCapPhepSuaBangQuangCao extends DanhSachPhieuCapPhepSuaBase, BaseBangQuangCao {}