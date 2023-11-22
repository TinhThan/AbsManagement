namespace AbsManagementAPI.Core.Entities
{
    public class PhieuChinhSuaBangQuangCaoEntity
    {
        public PhieuChinhSuaBangQuangCaoEntity()
        {
            ChiTietChinhSuaBangQuangCaos = new HashSet<ChiTietChinhSuaBangQuangCaoEntity>();
        }

        public int Id { get; set; }
        public int IdCanBoTao { get; set; }
        public int? IdCanBoDuyet { get; set; }
        public string LyDo { get; set; }
        public DateTimeOffset NgayCapNhat { get; set; }

        public virtual CanBoEntity CanBoTao { get; set; }
        public virtual CanBoEntity CanBoDuyet { get; set; }
        public virtual ICollection<ChiTietChinhSuaBangQuangCaoEntity> ChiTietChinhSuaBangQuangCaos { get; set; }
    }
}
