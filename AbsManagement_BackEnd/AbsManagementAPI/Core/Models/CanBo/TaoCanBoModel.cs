namespace AbsManagementAPI.Core.Models.CanBo
{
    public class TaoCanBoModel
    {
        public string Email { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public DateTimeOffset NgaySinh { get; set; }
        public string Role { get; set; } = "CanBoPhuong";
        public string MatKhau { get; set; }
    }
}
