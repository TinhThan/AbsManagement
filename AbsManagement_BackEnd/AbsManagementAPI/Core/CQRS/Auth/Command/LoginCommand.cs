using AbsManagementAPI.Core.Models.Auth;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.Auth.Command
{
    public class LoginCommand : IRequest<LoginResponseModel>
    {
        public LoginModel LoginModel { get; set; }
    }
}
