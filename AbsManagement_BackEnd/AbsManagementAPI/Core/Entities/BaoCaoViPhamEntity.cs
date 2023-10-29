namespace AbsManagementAPI.Core.Entities
{
    public class BaoCaoViPhamEntity
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string MaHinhThucBaoCao { get; set; }
        public string NoiDung { get; set; }
        public string ViTri {  get; set; }
        public string DanhSachHinhAnh {  get; set; }

        public virtual HinhThucBaoCaoEntity HinhThucBaoCao { get; set; }
    }
}
