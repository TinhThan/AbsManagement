using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;

namespace AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao
{
    public class PhieuCapPhepQuangCaoModel
    {
        public int Id { get; set; }
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
