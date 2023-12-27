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

        /// <summary>
        /// Verified Email
        /// </summary>
        /// <param name="verifiedEmailModel"></param>
        /// <response code="200">Verified Email thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("verified-email")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> VerifiedEmail(VerifiedEmailModel verifiedEmailModel)
        {
            return await _mediator.Send(new VerifiedEmailCommand()
            {
                VerifiedEmailModel = verifiedEmailModel
            });
        }

        /// <summary>
        /// forgot password
        /// </summary>
        /// <param name="fotgotPasswordModel"></param>
        /// <response code="200">Change password thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> ForgotPassword(ForgotPasswordModel fotgotPasswordModel)
        {
            return await _mediator.Send(new ForgotPasswordCommand()
            {
                ForgotPasswordModel = fotgotPasswordModel
            });
        }

        /// <summary>
        /// Validation OTP
        /// </summary>
        /// <param name="validationOTPModel"></param>
        /// <response code="200">Verified OTP thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("validation-OTP")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> ValidationOTPCode(ValidationOTPModel validationOTPModel)
        {
            return await _mediator.Send(new ValidationOTPCommand()
            {
                ValidationOTPModel = validationOTPModel
            });
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <response code="200">Change password thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            return await _mediator.Send(new ResetPasswordCommand()
            {
                ResetPasswordModel = resetPasswordModel
            });
        }
    }
}
