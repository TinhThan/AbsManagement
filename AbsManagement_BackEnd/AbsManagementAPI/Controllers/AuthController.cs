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
        [HttpPost("login")]
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
        [HttpPost("refreshtoken")]
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
        [HttpPost("verified-email")]
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
        [HttpPost("forgot-password")]
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
        [HttpPost("validation-OTP")]
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
        [HttpPost("reset-password")]
        public async Task<string> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            return await _mediator.Send(new ResetPasswordCommand()
            {
                ResetPasswordModel = resetPasswordModel
            });
        }
    }
}
