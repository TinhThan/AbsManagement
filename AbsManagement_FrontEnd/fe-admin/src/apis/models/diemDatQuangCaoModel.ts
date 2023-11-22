interface BaseDiemDatQuangCao {
    DiaChi: string;
    Phuong: string;
    Quan: string;
    DanhSachViTri: string[];
    TenLoaiViTri: string;
    TenHinhThucQuangCao: string;
    DanhSachHinhAnh: string[];
    IdTinhTrang: string;
}

export interface DiemDatQuangCaoModel extends BaseDiemDatQuangCao{
    Id: number;
    NgayCapNhat: Date | null;
}

export interface CapNhatDiemDatQuangCaoModel extends BaseDiemDatQuangCao{
    Id: number;
    NgayCapNhat: Date | null;
}

export interface ThemDiemDatQuangCaoModel extends BaseDiemDatQuangCao{
}

export interface XoaDiemDatQuangCaoModel{
    Id: number;
    NgayCapNhat: Date | null;
}
