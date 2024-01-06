namespace AbsManagementAPI.Core.Entities
{
    public class PhieuCapPhepQuangCaoEntity
    {
        public PhieuCapPhepQuangCaoEntity()
        {
        }
        public int Id { get; set; }
        public string IdTinhTrang { get; set; }
        public int IdBangQuangCao { get; set; }
        public int IdCanBoGui { get; set; }
        public int IdCanBoDuyet { get; set; }
        public DateTimeOffset NgayGui { get; set; }

        public virtual BangQuangCaoEntity BangQuangCao { get; set; }
        public virtual CanBoEntity CanBoGui { get; set; }
        public virtual CanBoEntity CanBoDuyet { get; set; }
    }
}
