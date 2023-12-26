using AbsManagementAPI.Core.Models.Auth;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.Auth.Command
{
    public class ValidationOTPCommand : IRequest<string>
    {
        public ValidationOTPModel ValidationOTPModel { get; set; }
    }
}
