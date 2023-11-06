namespace AbsManagementAPI.Core.Entities
{
    public class ChiTietChinhSuaBangQuangCaoEntity
    {
        public int Id { get; set; }
        public int IdBangQuangCao { get; set; }
        public int IdPhieuChinhSua { get; set; }
        public int IdDiemDatQuangCaoMoi { get; set; }
        public int IdLoaiBangQuangCaoMoi { get; set; }
        public string DanhSachHinhAnhMoi { get; set; }
        public DateTimeOffset NgayHetHanMoi { get; set; }
        public string IdTinhTrang { get; set; }

        public virtual BangQuangCaoEntity BangQuangCao { get; set; }
        public virtual PhieuChinhSuaBangQuangCaoEntity PhieuChinhSua { get; set; }
        public virtual DiemDatQuangCaoEntity DiemDatQuangCaoMoi { get; set; }
        public virtual LoaiBangQuangCaoEntity LoaiBangQuangCaoMoi { get; set; }
    }
}
