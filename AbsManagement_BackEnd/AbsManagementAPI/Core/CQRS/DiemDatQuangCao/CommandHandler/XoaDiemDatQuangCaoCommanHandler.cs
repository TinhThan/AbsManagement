using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Logged;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Logged.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.CommandHandler
{
    public class XoaDiemDatQuangCaoCommanHandler : BaseHandler, IRequestHandler<XoaDiemDatQuangCaoCommand, string>
    {
        public XoaDiemDatQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaDiemDatQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var diemDatQuangCao = await _dataContext.DiemDatQuangCaos.FirstOrDefaultAsync(t => t.Id == request.XoaDiemDatQuangCaoModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(diemDatQuangCao);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                        new ThemLogModel
                        {
                            Controller = "DiemDatQuangCaoController",
                            Method = "Delete",
                            FunctionName = "XoaDiemDatQuangCao",
                            Status = "Success",
                            OleValue = JsonConvert.SerializeObject(diemDatQuangCao),
                            NewValue = "",
                            Type = "Debug",
                            CreateDate = DateTime.Now,
                        }
                    });
                    return MessageSystem.DELETE_SUCCESS;
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                        new ThemLogModel
                        {
                            Controller = "DiemDatQuangCaoController",
                            Method = "Delete",
                            FunctionName = "XoaDiemDatQuangCao",
                            Status = "Fail",
                            OleValue = "",
                            NewValue = JsonConvert.SerializeObject(diemDatQuangCao),
                            Type = "Debug",
                            CreateDate = DateTime.Now,
                        }
                });
                throw new CustomMessageException(MessageSystem.DELETE_FAIL);
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                        new ThemLogModel
                        {
                            Controller = "DiemDatQuangCaoController",
                            Method = "Delete",
                            FunctionName = "XoaDiemDatQuangCao",
                            Status = "Error",
                            OleValue = "",
                            NewValue = JsonConvert.SerializeObject(diemDatQuangCao),
                            Type = "Error",
                            CreateDate = DateTime.Now,
                        }
                });
                throw new CustomMessageException(MessageSystem.DELETE_FAIL, ex.Message);
            }
        }
    }
}
