using AbsManagementAPI.Core.Models.Auth;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.Auth.Command
{
    public class RefreshTokenCommand : IRequest<string>
    {
        public RefreshTokenModel RefreshTokenModel { get; set; }
    }
}
