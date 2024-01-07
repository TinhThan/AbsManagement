export const ConfigUrlApi = {
    Urls: {
        User:{
            Login: '/auth/login',
            ForgotPassword: '/auth/forgot-password',
            ResetPassword: '/auth/reset-password',
            ValidOTP: '/auth/validation-OTP',
            Logout: '/logout',
            RefreshToken:'/auth/refreshtoken'
        },
        LoaiViTri:{
            ChiTiet:'/loaivitri/',
            DanhSach:'/loaivitri',
            TaoMoi:'/loaivitri',
            CapNhat:'/loaivitri/',
            Xoa:'/loaivitri/xoa'
        },
        HinhThucBaoCao:{
            DanhSach:'/hinhthucbaocao',
            TaoMoi:'/hinhthucbaocao',
            CapNhat:'/hinhthucbaocao/',
            Xoa:'/hinhthucbaocao/xoa'
        },
        HinhThucQuangCao:{
            DanhSach:'/hinhthucquangcao',
            TaoMoi:'/hinhthucquangcao',
            CapNhat:'/hinhthucquangcao/',
            Xoa:'/hinhthucquangcao/xoa'
        },
        LoaiBangQuangCao:{
            DanhSach:'/loaibangquangcao',
            TaoMoi:'/loaibangquangcao',
            CapNhat:'/loaibangquangcao/',
            Xoa:'/loaibangquangcao/xoa'
        },
        BangQuangCao:{
            ChiTiet:'/bangquangcao/chitiet/',
            DanhSach:'/bangquangcao?',
            DanhSachBySpace:'/bangquangcao/diemdatquangcao/',
            TaoMoi:'/bangquangcao/taomoi',
            CapNhat:'/bangquangcao/capnhat/',
            Gui:'/bangquangcao/gui/',
            Xoa:'/bangquangcao/xoa'
        },
        CanBo:{
            DanhSach:"/canbo/danhsach",
            TaoMoi: "/canbo/taomoi",
            CapNhat: "/canbo/capnhat/"
        },
        BaoCaoViPham:{
            DanhSach:"/baocaovipham?",
            ChiTiet:"/baocaovipham/",
            TaoMoi: "/baocaovipham/taomoi",
            CapNhat: "/baocaovipham/capnhat/"
        },
        DiemDatQuangCao:{
            DanhSach:"/diemdatquangcao?",
            ChiTiet:"/diemdatquangcao/chitiet/",
            TaoMoi: "/diemdatquangcao/taomoi",
            CapNhat: "/diemdatquangcao/capnhat/"
        },
        PhieuChinhSua:{
            TaoMoi: "/phieucapphepsuaquangcao/taomoi",
            DanhSachDiemDatQuangCao: "/phieucapphepsuaquangcao/danhsach/diemdatquangcao",
            DanhSachBangQuangCao: "/phieucapphepsuaquangcao/danhsach/bangquangcao",
            CapNhat: "/phieucapphepsuaquangcao/capnhat/"
        },
        PhieuCapPhep:{
            TaoMoi: "/phieucapphepquangcao/taomoi",
            DanhSach: "/phieucapphepquangcao",
            Duyet: "/phieucapphepquangcao/duyet/"
        }
    }
}