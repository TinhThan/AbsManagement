namespace AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao
{
    public class ThemPhieuCapPhepSuaQuangCaoModel
    {
        public int IdDiemDat { get; set; }
        public int IdBangQuangCao { get; set; }
        public string NoiDung { get; set; }
        public DateTimeOffset? NgayGui { get; set; }
        public string TinhTrang { get; set; }
    }
}
