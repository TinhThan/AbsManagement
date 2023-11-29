namespace AbsManagementAPI.Core.Models.CanBo
{
    public class CapNhatCanBoModel
    {
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public DateTimeOffset NgaySinh { get; set; }
        public string Role { get; set; } = "CanBoPhuong";
        public List<string> NoiCongTac { get; set; }
    }
}
