export interface BaseBangQuangCao {
    idDiemDatQuangCao:number;
    idLoaiBangQuangCao:number;
    danhSachHinhAnh: string[];
    kichThuoc: string;
    ngayHetHan: Date;
    ngayBatDau: Date;
    idTinhTrang:string;
    tenCongTy:string;
    email:string,
    soDienThoai: string,
    diaChiCongTy: string
}

export interface BangQuangCaoModel extends BaseBangQuangCao{
    id: number;
    diaChi: string;
    phuong: string;
    quan: string;
    tenLoaiBangQuangCao:string;
    danhSachViTri: number[];
}

export interface CapNhatBangQuangCaoModel extends BaseBangQuangCao{
}

export interface ThemMoiBangQuangCaoModel extends BaseBangQuangCao{
}

export interface XoaBangQuangCaoModel{
    id: number;
}
