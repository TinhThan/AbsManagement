namespace AbsManagementAPI.Core.Models.BaoCaoViPham
{
    public class ThemBaoCaoViPhamModel
    {
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public int IdHinhThucBaoCao { get; set; }
        public string NoiDung { get; set; }
        public decimal[] DanhSachViTri { get; set; }
        public string[] DanhSachHinhAnh { get; set; }
    }
}
