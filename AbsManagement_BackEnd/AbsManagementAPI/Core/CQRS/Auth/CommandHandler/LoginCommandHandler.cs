using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.Auth.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.Auth;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AbsManagementAPI.Core.CQRS.Auth.CommandHandler
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommand, LoginResponseModel>
    {
        public LoginCommandHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<LoginResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var password = HelperIdentity.HashPasswordSalt(request.LoginModel.Password);
            var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == request.LoginModel.Email &&
                                        t.MatKhau == password, cancellationToken);
            if(userExists == null)
            {
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR, MessageSystem.AUTH_INVALID);
            }

            var claims = new List<Claim>()
            {
                new Claim(nameof(CanBoEntity.Id),userExists.Id.ToString()),
                new Claim(ClaimTypes.Email,userExists.Email),
                new Claim(ClaimTypes.Name,userExists.HoTen),
                new Claim(ClaimTypes.Role,userExists.Role)
            };

            var refreshToken = userExists.RefreshToken = HelperIdentity.GenerateRefreshToken();
            userExists.RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(CurrentOption.AuthenticationString.ExpiredRefreshToken);

            try
            {
                _dataContext.CanBos.Update(userExists);
                var resultUpdate = await _dataContext.SaveChangesAsync(cancellationToken);
                if (resultUpdate > 0)
                {
                    return new LoginResponseModel()
                    {
                        AccessToken = HelperIdentity.GenerateToken(claims),
                        RefreshToken = refreshToken,
                        Email = userExists.Email,
                        HoTen = userExists.HoTen,
                        SoDienThoai = userExists.SoDienThoai
                    };
                }
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR);
            }
            catch (Exception ex)
            {
                if (ex is CustomException)
                {
                    throw;
                }
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR, ex.Message);
            }
        }
    }
}
