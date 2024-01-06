using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.BaoCaoViPham;
using AbsManagementAPI.Core.Models.CanBo;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using AbsManagementAPI.Core.Models.HinhThucBaoCao;
using AbsManagementAPI.Core.Models.HinhThucQuangCao;
using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using AbsManagementAPI.Entities;
using AbsManagementAPI.Core.Models.LoaiViTri;
using AutoMapper;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region BangQuangCao

            CreateMap<BangQuangCaoEntity, BangQuangCaoModel>()
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => e.DanhSachHinhAnh == null ? new List<string>()
                                    : JsonConvert.DeserializeObject<List<string>>(e.DanhSachHinhAnh)))
                .ForMember(src => src.DiaChi, desc => desc.MapFrom(e => e.DiemDatQuangCao.DiaChi))
                .ForMember(src => src.Phuong, desc => desc.MapFrom(e => e.DiemDatQuangCao.Phuong))
                .ForMember(src => src.Quan, desc => desc.MapFrom(e => e.DiemDatQuangCao.Quan))
                .ForMember(src => src.TenLoaiBangQuangCao, desc => desc.MapFrom(e => e.LoaiBangQuangCao.Ten))
                .ForMember(src => src.DanhSachViTri, desc => desc.MapFrom(e => e.DiemDatQuangCao.ViTri == null ? new List<string>()
                                    : JsonConvert.DeserializeObject<List<string>>(e.DiemDatQuangCao.ViTri)));

            CreateMap<ThemBangQuangCaoModel, BangQuangCaoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.DiemDatQuangCao, desc => desc.Ignore())
                .ForMember(src => src.LoaiBangQuangCao, desc => desc.Ignore())
                .ForMember(src => src.BaoCaoViPhams, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepSuaQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachHinhAnh)));

            CreateMap<CapNhatBangQuangCaoModel, BangQuangCaoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.DiemDatQuangCao, desc => desc.Ignore())
                .ForMember(src => src.LoaiBangQuangCao, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepSuaQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.BaoCaoViPhams, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachHinhAnh)));

            #endregion

            #region CanBo

            CreateMap<TaoCanBoModel, CanBoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.NoiCongTac, desc => desc.MapFrom(t => JsonConvert.SerializeObject(t.NoiCongTac)))
                .ForMember(src => src.MatKhau, desc => desc.Ignore())
                .ForMember(src => src.RefreshToken, desc => desc.Ignore())
                .ForMember(src => src.RefreshTokenExpiryTime, desc => desc.Ignore())
                .ForMember(src => src.EmailVerified, desc => desc.Ignore())
                .ForMember(src => src.PasswordResetOTP, desc => desc.Ignore())
                .ForMember(src => src.PasswordResetOTPExpiration, desc => desc.Ignore())
                .ForMember(src => src.NgayCapNhat, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepSuaQuangCaoGuis, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepSuaQuangCaoDuyets, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepQuangCaoDuyets, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepQuangCaoGuis, desc => desc.Ignore())
                .ForMember(src => src.BaoCaoViPhams, desc => desc.Ignore());
            CreateMap<CapNhatCanBoModel, CanBoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.Email, desc => desc.Ignore())
                .ForMember(src => src.NoiCongTac, desc => desc.MapFrom(t => JsonConvert.SerializeObject(t.NoiCongTac)))
                .ForMember(src => src.MatKhau, desc => desc.Ignore())
                .ForMember(src => src.RefreshToken, desc => desc.Ignore())
                .ForMember(src => src.RefreshTokenExpiryTime, desc => desc.Ignore())
                .ForMember(src => src.EmailVerified, desc => desc.Ignore())
                .ForMember(src => src.PasswordResetOTP, desc => desc.Ignore())
                .ForMember(src => src.PasswordResetOTPExpiration, desc => desc.Ignore())
                .ForMember(src => src.NgayCapNhat, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepSuaQuangCaoGuis, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepSuaQuangCaoDuyets, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepQuangCaoDuyets, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepQuangCaoGuis, desc => desc.Ignore())
                .ForMember(src => src.BaoCaoViPhams, desc => desc.Ignore());

            CreateMap<CanBoEntity, CanBoModel>()
                .ForMember(src => src.NoiCongTac, desc => desc.MapFrom(t => JsonConvert.DeserializeObject<List<string>>(t.NoiCongTac)));

            #endregion

            #region BaoCaoViPham

            CreateMap<ThemBaoCaoViPhamModel, BaoCaoViPhamEntity>()
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachHinhAnh)))
                .ForMember(src => src.ViTri, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachViTri)))
                .ForMember(src => src.IdTinhTrang, desc => desc.Ignore())
                .ForMember(src => src.HinhThucBaoCao, desc => desc.Ignore())
                .ForMember(src => src.CanBoXuLy, desc => desc.Ignore())
                .ForMember(src => src.NoiDungXuLy, desc => desc.Ignore())
                .ForMember(src => src.IdCanBoXuLy, desc => desc.Ignore())
                .ForMember(src => src.CreateDate, desc => desc.Ignore())
                .ForMember(src => src.DiemDatQuangCao, desc => desc.Ignore())
                .ForMember(src => src.BangQuangCao, desc => desc.Ignore())
                .ForMember(src => src.DanhSachHinhAnhXuLy, desc => desc.Ignore())
                .ForMember(src => src.ApproveDate, desc => desc.Ignore())
                .ForMember(src => src.Id, desc => desc.Ignore());

            CreateMap<BaoCaoViPhamEntity, BaoCaoViPhamModel>()
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => JsonConvert.DeserializeObject<List<string>>(e.DanhSachHinhAnh)))
                .ForMember(src => src.DanhSachViTri, desc => desc.MapFrom(e => JsonConvert.DeserializeObject<List<decimal>>(e.ViTri)))
                //.ForMember(src => src.CanBoXuLy, desc => desc.Ignore())
                .ForMember(src => src.TenHinhThucBaoCao, desc => desc.MapFrom(e => e.HinhThucBaoCao.Ten));

            #endregion

            #region DiemDatQuangCao

            CreateMap<DiemDatQuangCaoEntity, DiemDatQuangCaoModel>()
                .ForMember(src => src.TenLoaiViTri, desc => desc.MapFrom(e => e.LoaiViTri.Ten))
                .ForMember(src => src.TenHinhThucQuangCao, desc => desc.MapFrom(e => e.HinhThucQuangCao.Ten))
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => e.DanhSachHinhAnh == null ? new List<string>()
                                    : JsonConvert.DeserializeObject<List<string>>(e.DanhSachHinhAnh)))
                .ForMember(src => src.DanhSachViTri, desc => desc.MapFrom(e => string.IsNullOrEmpty(e.ViTri) ? new List<decimal>()
                                    : JsonConvert.DeserializeObject<List<decimal>>(e.ViTri)));

            CreateMap<ThemDiemDatQuangCaoModel, DiemDatQuangCaoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.IdTinhTrang, desc => desc.Ignore())
                .ForMember(src => src.LoaiViTri, desc => desc.Ignore())
                .ForMember(src => src.HinhThucQuangCao, desc => desc.Ignore())
                .ForMember(src => src.BangQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.BaoCaoViPhams, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepSuaQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachHinhAnh)))
                .ForMember(src => src.ViTri, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachViTri)));

            CreateMap<CapNhatDiemDatQuangCaoModel, DiemDatQuangCaoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.IdTinhTrang, desc => desc.Ignore())
                .ForMember(src => src.HinhThucQuangCao, desc => desc.Ignore())
                .ForMember(src => src.LoaiViTri, desc => desc.Ignore())
                .ForMember(src => src.BangQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.BaoCaoViPhams, desc => desc.Ignore())
                .ForMember(src => src.PhieuCapPhepSuaQuangCaos, desc => desc.Ignore())
                .ForMember(src => src.DanhSachHinhAnh, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachHinhAnh)))
                .ForMember(src => src.ViTri, desc => desc.MapFrom(e => JsonConvert.SerializeObject(e.DanhSachViTri)));
            #endregion

            #region HinhThucQuangCao

            CreateMap<HinhThucQuangCaoEntity, HinhThucQuangCaoModel>();

            CreateMap<ThemHinhThucQuangCaoModel, HinhThucQuangCaoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.DiemDatQuangCaos, desc => desc.Ignore());

            CreateMap<CapNhatHinhThucQuangCaoModel, HinhThucQuangCaoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.DiemDatQuangCaos, desc => desc.Ignore());

            #endregion

            #region HinhThucBaoCao

            CreateMap<HinhThucBaoCaoEntity, HinhThucBaoCaoModel>();

            CreateMap<ThemHinhThucBaoCaoModel, HinhThucBaoCaoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.BaoCaoViPhams, desc => desc.Ignore());

            CreateMap<CapNhatHinhThucBaoCaoModel, HinhThucBaoCaoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.BaoCaoViPhams, desc => desc.Ignore());

            #endregion

            #region LoaiBangQuangCao

            CreateMap<LoaiBangQuangCaoEntity, LoaiBangQuangCaoModel>();

            CreateMap<ThemLoaiBangQuangCaoModel, LoaiBangQuangCaoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.BangQuangCaos, desc => desc.Ignore());

            CreateMap<CapNhatLoaiBangQuangCaoModel, LoaiBangQuangCaoEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.BangQuangCaos, desc => desc.Ignore());

            #endregion

            #region

            CreateMap<PhieuCapPhepSuaQuangCaoEntity, PhieuCapPhepSuaBangQuangCaoModel>()
                .ForMember(src => src.BangQuangCao, desc => desc.MapFrom(e => e.IdBangQuangCao != null ? JsonConvert.DeserializeObject<CapNhatBangQuangCaoModel>(e.NoiDung) : null))
                .ForMember(src => src.TenCanBoDuyet, desc => desc.MapFrom(e => e.CanBoDuyet != null ? e.CanBoDuyet.HoTen : null))
                .ForMember(src => src.TenCanBoGui, desc => desc.MapFrom(e => e.CanBoGui != null ? e.CanBoGui.HoTen : null))
                .ForMember(src => src.EmailCanBoDuyet, desc => desc.MapFrom(e => e.CanBoDuyet != null ? e.CanBoDuyet.Email : null))
                .ForMember(src => src.EmailCanBoGui, desc => desc.MapFrom(e => e.CanBoGui != null ? e.CanBoGui.Email : null));


            CreateMap<PhieuCapPhepSuaQuangCaoEntity, PhieuCapPhepSuaDiemDatQuangCaoModel>()
                .ForMember(src => src.DiemDatQuangCao, desc => desc.MapFrom(e => e.IdDiemDat != null ? JsonConvert.DeserializeObject<CapNhatDiemDatQuangCaoModel>(e.NoiDung) : null))
                .ForMember(src => src.TenCanBoDuyet, desc => desc.MapFrom(e => e.CanBoDuyet != null ? e.CanBoDuyet.HoTen : null))
                .ForMember(src => src.TenCanBoGui, desc => desc.MapFrom(e => e.CanBoGui != null ? e.CanBoGui.HoTen : null))
                .ForMember(src => src.EmailCanBoDuyet, desc => desc.MapFrom(e => e.CanBoDuyet != null ? e.CanBoDuyet.Email : null))
                .ForMember(src => src.EmailCanBoGui, desc => desc.MapFrom(e => e.CanBoGui != null ? e.CanBoGui.Email : null));

            #endregion

            #region LoaiViTri

            CreateMap<LoaiViTriEntity, LoaiViTriModel>();

            CreateMap<ThemLoaiViTriModel, LoaiViTriEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.DiemDatQuangCaos, desc => desc.Ignore());

            CreateMap<CapNhatLoaiViTriModel, LoaiViTriEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.DiemDatQuangCaos, desc => desc.Ignore());
            #endregion

            #region PhieuCapPhepQuangCao

            CreateMap<PhieuCapPhepQuangCaoEntity, PhieuCapPhepQuangCaoModel>();


            CreateMap<ThemPhieuCapPhepQuangCaoModel, PhieuCapPhepQuangCaoEntity>()
                .ForMember(src => src.IdCanBoGui, desc => desc.Ignore())
                .ForMember(src => src.IdCanBoDuyet, desc => desc.Ignore())
                .ForMember(src => src.NgayGui, desc => desc.Ignore())
                .ForMember(src => src.BangQuangCao, desc => desc.Ignore())
                .ForMember(src => src.NgayDuyet, desc => desc.Ignore())
                .ForMember(src => src.CanBoGui, desc => desc.Ignore())
                .ForMember(src => src.CanBoDuyet, desc => desc.Ignore())
                .ForMember(src => src.IdTinhTrang, desc => desc.Ignore())
                .ForMember(src => src.Id, desc => desc.Ignore());


            CreateMap<CapNhatPhieuCapPhepQuangCaoModel, PhieuCapPhepQuangCaoEntity>()
                                .ForMember(src => src.IdCanBoGui, desc => desc.Ignore())
                .ForMember(src => src.IdCanBoDuyet, desc => desc.Ignore())
                .ForMember(src => src.NgayGui, desc => desc.Ignore())
                .ForMember(src => src.NgayDuyet, desc => desc.Ignore())
                .ForMember(src => src.BangQuangCao, desc => desc.Ignore())
                .ForMember(src => src.CanBoGui, desc => desc.Ignore())
                .ForMember(src => src.CanBoDuyet, desc => desc.Ignore())
               .ForMember(src => src.IdTinhTrang, desc => desc.Ignore())
               .ForMember(src => src.Id, desc => desc.Ignore());

            #endregion

        }
    }
}