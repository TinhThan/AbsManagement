namespace AbsManagementAPI.Core.Models.BaoCaoViPham
{
    public class ThemBaoCaoViPhamModel
    {
        public int CanBoXuLy { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string MaHinhThucBaoCao { get; set; }
        public string NoiDungXyLy { get; set; }
        public string ViTri { get; set; }
        public string DanhSachHinhAnh { get; set; }
        public TinhTrangBaoCaoViPham TinhTrang { get; set; }
    }
    public enum TinhTrangBaoCaoViPham
    {
        ChuaXyLy = 1,
        DaXuLy =2,
    }
}
