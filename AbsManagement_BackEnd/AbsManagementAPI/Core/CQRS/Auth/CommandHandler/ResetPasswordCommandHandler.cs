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
    public class ResetPasswordCommandHandler : BaseHandler, IRequestHandler<ResetPasswordCommand, string>
    {
        public ResetPasswordCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }
        public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == request.ResetPasswordModel.Email, cancellationToken);

            if (userExists == null)
            {
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR, MessageSystem.AUTH_INVALID);
            }

            try
            {
                userExists.MatKhau = HelperIdentity.HashPasswordBCrypt(request.ResetPasswordModel.Password);
                userExists.NgayCapNhat = DateTime.UtcNow;
                _dataContext.CanBos.Update(userExists);
                var result = await _dataContext.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return $"success: true, message: {MessageSystem.UPDATE_SUCCESS}";
                }
                return $"success: true, message: {MessageSystem.UPDATE_FAIL}";
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(ex.Message);
            }
        }
    }
}
