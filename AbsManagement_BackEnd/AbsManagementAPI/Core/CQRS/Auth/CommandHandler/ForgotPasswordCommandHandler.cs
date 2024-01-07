using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.Auth.Command;
using AbsManagementAPI.Core.CQRS.Logged;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Logged.Command;
using AbsManagementAPI.Core.Models.Auth;
using AbsManagementAPI.Core.Models.Mail;
using AbsManagementAPI.Servives;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.Auth.CommandHandler
{
    public class ForgotPasswordCommandHandler : BaseHandler, IRequestHandler<ForgotPasswordCommand, string>
    {
        public static string DataOld = "";

        private readonly IMailService _mail;
        public ForgotPasswordCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, IMailService mail) : base(httpContextAccessor, dataContext, mapper)
        {
            _mail = mail;
        }

        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == request.ForgotPasswordModel.Email, cancellationToken);
            DataOld = JsonConvert.SerializeObject(userExists);

            if (userExists == null)
            {
                throw new CustomMessageException(MessageSystem.AUTH_ERROR, MessageSystem.AUTH_INVALID);
            }

            try
            {
                // Generate OTP
                var otp = GenerateRandomOTP();
                var otpExpiration = DateTime.UtcNow.AddMinutes(10);

                userExists.PasswordResetOTP = otp;
                userExists.PasswordResetOTPExpiration = otpExpiration;

                var emailBody = $"Kinh gui {userExists.HoTen.ToUpper()}, <br /><br /> " +
                                $"Duoi day la ma xac thuc cua ban: <br />" +
                                $"<h1>{otp}</h1>" +
                                $"Day la mot ma xac thuc, khong phai la mat khau. Ma OTP nay co hieu luc den {otpExpiration}. <br /> " +
                                $"Chi can sao chep ma nay va dan vao truong Nhap ma xac minh tai khoan. <br /><br /> " +
                                $"Tran trong, <br /><br /> " +
                                $"Abs Management";

                var mailData = new MailData
                (
                    new List<string> { userExists.Email },
                    "Password Reset OTP - AbsManagement",
                    emailBody,
                    null,
                    userExists.HoTen
                );

                var result = await _mail.SendAsync(mailData, new CancellationToken());

                if (result)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "AuthController",
                        Method = "Update",
                        FunctionName = "FotgotPassword",
                        Status = "Success",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(userExists),
                        Type = "Debug",
                        CreateDate = DateTime.Now,
                    }
                    });
                    _dataContext.CanBos.Update(userExists);
                    await _dataContext.SaveChangesAsync(cancellationToken);
                    return $"success: true, message: {MessageSystem.SEND_MAIL_SUCCESS}";
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "AuthController",
                        Method = "Update",
                        FunctionName = "FotgotPassword",
                        Status = "Fail",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(userExists),
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
                        Controller = "AuthController",
                        Method = "Update",
                        FunctionName = "FotgotPassword",
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

        private string GenerateRandomOTP()
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
