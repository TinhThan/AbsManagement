namespace AbsManagementAPI.Core.Models.User
{
    public class UserModel
    {
        public string Email { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai {  get; set; }
        public List<string> Role { get; set; }
    }
}
