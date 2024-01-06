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
        public int IdCanBoDuyet { get; set; }
        public DateTimeOffset NgayGui { get; set; }
    }
}
