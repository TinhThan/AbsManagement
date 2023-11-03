import { MenuTree } from ".";

export const menuDefaults:MenuTree[] = [
    {
        id: "TrangChu",
        ten: "Trang Chủ",
        idMenuCha: "",
        thuTu:1,
        url: "",
        ghiChu: "Bản đồ",
        icon:"",
        trangThai: 1,
        children:[
            {
                id: "BangQuangCao_DanhMuc",
                ten: "Danh mục",
                idMenuCha: "BangQuangCao",
                thuTu:1,
                url: "",
                ghiChu: "Bảng quảng cáo danh mục",
                icon:"fas fa-list",
                trangThai: 1,
                children:[
                    {
                        id: "BangQuangCao_DanhMuc_DanhSach",
                        ten: "Danh sách",
                        idMenuCha: "BangQuangCao_DanhMuc",
                        thuTu:1,
                        url: 'bang-quang-cao/danhsach',
                        ghiChu: "Danh sách bảng quảng cáo",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },
                    {
                        id: "BangQuangCao_DanhMuc_Map",
                        ten: "Bản đồ",
                        idMenuCha: "BangQuangCao_DanhMuc",
                        thuTu:2,
                        url: 'bang-quang-cao/bando',
                        ghiChu: "Bản đồ bảng quảng cáo",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    }
                ]
            },
            {
                id: "BangQuangCao_BaoCao",
                ten: "Báo cáo",
                idMenuCha: "BangQuangCao",
                thuTu:2,
                url:"",
                ghiChu: "Bảng quảng cáo báo cáo",
                icon:"fas fa-file-alt",
                trangThai: 1,
                children:[]
            },
        ]
    },
    {
        id: "BaoCaoViPham",
        ten: "Báo cáo vi phạm",
        idMenuCha: "",
        thuTu:2,
        url: "",
        ghiChu: "Báo cáo vi pham",
        icon:"",
        trangThai: 1,
        children:[
            {
                id: "BaoCaoViPham_DanhMuc",
                ten: "Danh mục",
                idMenuCha: "BaoCaoViPham",
                thuTu:1,
                url: "",
                ghiChu: "Báo cáo vi phạm danh mục",
                icon:"fas fa-list",
                trangThai: 1,
                children:[
                    {
                        id: "BaoCaoViPham_DanhMuc_DanhSach",
                        ten: "Danh sách",
                        idMenuCha: "BaoCaoViPham_DanhMuc",
                        thuTu:1,
                        url: 'bao-cao-vi-pham/danhsach',
                        ghiChu: "Danh sách báo cáo vi phạm",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },
                    {
                        id: "BaoCaoViPham_DanhMuc_Map",
                        ten: "Bản đồ",
                        idMenuCha: "BaoCaoViPham_DanhMuc",
                        thuTu:2,
                        url: 'bao-cao-vi-pham/bando',
                        ghiChu: "Bản đồ báo cáo vi phạm",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    }
                ]
            },
            {
                id: "BaoCaoViPham_BaoCao",
                ten: "Báo cáo",
                idMenuCha: "BaoCaoViPham",
                thuTu:2,
                url: "",
                ghiChu: "Báo cáo vi phạm báo cáo",
                icon:"fas fa-file-alt",
                trangThai: 1,
                children:[]
            },
        ]
    }
]


