namespace AbsManagementAPI.Core.Entities
{
    public class CanBoEntity
    {
        public CanBoEntity()
        {
            BaoCaoViPhams = new HashSet<BaoCaoViPhamEntity>();
            PhieuChinhSuaBangQuangCao_Taos = new HashSet<PhieuChinhSuaBangQuangCaoEntity>();
            PhieuChinhSuaBangQuangCao_Duyets = new HashSet<PhieuChinhSuaBangQuangCaoEntity>();
            PhieuChinhSuaDiemDatQuangCao_Taos = new HashSet<PhieuChinhSuaDiemDatQuangCaoEntity>();
            PhieuChinhSuaDiemDatQuangCao_Duyets = new HashSet<PhieuChinhSuaDiemDatQuangCaoEntity>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public DateTimeOffset NgaySinh { get; set; }
        public string Role { get; set; }
        public string MatKhau { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
        public string NoiCongTac { get; set; }
        public int EmailVerified { get; set; }
        public DateTimeOffset NgayCapNhat { get; set; }

        public virtual ICollection<BaoCaoViPhamEntity> BaoCaoViPhams { get; set; }
        public virtual ICollection<PhieuChinhSuaBangQuangCaoEntity> PhieuChinhSuaBangQuangCao_Taos { get; set; }
        public virtual ICollection<PhieuChinhSuaBangQuangCaoEntity> PhieuChinhSuaBangQuangCao_Duyets { get; set; }
        public virtual ICollection<PhieuChinhSuaDiemDatQuangCaoEntity> PhieuChinhSuaDiemDatQuangCao_Taos { get; set; }
        public virtual ICollection<PhieuChinhSuaDiemDatQuangCaoEntity> PhieuChinhSuaDiemDatQuangCao_Duyets { get; set; }
    }
}
