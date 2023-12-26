using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AbsManagementAPI.Core.Authentication
{
    public class HelperIdentity
    {
        private const string _Salt = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        private const int _iterCount = 10000;

        const string strExpiredDate = "IDX10223";

        public static AuthInfo GetUserContext(HttpContext context)
        {
            var auth = new AuthInfo();
            if (context == null) return auth;
            auth.Id = int.Parse(context.User.Claims.FirstOrDefault(t => t.Type == nameof(CanBoEntity.Id))?.Value ?? "0");
            auth.Email = context.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Email)?.Value ?? "";
            auth.HoTen = context.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Name)?.Value ?? "";
            auth.Role = context.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Role)?.Value ?? "";
            auth.NoiCongTac = JsonConvert.DeserializeObject<List<string>>(context.User.Claims.FirstOrDefault(t => t.Type == nameof(CanBoEntity.NoiCongTac))?.Value ?? "[]");
            return auth;
        }

        //public static List<Claim> GetClaimsFromClient(HttpContext context)
        //{
        //    var claims = new List<Claim>
        //        {
        //            GetClaimFromClient(context, "sub"),
        //            GetClaimFromClient(context, "name"),
        //            GetClaimFromClient(context, nameof(UserContext.ID).ToLower(),"sub"),
        //            GetClaimFromClient(context, nameof(UserContext.UID).ToLower()),
        //            GetClaimFromClient(context, nameof(UserContext.GUID).ToLower()),
        //            GetClaimFromClient(context, nameof(UserContext.ShopID).ToLower())
        //        };
        //    return claims;
        //}

        //public static Claim GetClaimFromClient(HttpContext context, string key)
        //        => new Claim(key, (GetClaimValue(context, $"client_{key}") ?? ""));

        //public static string GetClaimValue(HttpContext context, string key)
        //    => context.User.Claims.FirstOrDefault(t => t.Type == key)?.Value;

        public static async Task ThrowAuthException(HttpContext context, string message, string detail)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = message,
                Detail = detail
            });
        }

        public static string ComputeSHA256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string HashPasswordSalt(string password)
        {
            // derive a 256-bit subkey (use HMACSHA512 with 10,000 iterations)
            var hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(_Salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: _iterCount,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(hashed);
        }

        public static string HashPasswordBCrypt(string password)
        {
            // generate a salt
            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            // hash the password with the generated salt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        public static string GenerateSlug(string email, string phoneNumber)
        {
            string dataToHash = $"{email}:{phoneNumber}";

            using (HMACSHA256 hmac = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(_Salt)))
            {
                byte[] hashedData = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataToHash));
                string hashedHex = BitConverter.ToString(hashedData).Replace("-", "").ToLower();

                return BCrypt.Net.BCrypt.HashPassword(hashedHex, 10);
            }
        }

        public static string GenerateToken(List<Claim> listClaim)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(CurrentOption.AuthenticationString.PrivateKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = CurrentOption.AuthenticationString.Issuer,
                Audience = CurrentOption.AuthenticationString.Issuer,
                Subject = new ClaimsIdentity(listClaim),
                Expires = DateTime.UtcNow.AddMinutes(CurrentOption.AuthenticationString.ExpiredToken),
                SigningCredentials = signinCredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static JwtSecurityToken ReadToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(accessToken);
        }

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(CurrentOption.AuthenticationString.PrivateKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = secretKey
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private static Dictionary<string, string> ListError = new Dictionary<string, string>()
        {
            { strExpiredDate, MessageSystem.TOKEN_EXPIRED }
        };

        public static string StatusError(string exMessage)
        {
            if (exMessage.Contains(strExpiredDate))
            {
                return ListError[strExpiredDate];
            }
            else
            {
                return MessageSystem.AUTH_AUTHENTICATED_ERROR;
            }
        }

        public static string MessageError(string exMessage)
        {
            if (exMessage.Contains(strExpiredDate))
            {
                return ListError[strExpiredDate];
            }
            else
            {
                return exMessage;
            }
        }
    }
}
