using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;

namespace AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao
{
    public class ThemPhieuCapPhepSuaQuangCaoModel
    {
        public int? IdDiemDat { get; set; }
        public int? IdBangQuangCao { get; set; }
        public CapNhatDiemDatQuangCaoModel? CapNhatDiemQuangCao { get; set; }
        public CapNhatBangQuangCaoModel? CapNhatBangQuangCao { get; set; }
    }
}
