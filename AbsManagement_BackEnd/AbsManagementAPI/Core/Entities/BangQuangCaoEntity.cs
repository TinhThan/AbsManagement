namespace AbsManagementAPI.Core.Entities
{
    public class BangQuangCaoEntity
    {
        public int Id { get; set; }
        public string DiaChi { get;set; }
        public string Phuong { get; set; }
        public string Quan {  get; set; }
        public string ViTri { get; set; }
        public string MaLoaiViTri { get; set; }
        public string MaHinhThucQuangCao { get; set; }
        public string MaLoaiBangQuangCao { get; set; }
        public string DanhSachHinhAnh { get; set; }
        public string KichThuoc { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
        public TrangThaiBangQuangCao TrangThai { get; set; } 
        public DateTimeOffset? NgayCapNhat { get; set; }
        public string? NhanVienCapNhat { get; set; }

        public virtual HinhThucQuangCaoEntity HinhThucQuangCao { get; set; }
        public virtual LoaiBangQuangCaoEntity LoaiBangQuangCao { get; set; }
        public virtual LoaiViTriEntity LoaiViTri { get; set; }
    }

    public enum TrangThaiBangQuangCao
    {
        CHUAQUYHOACH,
        DAQUYHOACH
    }
}
