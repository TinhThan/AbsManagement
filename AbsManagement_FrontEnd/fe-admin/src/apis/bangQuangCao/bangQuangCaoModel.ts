interface BaseBangQuangCao {
    IdDiemDatQuangCao:number;
    IdLoaiBangQuangCao:number;
    DanhSachHinhAnh: string[];
    KichThuoc: string;
    NgayHetHan: Date;
    NgayCapNhat: Date;
    IdTinhTrang:string;
}

export interface BangQuangCaoModel extends BaseBangQuangCao{
    Id: number;
    DiaChi: string;
    Phuong: string;
    Quan: string;
    TenLoaiBangQuangCao:string;
    DanhSachViTri: string[];
}

export interface CapNhatBangQuangCaoModel extends BaseBangQuangCao{
}

export interface ThemMoiBangQuangCaoModel extends BaseBangQuangCao{
}

export interface XoaBangQuangCaoModel{
    Id: number;
    NgayCapNhat: Date | null;
}
