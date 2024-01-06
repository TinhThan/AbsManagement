using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao
{
    public class ThemPhieuCapPhepQuangCaoModel
    {
        public int IdDiemDatQuangCao { get; set; }
        public string TenCongTy { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public int IdLoaiBangQuangCao { get; set; }
        public string KichThuoc { get; set; }
        public List<string> DanhSachHinhAnh { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
        public DateTimeOffset NgayBatDau { get; set; }
    }
}
