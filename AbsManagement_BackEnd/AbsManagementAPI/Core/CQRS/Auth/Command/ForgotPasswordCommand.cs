using AbsManagementAPI.Core.Models.Auth;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.Auth.Command
{
    public class ForgotPasswordCommand : IRequest<string>
    {
        public ForgotPasswordModel ForgotPasswordModel { get; set; }
    }
}
