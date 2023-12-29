using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.DiemDatQuangCao
{
    public class DiemDatQuangCaoModel
    {
        public int Id { get; set; }
        public string DiaChi { get; set; }
        public string Phuong { get; set; }
        public string Quan { get; set; }
        public List<decimal> DanhSachViTri { get; set; }
        public int IdLoaiViTri { get; set; }
        public string TenLoaiViTri { get; set; }
        public int IdHinhThucQuangCao { get; set; }
        public string TenHinhThucQuangCao { get; set; }
        public List<string> DanhSachHinhAnh { get; set; }
        public string IdTinhTrang { get; set; }
        //public virtual ICollection<BangQuangCaoEntity> BangQuangCaos { get; set; }
    }
}
