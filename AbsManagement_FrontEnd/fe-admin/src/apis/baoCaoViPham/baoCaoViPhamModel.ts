import { BangQuangCaoModel } from "../bangQuangCao/bangQuangCaoModel";
import { CanBoModel } from "../canBo/canBoModel";
import { DiemDatQuangCaoModel } from "../diemDatQuangCao/diemDatQuangCaoModel";

interface BaseBaoCaoViPham {
    idCanBoXuLy: number | null;
    idBangQuangCao: number | null;
    idDiemDatQuangCao: number | null;
    tenHinhThucBaoCao: string;
    hoTen: string;
    email: string;
    soDienThoai: string;
    idHinhThucBaoCao : number;
    noiDung: string;
    noiDungXuLy: string | null;
    danhSachHinhAnh: string[];
    danhSachViTri: number[];
    diaChi: string;
    phuong: string;
    quan: string;
    createDate: Date;
    idTinhTrang:string;
    hoTenCanBoXuLy: string;
    emailCanBoXuLy: string;
    soDienThoaiCanBoXuLy:string;
}

export interface BaoCaoViPhamModel extends BaseBaoCaoViPham{
    id: number;
    diemDatQuangCao: DiemDatQuangCaoModel;
    bangQuangCao: BangQuangCaoModel;
    canBoXuLy: CanBoModel | null;
    key:number;
}

export interface CapNhatBaoCaoViPhamModel {
}

export interface ThemMoiBaoCaoViPhamModel extends BaseBaoCaoViPham{
}

export interface XoaBaoCaoViPhamModel{
    id: number;
    ngayCapNhat: Date | null;
}
