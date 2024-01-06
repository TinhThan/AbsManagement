using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao
{
    public class ThemPhieuCapPhepQuangCaoModel
    {
        public int IdBangQuangCao { get; set; }
        public int IdCanBoDuyet { get; set; }
        public DateTimeOffset NgayGui { get; set; }
    }
}
