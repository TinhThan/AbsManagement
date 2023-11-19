using AbsManagementAPI.Core.Models.User;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.User.Command
{
    public class LoginCommand : IRequest<LoginResponseModel>
    {
        public LoginModel LoginModel { get; set; }
    }
}
