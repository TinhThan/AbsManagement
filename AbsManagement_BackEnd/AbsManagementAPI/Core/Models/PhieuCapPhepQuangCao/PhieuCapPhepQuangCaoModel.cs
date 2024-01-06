using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;

namespace AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao
{
    public class PhieuCapPhepQuangCaoModel
    {
        public int Id { get; set; }
        public string IdTinhTrang { get; set; }
        public int IdBangQuangCao { get; set; }
        public int IdCanBoDuyet { get; set; }
        public int IdCanBoGui { get; set; }

        //Thông tin cán bộ
        public string TenCanBoGui { get; set; }
        public string EmailCanBoGui { get; set; }

        public string? TenCanBoDuyet { get; set; }
        public string? EmailCanBoDuyet { get; set; }

        public DateTimeOffset NgayGui { get; set; }
        public DateTimeOffset? NgayDuyet { get; set; }

        //Thông tin bảng quảng cáo
        public string TenLoaiBangQuangCao { get; set; }
        public string DiaChi { get; set; }
        public string KichThuoc { get; set; }
        public List<string> DanhSachHinhAnh { get; set; }
        public List<decimal> DanhSachViTri { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
        public DateTimeOffset NgayBatDau { get; set; }
        public string TenCongTy { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChiCongTy { get; set; }
    }
}
