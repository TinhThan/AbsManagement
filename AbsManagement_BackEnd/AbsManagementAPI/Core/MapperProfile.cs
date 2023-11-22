using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.CanBo;
using AutoMapper;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BangQuangCaoEntity,BangQuangCaoModel>()
                .ForMember(src=>src.DanhSachHinhAnh,desc=>desc.MapFrom(e=>e.DanhSachHinhAnh == null ? new List<string>() 
                                    : JsonConvert.DeserializeObject<List<string>>(e.DanhSachHinhAnh)))
                .ForMember(src => src.DiaChi, desc => desc.MapFrom(e => e.DiemDatQuangCao.DiaChi))
                .ForMember(src => src.Phuong, desc => desc.MapFrom(e => e.DiemDatQuangCao.Phuong))
                .ForMember(src => src.Quan, desc => desc.MapFrom(e => e.DiemDatQuangCao.Quan))
                .ForMember(src => src.TenLoaiBangQuangCao, desc => desc.MapFrom(e => e.LoaiBangQuangCao.Ten))
                .ForMember(src => src.DanhSachViTri, desc => desc.MapFrom(e => e.DiemDatQuangCao.ViTri == null ? new List<string>()
                                    : JsonConvert.DeserializeObject<List<string>>(e.DiemDatQuangCao.ViTri)));

            CreateMap<ThemBangQuangCaoModel,BangQuangCaoEntity>()
                .ForMember(src => src.NgayCapNhat, desc => desc.Ignore())
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.IdTinhTrang, desc => desc.Ignore())
                .ForMember(src => src.DiemDatQuangCao, desc => desc.Ignore())
                .ForMember(src => src.ChiTietChinhSuaBangQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.LoaiBangQuangCao, desc => desc.Ignore())
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachHinhAnh)));

            CreateMap<CapNhatBangQuangCaoModel, BangQuangCaoEntity>()
                .ForMember(src => src.NgayCapNhat, desc => desc.Ignore())
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.DiemDatQuangCao, desc => desc.Ignore())
                .ForMember(src => src.ChiTietChinhSuaBangQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.LoaiBangQuangCao, desc => desc.Ignore())
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachHinhAnh)));

            CreateMap<TaoCanBoModel, CanBoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.MatKhau, desc => desc.Ignore())
                .ForMember(src => src.RefreshToken, desc => desc.Ignore())
                .ForMember(src => src.RefreshTokenExpiryTime, desc => desc.Ignore())
                .ForMember(src => src.BaoCaoViPhams, desc => desc.Ignore())
                .ForMember(src => src.PhieuChinhSuaBangQuangCao_Taos, desc => desc.Ignore())
                .ForMember(src => src.PhieuChinhSuaBangQuangCao_Duyets, desc => desc.Ignore())
                .ForMember(src => src.PhieuChinhSuaDiemDatQuangCao_Taos, desc => desc.Ignore())
                .ForMember(src => src.PhieuChinhSuaDiemDatQuangCao_Duyets, desc => desc.Ignore());

        }
    }
}
