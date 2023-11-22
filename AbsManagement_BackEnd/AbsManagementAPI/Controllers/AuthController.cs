using AbsManagementAPI.Core.CQRS.Auth.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.Auth;
using AbsManagementAPI.Core.Models.CanBo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="loginModel"></param>
        /// <response code="200">Đăng nhập thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<LoginResponseModel> Login(LoginModel loginModel)
        {
            return await _mediator.Send(new LoginCommand()
            {
                LoginModel = loginModel
            });
        }

        /// <summary>
        /// Refresh token mới
        /// </summary>
        /// <param name="refreshTokenModel"></param>
        /// <response code="200">Refresh token thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> RefreshToken(RefreshTokenModel refreshTokenModel)
        {
            return await _mediator.Send(new RefreshTokenCommand()
            {
                RefreshTokenModel = refreshTokenModel
            });
        }
    }
}
