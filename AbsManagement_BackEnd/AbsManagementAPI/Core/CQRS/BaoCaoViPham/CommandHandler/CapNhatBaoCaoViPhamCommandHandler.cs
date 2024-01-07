using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.Logged;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Logged.Command;
using AbsManagementAPI.Core.Models.Mail;
using AbsManagementAPI.Servives;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.CommandHandler
{
    public class CapNhatBaoCaoViPhamCommandHandler : BaseHandler, IRequestHandler<CapNhatBaoCaoViPhamCommand, string>
    {
        private readonly IMailService _mail;
        public static string DataOld = "";

        public CapNhatBaoCaoViPhamCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, IMailService mail) : base(httpContextAccessor, dataContext, mapper)
        {
            _mail = mail;
        }
        public async Task<string> Handle(CapNhatBaoCaoViPhamCommand request, CancellationToken cancellationToken)
        {
            var baoCaoViPham = await _dataContext.BaoCaoViPhams.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            DataOld = JsonConvert.SerializeObject(baoCaoViPham);
            if (baoCaoViPham == null )
            {
                throw new CustomMessageException(MessageSystem.DATA_INVALID);

            }
            try
            {
                var emailBody = $"Kinh gui {request.CapNhatBaoCaoViPhamModel.userName}, <br /><br /> " +
                                $"Chung toi da cap nhat thong tin bao cao cua ban. Cam on ban da phan hoi. <br /><br /> " +
                                $"Tran trong, <br /><br /> " +
                                $"Abs Management";

                var mailData = new MailData
                (
                    new List<string> { request.CapNhatBaoCaoViPhamModel.userEmail },
                    "Report status - AbsManagement",
                    emailBody,
                    null,
                    request.CapNhatBaoCaoViPhamModel.userName
                );

                baoCaoViPham.NoiDungXuLy = request.CapNhatBaoCaoViPhamModel.NoiDungXyLy;
                baoCaoViPham.IdTinhTrang = request.CapNhatBaoCaoViPhamModel.IdTinhTrang;
                //baoCaoViPham.ApproveDate = DateTime.Now;
                baoCaoViPham.IdCanBoXuLy = authInfo.Id;             //baoCaoViPham.DanhSachHinhAnhXuLy = JsonConvert.SerializeObject(request.CapNhatBaoCaoViPhamModel.DanhSachHinhAnhXuLy);
                _dataContext.Update(baoCaoViPham);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                await _mail.SendAsync(mailData, new CancellationToken());

                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                     new ThemLogModel
                     {
                         Controller = "BaoCaoViPhamController",
                         Method = "Update",
                         FunctionName = "CapNhatBaoCaoViPham",
                         Status = "Success",
                         OleValue = DataOld,
                         NewValue = JsonConvert.SerializeObject(baoCaoViPham),
                         Type = "Debug",
                         CreateDate = DateTime.Now,
                     }
                    });
                    return MessageSystem.UPDATE_SUCCESS;
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                     new ThemLogModel
                     {
                         Controller = "BaoCaoViPhamController",
                         Method = "Update",
                         FunctionName = "CapNhatBaoCaoViPham",
                         Status = "Fail",
                         OleValue = DataOld,
                         NewValue = JsonConvert.SerializeObject(baoCaoViPham),
                         Type = "Debug",
                         CreateDate = DateTime.Now,
                     }
                });
                throw new CustomMessageException(MessageSystem.UPDATE_FAIL);
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                     new ThemLogModel
                     {
                         Controller = "BaoCaoViPhamController",
                         Method = "Update",
                         FunctionName = "CapNhatBaoCaoViPham",
                         Status = "Error",
                         OleValue = DataOld,
                         NewValue = JsonConvert.SerializeObject(baoCaoViPham),
                         Type = "Error",
                         CreateDate = DateTime.Now,
                     }
                });
                throw new CustomMessageException(MessageSystem.UPDATE_FAIL, ex.Message);
            }
        }
    }
}
