﻿namespace AbsManagementAPI.Core.Models.Auth
{
    public class LoginResponseModel : RefreshTokenModel
    {
        public string Email { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Role { get; set; }
        public List<string> NoiCongTac { get; set; }
    }
}
