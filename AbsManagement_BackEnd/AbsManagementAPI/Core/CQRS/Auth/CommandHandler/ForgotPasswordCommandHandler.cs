﻿using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.Auth.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.Auth;
using AbsManagementAPI.Core.Models.Mail;
using AbsManagementAPI.Servives;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.Auth.CommandHandler
{
    public class ForgotPasswordCommandHandler : BaseHandler, IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly IMailService _mail;
        public ForgotPasswordCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, IMailService mail) : base(httpContextAccessor, dataContext, mapper)
        {
            _mail = mail;
        }

        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == request.ForgotPasswordModel.Email, cancellationToken);

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

                var emailBody = $"Dear {userExists.HoTen.ToUpper()}, <br /><br /> " +
                                $"Here is your access code: <br />" +
                                $"<h1>{otp}</h1>" +
                                $"This is a validation code, not a password. This OTP is valid until {otpExpiration}. <br /> " +
                                $"Simply copy this code and paste into the Account Verification input field. <br /><br /> " +
                                $"Best Regards, <br /><br /> " +
                                $"Abs Management Admin";

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
                    _dataContext.CanBos.Update(userExists);
                    await _dataContext.SaveChangesAsync(cancellationToken);
                    return $"success: true, message: {MessageSystem.SEND_MAIL_SUCCESS}";
                }
                return $"success: false, message: {MessageSystem.AUTH_REGISTER_ERROR}";
            }
            catch (Exception ex)
            {
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