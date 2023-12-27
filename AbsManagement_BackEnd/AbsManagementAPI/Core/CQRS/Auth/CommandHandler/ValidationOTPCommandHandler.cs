using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.Auth.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.Auth.CommandHandler
{
    public class ValidationOTPCommandHandler : BaseHandler, IRequestHandler<ValidationOTPCommand, string>
    {
        public ValidationOTPCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }
        public async Task<string> Handle(ValidationOTPCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == request.ValidationOTPModel.Email, cancellationToken);

            if (userExists == null)
            {
                throw new CustomMessageException(MessageSystem.AUTH_ERROR, MessageSystem.AUTH_INVALID);
            }

            if (userExists.PasswordResetOTP.Trim() != request.ValidationOTPModel.OTPCode)
            {
                throw new CustomMessageException(MessageSystem.AUTH_ERROR, MessageSystem.AUTH_OTP_INVALID);
            }
            
            if (userExists.PasswordResetOTPExpiration < DateTime.UtcNow)
            {
                throw new CustomMessageException(MessageSystem.AUTH_ERROR, MessageSystem.AUTH_OTP_EXPRIRED_ITME);
            }

            return $"success: true, message: {MessageSystem.VALID_SUCEESS}";
        }
    }
}
