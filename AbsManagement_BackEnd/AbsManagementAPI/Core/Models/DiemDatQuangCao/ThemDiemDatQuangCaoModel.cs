using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.DiemDatQuangCao
{
    public class ThemDiemDatQuangCaoModel
    {
        public string DiaChi { get; set; }
        public string Phuong { get; set; }
        public string Quan { get; set; }
        public string ViTri { get; set; }
        public int IdLoaiViTri { get; set; }
        public int IdHinhThucQuangCao { get; set; }
        public List<string> DanhSachHinhAnh { get; set; }
    }
}
