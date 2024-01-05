namespace AbsManagementAPI.Core.Entities
{
    public class PhieuCapPhepQuangCaoEntity
    {
        public PhieuCapPhepQuangCaoEntity()
        {
        }
        public int Id { get; set; }
        public int IdDiemDatQuangCao { get; set; }
        public string TenCongTy { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public int IdLoaiBangQuangCao { get; set; }
        public string KichThuoc { get; set; }
        public string DanhSachHinhAnh { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
        public string IdTinhTrang { get; set; }
        public DateTimeOffset NgayBatDau { get; set; }

        public virtual DiemDatQuangCaoEntity DiemDatQuangCao { get; set; }
        public virtual LoaiBangQuangCaoEntity LoaiBangQuangCao { get; set; }
    }
}
