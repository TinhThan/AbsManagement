using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.BangQuangCao
{
    public class ThemBangQuangCaoModel
    {
        public int IdDiemDatQuangCao { get; set; }
        public int IdLoaiBangQuangCao { get; set; }
        public string KichThuoc { get; set; }
        public List<string> DanhSachHinhAnh { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
    }
}
