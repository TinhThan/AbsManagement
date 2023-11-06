namespace AbsManagementAPI.Core.Entities
{
    public class ChiTietPhieuChinhSuaDiemDatQuangCaoEntity
    {
        public int Id { get; set; }
        public int IdPhieuSua { get; set; }
        public int IdDiemDatQuangCao { get; set; }
        public string DiaChiMoi { get; set; }
        public string PhuongMoi { get; set; }
        public string QuanMoi { get; set; }
        public string ViTriMoi { get; set; }
        public int IdLoaiVitriMoi { get; set; }
        public int IdHinhThucMoi { get; set; }
        public string DanhSachHinhAnhMoi { get; set; }
        public string IdTinhTrang { get; set; }

        public virtual PhieuChinhSuaDiemDatQuangCaoEntity PhieuChinhSua { get; set; }
        public virtual DiemDatQuangCaoEntity DiemDatQuangCao { get; set; }
        public virtual LoaiViTriEntity LoaiViTriMoi { get; set; }
        public virtual HinhThucQuangCaoEntity HinhThucQuangCaoMoi { get; set; }
    }
}
