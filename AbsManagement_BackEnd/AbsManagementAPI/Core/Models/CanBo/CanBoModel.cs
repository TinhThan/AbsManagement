using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.CanBo
{
    public class CanBoModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public DateTimeOffset NgaySinh { get; set; }
        public string Role { get; set; }
        public List<string> NoiCongTac { get; set; }
    }
}
