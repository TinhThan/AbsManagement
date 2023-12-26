using AbsManagementAPI.Core.Models.Mail;

namespace AbsManagementAPI.Servives
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData, CancellationToken ct);
    }
}
