interface HinhThucBaoCao {
    ma:string;
    ten:string;
}

export interface HinhThucBaoCaoModel extends HinhThucBaoCao{
    id: number;
}

export interface CapNhatHinhThucBaoCaoModel extends HinhThucBaoCao{
}

export interface ThemMoiHinhThucBaoCaoModel extends HinhThucBaoCao{
}
