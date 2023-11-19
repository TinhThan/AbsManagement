namespace AbsManagementAPI.Core.Models.User
{
    public class LoginResponseModel : RefreshTokenModel
    {
        public string Email { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
    }
}
