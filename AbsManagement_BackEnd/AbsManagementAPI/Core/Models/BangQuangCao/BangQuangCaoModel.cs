using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.BangQuangCao
{
    public class BangQuangCaoModel
    {
        public int Id { get; set; }
        public int IdDiemDatQuangCao { get; set; }
        public int IdLoaiBangQuangCao { get; set; }
        public string KichThuoc { get; set; }
        public List<string> DanhSachHinhAnh { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
        public DateTimeOffset? NgayCapNhat { get; set; }
        public string IdTinhTrang { get; set; }
    }
}
