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
        /// Gởi email
        /// </summary>
        /// <param name="mailData"></param>
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
