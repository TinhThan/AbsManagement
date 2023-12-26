using AbsManagementAPI.Core.Models.Auth;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.Auth.Command
{
    public class ResetPasswordCommand : IRequest<string>
    {
        public ResetPasswordModel ResetPasswordModel { get; set; }
    }
}
