using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.Auth.Command;
using AbsManagementAPI.Core.CQRS.Logged;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Logged.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.Auth.CommandHandler
{
    public class VerifiedEmailCommandHandler : BaseHandler, IRequestHandler<VerifiedEmailCommand, string>
    {
        public static string DataOld = "";

        public VerifiedEmailCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(VerifiedEmailCommand request, CancellationToken cancellationToken)
        {
            var password = HelperIdentity.HashPasswordBCrypt(request.VerifiedEmailModel.Password);
            var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == request.VerifiedEmailModel.Email, cancellationToken);
            DataOld = JsonConvert.SerializeObject(userExists);

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
            userExists.NgayCapNhat = DateTime.UtcNow;

            try
            {
                _dataContext.CanBos.Update(userExists);
                var resultUpdate = await _dataContext.SaveChangesAsync(cancellationToken);
                if (resultUpdate > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "AuthController",
                        Method = "Update",
                        FunctionName = "FotgotVeriedEmail",
                        Status = "Success",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(userExists),
                        Type = "Debug",
                        CreateDate = DateTime.Now,
                    }
                    });
                    return $"success: true, message: {MessageSystem.AUTH_VERIFIED_SUCCESS}";
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "AuthController",
                        Method = "Update",
                        FunctionName = "FotgotVeriedEmail",
                        Status = "Fail",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(userExists),
                        Type = "Debug",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.AUTH_ERROR);
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "AuthController",
                        Method = "Update",
                        FunctionName = "FotgotVeriedEmail",
                        Status = "Error",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(userExists),
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                });
                if (ex is CustomException)
                {
                    throw;
                }
                throw new CustomMessageException(MessageSystem.AUTH_ERROR, ex.Message);
            }
        }
    }
}
