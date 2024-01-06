using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.CQRS.Log.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.Log;
using AbsManagementAPI.Core.Models.Mail;
using AbsManagementAPI.Servives;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.CanBo.CommandHandler
{
    public class TaoCanBoCommandHandler : BaseHandler, IRequestHandler<TaoCanBoCommand, string>
    {
        private readonly IMailService _mail;
        public TaoCanBoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, IMailService mail) : base(httpContextAccessor, dataContext, mapper)
        {
            _mail = mail;
        }

        public async Task<string> Handle(TaoCanBoCommand request, CancellationToken cancellationToken)
        {
            var reqRegister = request.TaoCanBoModel;
            var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == reqRegister.Email, cancellationToken);
            if (userExists != null)
            {
                return $"success: false, message: {MessageSystem.AUTH_EXIT}";
            }

            try
            {
                var canBoMoi = _mapper.Map<CanBoEntity>(reqRegister);
                var refreshToken = HelperIdentity.GenerateRefreshToken();

                //initial value
                canBoMoi.EmailVerified = 0;
                canBoMoi.PasswordResetOTP = "0";
                canBoMoi.PasswordResetOTPExpiration = DateTime.UtcNow;
                canBoMoi.MatKhau = HelperIdentity.HashPasswordBCrypt("1");
                canBoMoi.NgayCapNhat = DateTime.UtcNow;
                canBoMoi.RefreshToken = refreshToken;
                canBoMoi.RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(CurrentOption.AuthenticationString.ExpiredRefreshToken);

                await _dataContext.CanBos.AddAsync(canBoMoi);
                var result = await _dataContext.SaveChangesAsync(cancellationToken);
                //var slug = HelperIdentity.GenerateSlug(reqRegister.Email, reqRegister.SoDienThoai);

                //var emailBody = $"Hi {reqRegister.HoTen.ToUpper()}, <br /><br /> " +
                //   $"Thank you for joining AbsManagement! To activate your account and start exploring, please click the verification link below: <br /><br /> " +
                //   $"<a href=https://localhost:3000/?id={slug}>https://localhost:3000?id={slug}</a> <br /><br /> " +
                //   $"Best Regards, <br /><br /> " +
                //   $"Abs Management Admin";

                //var mailData = new MailData
                //(
                //    new List<string> { reqRegister.Email },
                //    "Welcome to AbsManagement",
                //    emailBody,
                //    null,
                //    reqRegister.HoTen
                //);

                if (result > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                       new ThemLogModel
                       {
                           Controller = "CanBoController",
                           Method = "Create",
                           FunctionName = "ThemCanBo",
                           Status = "Success",
                           OleValue = "",
                           NewValue = JsonConvert.SerializeObject(canBoMoi),
                           Type = "Debug",
                           CreateDate = DateTime.Now,
                       }
                    });
                    //await _mail.SendAsync(mailData, new CancellationToken());
                    return $"success: true, message: {MessageSystem.ADD_SUCCESS}";
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                       new ThemLogModel
                       {
                           Controller = "CanBoController",
                           Method = "Create",
                           FunctionName = "ThemCanBo",
                           Status = "Fail",
                           OleValue = "",
                           NewValue = JsonConvert.SerializeObject(canBoMoi),
                           Type = "Debug",
                           CreateDate = DateTime.Now,
                       }
                });
                return $"success: false, message: {MessageSystem.AUTH_REGISTER_ERROR}";
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                       new ThemLogModel
                       {
                           Controller = "CanBoController",
                           Method = "Create",
                           FunctionName = "ThemCanBo",
                           Status = "Error",
                           OleValue = "",
                           NewValue = JsonConvert.SerializeObject(reqRegister),
                           Type = "Error",
                           CreateDate = DateTime.Now,
                       }
                });
                throw new CustomMessageException(ex.Message);
            }
        }
    }
}
