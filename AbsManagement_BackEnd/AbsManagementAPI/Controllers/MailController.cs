using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.Mail;
using AbsManagementAPI.Servives;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mail;

        public MailController(IMailService mail)
        {
            _mail = mail;
        }

        /// <summary>
        /// Gữi email
        /// </summary>
        /// <param name="mailData"></param>
        /// <response code="200">Lấy Gữi email thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("sendmail")]
        public async Task<IActionResult> SendMailAsync(MailData mailData)
        {
            bool result = await _mail.SendAsync(mailData, new CancellationToken());

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }
    }
}
