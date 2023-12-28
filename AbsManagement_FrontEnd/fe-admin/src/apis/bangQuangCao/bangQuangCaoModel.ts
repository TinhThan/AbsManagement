interface BaseBangQuangCao {
    idDiemDatQuangCao:number;
    idLoaiBangQuangCao:number;
    danhSachHinhAnh: string[];
    kichThuoc: string;
    ngayHetHan: Date;
    ngayCapNhat: Date;
    idTinhTrang:string;
}

export interface BangQuangCaoModel extends BaseBangQuangCao{
    id: number;
    diaChi: string;
    phuong: string;
    quan: string;
    tenLoaiBangQuangCao:string;
    danhSachViTri: string[];
}

export interface CapNhatBangQuangCaoModel extends BaseBangQuangCao{
}

export interface ThemMoiBangQuangCaoModel extends BaseBangQuangCao{
}

export interface XoaBangQuangCaoModel{
    id: number;
    ngayCapNhat: Date | null;
}
