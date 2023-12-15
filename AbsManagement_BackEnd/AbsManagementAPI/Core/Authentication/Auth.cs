﻿namespace AbsManagementAPI.Core.Authentication
{
    public class AuthInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Role { get; set; }
        public List<string> NoiCongTac { get; set; }
    }
}
