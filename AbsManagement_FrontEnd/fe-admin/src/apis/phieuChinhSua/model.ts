import { CapNhatBangQuangCaoModel } from '../bangQuangCao/bangQuangCaoModel';
import { CapNhatDiemDatQuangCaoModel } from './../diemDatQuangCao/diemDatQuangCaoModel';

export interface ThemPhieuChinhSuaModel{
    idDiemDat: number | null;
    idBangQuangCao: number | null;
    capNhatDiemQuangCao: CapNhatDiemDatQuangCaoModel | null;
    capNhatBangQuangCao: CapNhatBangQuangCaoModel | null;
}