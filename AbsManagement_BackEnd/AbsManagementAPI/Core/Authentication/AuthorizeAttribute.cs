using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
                var accessToken = context.HttpContext.Request.Headers["AccessToken"].FirstOrDefault();
                if (accessToken == null)
                {

                }
                //var _itemsDataContext = context.HttpContext.Items;
                //if (_itemsDataContext["StatusToken"].ToString() != MessageSystem.AUTH_AUTHENTICATED_ACCEPT)
                //{
                //    var statusToken = _itemsDataContext["StatusToken"].ToString();
                //    var messageToken = _itemsDataContext["MessageToken"]?.ToString();

                //    if (_itemsDataContext["StatusToken"].ToString() == MessageSystem.AUTH_FORBIDDEN)
                //    {
                //        context.Result = new JsonResult(
                //                        new
                //                        {
                //                            Status = StatusCodes.Status403Forbidden,
                //                            Detail = MessageSystem.AUTH_FORBIDDEN,
                //                            Description = messageToken
                //                        })
                //        {
                //            StatusCode = StatusCodes.Status403Forbidden
                //        };
                //    }
                //    else
                //    {
                //        context.Result = new JsonResult(
                //                        new
                //                        {
                //                            Status = StatusCodes.Status401Unauthorized,
                //                            Detail = statusToken,
                //                            Description = messageToken
                //                        })
                //        {
                //            StatusCode = StatusCodes.Status401Unauthorized
                //        };
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR, ex.ToString());
            }
        }
    }
}
