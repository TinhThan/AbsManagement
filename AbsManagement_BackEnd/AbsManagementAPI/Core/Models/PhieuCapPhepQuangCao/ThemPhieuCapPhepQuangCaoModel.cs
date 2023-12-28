using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao
{
    public class ThemPhieuCapPhepQuangCaoModel
    {
        public int IdDiemDatQuangCao { get; set; }
        public int IdCongTy { get; set; }
        public int IdLoaiBangQuangCao { get; set; }
        public string KichThuoc { get; set; }
        public string DanhSachHinhAnh { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
        public string IdTinhTrang { get; set; }
        public DateTimeOffset NgayBatDau { get; set; }
    }
}
