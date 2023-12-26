interface LoaiBangQuangCao {
    ma:string;
    ten:string;
}

export interface LoaiBangQuangCaoModel extends LoaiBangQuangCao{
    id: number;
}

export interface CapNhatLoaiBangQuangCaoModel extends LoaiBangQuangCao{
}

export interface ThemMoiLoaiBangQuangCaoModel extends LoaiBangQuangCao{
}
