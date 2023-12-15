interface LoaiViTri {
    ma:string;
    ten:string;
}

export interface LoaiViTriModel extends LoaiViTri{
    id: number;
}

export interface CapNhatLoaiViTriModel extends LoaiViTri{
}

export interface ThemMoiLoaiViTriModel extends LoaiViTri{
}
