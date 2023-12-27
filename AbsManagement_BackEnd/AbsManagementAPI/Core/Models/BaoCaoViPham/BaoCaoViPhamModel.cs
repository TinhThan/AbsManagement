﻿using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.BaoCaoViPham
{
    public class BaoCaoViPhamModel
    {
        public int Id { get; set; }
        public int? IdCanBoXuLy { get; set; }
        public int? IdBangQuangCao { get; set; }
        public int? IdDiemDatQuangCao { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public int IdHinhThucBaoCao { get; set; }
        public string NoiDung { get; set; }
        public string? NoiDungXuLy { get; set; }
        public List<decimal> DanhSachViTri { get; set; }
        public List<string> DanhSachHinhAnh { get; set; }
        public string IdTinhTrang { get; set; }
        public string DiaChi { get; set; }
        public string Phuong { get; set; }
        public string Quan { get; set; }
        public DateTime CreateDate { get; set; }

        public HinhThucBaoCaoEntity HinhThucBaoCao { get; set; }
        public DiemDatQuangCaoEntity DiemDatQuangCao { get; set; }
        public BangQuangCaoEntity BangQuangCao { get; set; }
        public CanBoEntity CanBoXuLy { get; set; }
    }
}
