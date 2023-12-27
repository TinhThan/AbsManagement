using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.Auth.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.Auth.CommandHandler
{
    public class VerifiedEmailCommandHandler : BaseHandler, IRequestHandler<VerifiedEmailCommand, string>
    {
        public VerifiedEmailCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(VerifiedEmailCommand request, CancellationToken cancellationToken)
        {
            var password = HelperIdentity.HashPasswordBCrypt(request.VerifiedEmailModel.Password);
            var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == request.VerifiedEmailModel.Email, cancellationToken);
            
            if (userExists == null)
            {
                throw new CustomMessageException(MessageSystem.AUTH_ERROR, MessageSystem.AUTH_INVALID);
            }

            if (userExists.EmailVerified == 1)
            {
                throw new CustomMessageException(MessageSystem.AUTH_ERROR, MessageSystem.AUTH_VERIFIED);
            }

            userExists.EmailVerified = 1;
            userExists.MatKhau = password;
            userExists.NgayCapNhat = DateTimeOffset.UtcNow;

            try
            {
                _dataContext.CanBos.Update(userExists);
                var resultUpdate = await _dataContext.SaveChangesAsync(cancellationToken);
                if (resultUpdate > 0)
                {
                    return $"success: true, message: {MessageSystem.AUTH_VERIFIED_SUCCESS}";
                }
                throw new CustomMessageException(MessageSystem.AUTH_ERROR);
            }
            catch (Exception ex)
            {
                if (ex is CustomException)
                {
                    throw;
                }
                throw new CustomMessageException(MessageSystem.AUTH_ERROR, ex.Message);
            }
        }
    }
}
