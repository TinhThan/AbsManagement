using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AbsManagementAPI.Core.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute()
        {
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var accessToken = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];
                if (accessToken == null)
                {
                    context.Result = new JsonResult(new ExceptionResponse
                    {
                        Title = "Xác thực thất bại",
                        Description = "Accesstoken không được rỗng",
                        Detail = "Accesstoken không được rỗng"
                    })
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                    return;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(CurrentOption.AuthenticationString.PrivateKey);
                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Result = new JsonResult(new ExceptionResponse
                    {
                        Title = "Xác thực thất bại",
                        Description = "Xác minh chứng thực không thành công.",
                        Detail = "Xác minh chứng thực không thành công."
                    })
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                    return;
                }

                if (!context.HttpContext.User.Claims.Any())
                {
                    var token = HelperIdentity.ReadToken(accessToken);
                    context.HttpContext.User.AddIdentity(new ClaimsIdentity(token.Claims));
                }
            }
            catch (Exception ex)
            {
                var desc = ex.Message.Contains("IDX10223") ? "Token_Expired" : ex.Message;
                context.Result = new JsonResult(new ExceptionResponse
                {
                    Title = "Xác thực thất bại",
                    Description = desc,
                    Detail = desc
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }



    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute, IAllowAnonymous
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Items["StatusToken"] = "Anonymous";
        }
    }
}
