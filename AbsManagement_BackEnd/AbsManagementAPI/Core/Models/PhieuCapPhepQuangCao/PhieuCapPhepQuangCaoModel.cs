using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;

namespace AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao
{
    public class PhieuCapPhepQuangCaoModel
    {
        public int Id { get; set; }
        public string IdTinhTrang { get; set; }
        public int IdBangQuangCao { get; set; }
        public int IdCanBoDuyet { get; set; }
        public DateTimeOffset NgayGui { get; set; }

        public virtual BangQuangCaoEntity BangQuangCaos { get; set; }
    }
}
