using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.Auth.Command;
using AbsManagementAPI.Core.CQRS.Log;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Log.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.Auth.CommandHandler
{
    public class ResetPasswordCommandHandler : BaseHandler, IRequestHandler<ResetPasswordCommand, string>
    {
        public static string DataOld = "";

        public ResetPasswordCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }
        public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == request.ResetPasswordModel.Email, cancellationToken);
            DataOld = JsonConvert.SerializeObject(userExists);

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
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "AuthController",
                        Method = "Update",
                        FunctionName = "ResetPassword",
                        Status = "Success",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(userExists),
                        Type = "Debug",
                        CreateDate = DateTime.Now,
                    }
                    });
                    return $"success: true, message: {MessageSystem.UPDATE_SUCCESS}";
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "AuthController",
                        Method = "Update",
                        FunctionName = "ResetPassword",
                        Status = "Fail",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(userExists),
                        Type = "Debug",
                        CreateDate = DateTime.Now,
                    }
                });
                return $"success: true, message: {MessageSystem.UPDATE_FAIL}";
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
                        FunctionName = "ResetPassword",
                        Status = "Error",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(userExists),
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(ex.Message);
            }
        }
    }
}
