using AbsManagementAPI.Core.Models.Auth;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.Auth.Command
{
    public class VerifiedEmailCommand : IRequest<string>
    {
        public VerifiedEmailModel VerifiedEmailModel { get; set; }
    }
}
