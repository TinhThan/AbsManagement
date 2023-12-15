using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.Auth.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AbsManagementAPI.Core.CQRS.Auth.CommandHandler
{
    public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommand, string>
    {
        public RefreshTokenCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pricipal = HelperIdentity.GetPrincipalFromExpiredToken(request.RefreshTokenModel.AccessToken);
                var email = pricipal.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Email)?.Value;

                var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == email, cancellationToken);

                if (userExists == null)
                {
                    throw new CustomMessageException("Người dùng không tồn tại", email);
                }

                if (userExists.RefreshToken != request.RefreshTokenModel.RefreshToken)
                {
                    throw new CustomMessageException(MessageSystem.REFRESH_TOKEN_INVALID, email);
                }

                if (userExists.RefreshTokenExpiryTime < DateTime.UtcNow)
                {
                    throw new CustomMessageException(MessageSystem.REFRESH_TOKEN_EXPIRED, email);
                }

                var newAccessToken = HelperIdentity.GenerateToken(pricipal.Claims.ToList());
                return newAccessToken;
            }
            catch (Exception e)
            {
                if (e is CustomException)
                {
                    throw;
                }
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR, e.Message);
            }
        }
    }
}
