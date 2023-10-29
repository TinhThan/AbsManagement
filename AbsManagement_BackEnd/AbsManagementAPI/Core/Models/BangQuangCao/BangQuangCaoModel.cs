using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.BangQuangCao
{
    public class BangQuangCaoModel
    {
        public int Id { get; set; }
        public string DiaChi { get; set; }
        public string Phuong { get; set; }
        public string Quan { get; set; }
        public List<string> DanhSachViTri { get; set; }
        public string MaLoaiViTri { get; set; }
        public string MaHinhThucQuangCao { get; set; }
        public string MaLoaiBangQuangCao { get; set; }
        public List<string> DanhSachHinhAnh { get; set; }
        public string KichThuoc { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
        public TrangThaiBangQuangCao TrangThai { get; set; }
        public DateTimeOffset? NgayCapNhat { get; set; }
        public string? NhanVienCapNhat { get; set; }
    }
}
