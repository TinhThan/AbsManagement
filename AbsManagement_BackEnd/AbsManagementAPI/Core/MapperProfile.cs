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
                                    : JsonConvert.DeserializeObject<List<string>>(e.DanhSachHinhAnh)))
                .ForMember(src => src.DanhSachViTri, desc => desc.MapFrom(e => e.ViTri == null ? new List<string>()
                                    : JsonConvert.DeserializeObject<List<string>>(e.ViTri)));

            CreateMap<ThemBangQuangCaoModel,BangQuangCaoEntity>()
                .ForMember(src => src.NgayCapNhat, desc => desc.Ignore())
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.NhanVienCapNhat, desc => desc.Ignore())
                .ForMember(src => src.TrangThai, desc => desc.Ignore())
                .ForMember(src => src.LoaiViTri, desc => desc.Ignore())
                .ForMember(src => src.LoaiBangQuangCao, desc => desc.Ignore())
                .ForMember(src => src.HinhThucQuangCao, desc => desc.Ignore())
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachHinhAnh)))
                .ForMember(src => src.ViTri, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachViTri)));

            CreateMap<CapNhatBangQuangCaoModel, BangQuangCaoEntity>()
                .ForMember(src => src.NgayCapNhat, desc => desc.Ignore())
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.NhanVienCapNhat, desc => desc.Ignore())
                .ForMember(src => src.LoaiViTri, desc => desc.Ignore())
                .ForMember(src => src.LoaiBangQuangCao, desc => desc.Ignore())
                .ForMember(src => src.HinhThucQuangCao, desc => desc.Ignore())
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachHinhAnh)))
                .ForMember(src => src.ViTri, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachViTri)));
        }
    }
}
