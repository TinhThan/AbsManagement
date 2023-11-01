interface BaseBangQuangCao {
    DiaChi: string;
    Phuong: string;
    Quan: string;
    DanhSachViTri: string[];
    MaLoaiViTri: string;
    MaHinhThucQuangCao: string;
    MaLoaiBangQuangCao: string;
    DanhSachHinhAnh: string[];
    KichThuoc: string;
    NgayHetHan: Date;
}

export interface BangQuangCaoModel extends BaseBangQuangCao{
    Id: number;
    TrangThai: number;
    NgayCapNhat: Date | null;
    NhanVienCapNhat: string;
}

export interface CapNhatBangQuangCaoModel extends BaseBangQuangCao{
    TrangThai: number;
    NgayCapNhat: Date | null;
}

export interface ThemMoiBangQuangCaoModel extends BaseBangQuangCao{
}

export interface XoaBangQuangCaoModel{
    Id: number;
    NgayCapNhat: Date | null;
}
