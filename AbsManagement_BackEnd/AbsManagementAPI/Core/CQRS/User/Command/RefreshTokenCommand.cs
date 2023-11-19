using AbsManagementAPI.Core.Models.User;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.User.Command
{
    public class RefreshTokenCommand : IRequest<string>
    {
        public RefreshTokenModel RefreshTokenModel { get; set; }
    }
}
