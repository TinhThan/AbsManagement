using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.CanBo;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;

namespace AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao
{
    public class PhieuCapPhepSuaBangQuangCaoModel
    {
        public int Id { get; set; }
        public int? IdDiemDat { get; set; }
        public int? IdBangQuangCao { get; set; }
        public CapNhatBangQuangCaoModel BangQuangCao { get; set; }
        public string TenCanBoGui { get; set; }
        public string EmailCanBoGui { get; set; }

        public string? TenCanBoDuyet { get; set; }
        public string? EmailCanBoDuyet { get; set; }

        public DateTimeOffset? NgayGui { get; set; }
        public DateTimeOffset? NgayDuyet { get; set; }
        public string TinhTrang { get; set; }
    }

    public class PhieuCapPhepSuaDiemDatQuangCaoModel
    {
        public int Id { get; set; }
        public int? IdDiemDat { get; set; }
        public int? IdBangQuangCao { get; set; }
        public CapNhatDiemDatQuangCaoModel DiemDatQuangCao { get; set; }
        public string TenCanBoGui { get; set; }
        public string EmailCanBoGui { get; set; }

        public string? TenCanBoDuyet { get; set; }
        public string? EmailCanBoDuyet { get; set; }

        public DateTimeOffset? NgayGui { get; set; }
        public DateTimeOffset? NgayDuyet { get; set; }
        public string TinhTrang { get; set; }
    }
}
