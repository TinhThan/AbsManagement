﻿using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.Auth.Command;
using AbsManagementAPI.Core.CQRS.Logged;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Logged.Command;
using AbsManagementAPI.Core.Models.Auth;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AbsManagementAPI.Core.CQRS.Auth.CommandHandler
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommand, LoginResponseModel>
    {
        public LoginCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<LoginResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Email == request.LoginModel.Email, cancellationToken);
            if (userExists == null)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                   new ThemLogModel
                   {
                       Controller = "AuthController",
                       Method = "Create",
                       FunctionName = "Login",
                       Status = "Error",
                       OleValue = "",
                       NewValue = "",
                       Type = "Error",
                       CreateDate = DateTime.Now,
                   }
                });
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR, MessageSystem.AUTH_INVALID);
            }

            bool password_check = BCrypt.Net.BCrypt.Verify(request.LoginModel.Password, userExists.MatKhau);
            if (!password_check)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                   new ThemLogModel
                   {
                       Controller = "AuthController",
                       Method = "Create",
                       FunctionName = "Login",
                       Status = "Fail",
                       OleValue = "",
                       NewValue = "",
                       Type = "Debug",
                       CreateDate = DateTime.Now,
                   }
                });
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR, MessageSystem.AUTH_PASSWORD_ERROR);
            }

            if (userExists.EmailVerified == 0)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "AuthController",
                        Method = "Create",
                        FunctionName = "Login",
                        Status = "Fail",
                        OleValue = "",
                        NewValue = "",
                        Type = "Debug",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR, MessageSystem.AUTH_NOT_VERIFIED);
            }

            var claims = new List<Claim>()
            {
                new Claim(nameof(CanBoEntity.Id),userExists.Id.ToString()),
                new Claim(ClaimTypes.Email,userExists.Email),
                new Claim(ClaimTypes.Name,userExists.HoTen),
                new Claim(ClaimTypes.Role,userExists.Role),
                new Claim(nameof(CanBoEntity.NoiCongTac),userExists.NoiCongTac),
            };

            var refreshToken = userExists.RefreshToken = HelperIdentity.GenerateRefreshToken();
            userExists.RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(CurrentOption.AuthenticationString.ExpiredRefreshToken);

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
                         Method = "Create",
                         FunctionName = "Login",
                         Status = "Success",
                         OleValue = "",
                         NewValue = "",
                         Type = "Debug",
                         CreateDate = DateTime.Now,
                     }
                    });
                    return new LoginResponseModel()
                    {
                        AccessToken = HelperIdentity.GenerateToken(claims),
                        RefreshToken = refreshToken,
                        Email = userExists.Email,
                        HoTen = userExists.HoTen,
                        SoDienThoai = userExists.SoDienThoai,
                        Role = userExists.Role,
                        NoiCongTac = string.IsNullOrEmpty(userExists.NoiCongTac) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(userExists.NoiCongTac)
                    };
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "AuthController",
                        Method = "Create",
                        FunctionName = "Login",
                        Status = "Fail",
                        OleValue = "",
                        NewValue = "",
                        Type = "Debug",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR);
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "AuthController",
                        Method = "Create",
                        FunctionName = "Login",
                        Status = "Error",
                        OleValue = "",
                        NewValue = "",
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                });
                if (ex is CustomException)
                {
                    throw;
                }
                throw new CustomMessageException(MessageSystem.AUTH_AUTHENTICATED_ERROR, ex.Message);
            }
        }
    }
}
