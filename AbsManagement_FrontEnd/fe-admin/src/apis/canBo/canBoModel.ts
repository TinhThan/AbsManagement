interface CanBo {
    email:string;
    hoTen:string;
    soDienThoai: string;
    role:string;
    noiCongTac: string[];
}

export interface CanBoModel extends CanBo{
    id: number;
    ngaySinh: Date;
}

export interface CapNhatCanBoModel extends CanBo{
    id: number;
    ngaySinh: any;
}

export interface ThemMoiCanBoModel extends CanBo{
    ngaySinh: any;
}
