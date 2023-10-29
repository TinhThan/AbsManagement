using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.BangQuangCao
{
    public class ThemBangQuangCaoModel
    {
        public string DiaChi { get; set; }
        public string Phuong { get; set; }
        public string Quan { get; set; }
        public List<string> DanhSachViTri { get; set; } = new List<string>();
        public string MaLoaiViTri { get; set; }
        public string MaHinhThucQuangCao { get; set; }
        public string MaLoaiBangQuangCao { get; set; }
        public List<string> DanhSachHinhAnh { get; set; } = new List<string>();
        public string KichThuoc { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
    }
}