export const menuCanBoSos:MenuTree[] = [
    {
        id: "QuanTri",
        ten: "Quản Tri",
        idMenuCha: "",
        thuTu:1,
        url: "",
        ghiChu: "Quản trị",
        icon:"",
        trangThai: 1,
        children:[
            {
                id: "DanhMuc_QuanTri",
                ten: "Danh mục",
                idMenuCha: "QuanTri",
                thuTu:1,
                url: "",
                ghiChu: "Quản trị danh mục",
                icon:"fas fa-list",
                trangThai: 1,
                children:[
                    {
                        id: "DanhMuc_CanBo",
                        ten: "Cán bộ",
                        idMenuCha: "DanhMuc_QuanTri",
                        thuTu:1,
                        url: 'can-bo',
                        ghiChu: "Quản lý cán bộ",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },
                    {
                        id: "DanhMuc_Quan",
                        ten: "Quận",
                        idMenuCha: "DanhMuc_QuanTri",
                        thuTu:2,
                        url: 'quan',
                        ghiChu: "Quản trị quận",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },
                    {
                        id: "DanhMuc_Phuong",
                        ten: "Phường",
                        idMenuCha: "DanhMuc_QuanTri",
                        thuTu:3,
                        url: 'phuong',
                        ghiChu: "Quản trị phường",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    }
                ]
            },
            {
                id: "ThietLap_QuanTri",
                ten: "Thiết lập",
                idMenuCha: "QuanTri",
                thuTu:2,
                url:"",
                ghiChu: "Quản trị thiết lập",
                icon:"fas fa-file-alt",
                trangThai: 1,
                children:[
                    {
                        id: "HinhThucQuangCao",
                        ten: "Hình thứ quảng cáo",
                        idMenuCha: "ThietLap_QuanTri",
                        thuTu:1,
                        url: 'hinh-thuc-quang-cao',
                        ghiChu: "Thiết lập hình thức quảng cáo",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },{
                        id: "LoaiViTri",
                        ten: "Loại vị trí",
                        idMenuCha: "ThietLap_QuanTri",
                        thuTu:2,
                        url: 'loai-vi-tri',
                        ghiChu: "Thiết lập loại vị trí",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },
                    {
                        id: "LoaiBangQuangCao",
                        ten: "Loại bảng quảng cáo",
                        idMenuCha: "ThietLap_QuanTri",
                        thuTu:3,
                        url: 'loai-bang-quang-cao',
                        ghiChu: "Thiết lập loại bảng quảng cáo",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },{
                        id: "HinhThucBaoCao",
                        ten: "Hình thức báo cáo",
                        idMenuCha: "ThietLap_QuanTri",
                        thuTu:4,
                        url: 'hinh-thuc-bao-cao',
                        ghiChu: "Thiết lập hình thức báo cáo",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },
                ]
            },
        ]
    },
    {
        id: "QuangCao",
        ten: "Quảng cáo",
        idMenuCha: "",
        thuTu:2,
        url: "",
        ghiChu: "Quảng cao",
        icon:"",
        trangThai: 1,
        children:[
            {
                id: "QuangCao_DanhMuc",
                ten: "Danh mục",
                idMenuCha: "QuangCao",
                thuTu:1,
                url: "",
                ghiChu: "Danh mục quảng cáo",
                icon:"fas fa-list",
                trangThai: 1,
                children:[
                    {
                        id: "DanhMuc_DiemDatQuangCao",
                        ten: "Điểm đặt quảng cáo",
                        idMenuCha: "QuangCao_DanhMuc",
                        thuTu:4,
                        url: 'diem-dat-quang-cao',
                        ghiChu: "Quản trị điểm đặt quảng cáo",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },
                    {
                        id: "DanhMuc_BangQuangCao",
                        ten: "Bảng quảng cáo",
                        idMenuCha: "QuangCao_DanhMuc",
                        thuTu:5,
                        url: 'bang-quang-cao',
                        ghiChu: "Quản trị bảng quảng cáo",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    }
                ]
            },
            {
                id: "QuangCao_TinhNang",
                ten: "Tính năng",
                idMenuCha: "QuangCao",
                thuTu:1,
                url: "",
                ghiChu: "Tính năng quảng cáo",
                icon:"fas fa-list",
                trangThai: 1,
                children:[
                    {
                        id: "TinhNang_DuyetDiemQuangCao",
                        ten: "Duyệt điểm quảng cáo",
                        idMenuCha: "QuangCao_TinhNang",
                        thuTu:1,
                        url: 'duyet-diem-dat-quang-cao',
                        ghiChu: "Duyệt điểm đặt quảng cáo",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },
                    {
                        id: "TinhNang_DuyetBangQuangCao",
                        ten: "Duyệt bảng quảng cáo",
                        idMenuCha: "QuangCao_TinhNang",
                        thuTu:2,
                        url: 'duyet-bang-quang-cao',
                        ghiChu: "Duyệt bảng quảng cáo",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },
                    {
                        id: "TinhNang_DuyetCapPhepQuangCao",
                        ten: "Duyệt cấp phép quảng cáo",
                        idMenuCha: "QuangCao_TinhNang",
                        thuTu:3,
                        url: 'duyet-cap-phep-quang-cao',
                        ghiChu: "Duyệt cấp phép quảng cáo",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    }
                ]
            },
            {
                id: "QuangCao_ThongKe",
                ten: "Thống kê",
                idMenuCha: "QuangCao",
                thuTu:1,
                url: "",
                ghiChu: "Thống kê quảng cáo",
                icon:"fas fa-list",
                trangThai: 1,
                children:[
                    {
                        id: "ThongKe_Phuong",
                        ten: "Thống kê phường",
                        idMenuCha: "QuangCao_ThongKe",
                        thuTu:1,
                        url: 'thong-ke-phuong',
                        ghiChu: "Thống kê phường",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    },
                    {
                        id: "ThongKe_Quan",
                        ten: "Thống kê Quận",
                        idMenuCha: "QuangCao_ThongKe",
                        thuTu:2,
                        url: 'thong-ke-quan',
                        ghiChu: "Thống kê quận",
                        icon:"",
                        trangThai: 1,
                        children:[]
                    }
                ]
            }
        ]
    }
]
