interface HinhThucQuangCao {
    ma:string;
    ten:string;
}

export interface HinhThucQuangCaoModel extends HinhThucQuangCao{
    id: number;
}

export interface CapNhatHinhThucQuangCaoModel extends HinhThucQuangCao{
}

export interface ThemMoiHinhThucQuangCaoModel extends HinhThucQuangCao{
}
