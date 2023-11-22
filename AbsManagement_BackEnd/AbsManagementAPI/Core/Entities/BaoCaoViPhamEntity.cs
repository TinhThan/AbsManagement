namespace AbsManagementAPI.Core.Entities
{
    public class BaoCaoViPhamEntity
    {
        public int Id { get; set; }
        public int? IdCanBoXuLy { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public int IdHinhThucBaoCao { get; set; }
        public string NoiDung { get; set; }
        public string? NoiDungXuLy { get; set; }
        public string ViTri {  get; set; }
        public string DanhSachHinhAnh {  get; set; }
        public string IdTinhTrang { get; set; }

        public virtual HinhThucBaoCaoEntity HinhThucBaoCao { get; set; }
        public virtual CanBoEntity CanBoXuLy { get; set; }
    }
}
