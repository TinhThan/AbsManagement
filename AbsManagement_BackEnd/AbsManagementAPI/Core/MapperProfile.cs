using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.BangQuangCao;
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
                                    : JsonConvert.DeserializeObject<List<string>>(e.DanhSachHinhAnh)));

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
        }
    }
}
