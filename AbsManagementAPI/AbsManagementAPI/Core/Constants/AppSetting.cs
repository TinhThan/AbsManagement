namespace AbsManagementAPI.Core.Constants
{
    public class AppSetting
    {
        public AuthenticationString AuthenticationStrings { get; set; }
    }

    public class CurrentOption
    {
        public static AuthenticationString AuthenticationString { get; set; } = new AuthenticationString();
    }

    public class AuthenticationString
    {
        public int ExpiredToken { get; set; }
        public int ExpiredRefreshToken { get; set; }
        public string Issuer { get; set; }
        public string PrivateKey { get; set; }
        public string ApiName { get; set; }

        public AuthenticationString()
        {
        }
    }
}
