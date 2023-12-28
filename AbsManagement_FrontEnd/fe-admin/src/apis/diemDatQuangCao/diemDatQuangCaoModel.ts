interface BaseDiemDatQuangCao {
    diaChi: string;
    phuong: string;
    quan: string;
    danhSachViTri: number[];
    tenLoaiViTri: string;
    tenHinhThucQuangCao: string;
    danhSachHinhAnh: string[];
    idTinhTrang: string;
}

export interface DiemDatQuangCaoModel extends BaseDiemDatQuangCao{
    id: number;
    ngayCapNhat: Date | null;
}

export interface CapNhatDiemDatQuangCaoModel extends BaseDiemDatQuangCao{
    id: number;
    ngayCapNhat: Date | null;
}

export interface ThemDiemDatQuangCaoModel extends BaseDiemDatQuangCao{
}

export interface XoaDiemDatQuangCaoModel{
    id: number;
    ngayCapNhat: Date | null;
}
