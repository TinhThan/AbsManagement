using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;

namespace AbsManagementAPI.Core.Models.BangQuangCao
{
    public class BangQuangCaoModel
    {
        public int Id { get; set; }
        public int IdDiemDatQuangCao { get; set; }
        public int IdLoaiBangQuangCao { get; set; }
        public string TenLoaiBangQuangCao { get; set; }
        public string KichThuoc { get; set; }
        public List<string> DanhSachHinhAnh { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
        public string IdTinhTrang { get; set; }

        //Thông tin điểm đặt quảng cáo.
        public string DiaChi { get; set; }
        public string Phuong { get; set; }
        public string Quan { get; set; }
        public List<string> DanhSachViTri { get; set; }
    }
}
