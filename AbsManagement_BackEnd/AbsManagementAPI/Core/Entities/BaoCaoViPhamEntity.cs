namespace AbsManagementAPI.Core.Entities
{
    public class BaoCaoViPhamEntity
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
        public string ViTri {  get; set; }
        public string DanhSachHinhAnh {  get; set; }
        public string IdTinhTrang { get; set; }
        public string DiaChi { get; set; }
        public string Phuong { get; set; }
        public string Quan { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual HinhThucBaoCaoEntity HinhThucBaoCao { get; set; }
        public virtual DiemDatQuangCaoEntity DiemDatQuangCao { get; set; }
        public virtual BangQuangCaoEntity BangQuangCao { get; set; }
        public virtual CanBoEntity CanBoXuLy { get; set; }
    }
}
