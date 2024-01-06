using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.Log;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Log.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.CommandHandler
{
    public class XoaBaoCaoViPhamCommandHandler : BaseHandler, IRequestHandler<XoaBaoCaoViPhamCommand, string>
    {
        public XoaBaoCaoViPhamCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaBaoCaoViPhamCommand request, CancellationToken cancellationToken)
        {
            var baoCaoViPham = await _dataContext.BaoCaoViPhams.FirstOrDefaultAsync(t => t.Id == request.XoaBaoCaoViPhamModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(baoCaoViPham);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                      new ThemLogModel
                      {
                          Controller = "BaoCaoViPhamController",
                          Method = "Delete",
                          FunctionName = "XoaBaoCaoViPham",
                          Status = "Success",
                          OleValue = JsonConvert.SerializeObject(baoCaoViPham),
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
                          Controller = "BaoCaoViPhamController",
                          Method = "Delete",
                          FunctionName = "ThemBaoCaoViPham",
                          Status = "Fail",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(baoCaoViPham),
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
                          Controller = "BaoCaoViPhamController",
                          Method = "Create",
                          FunctionName = "ThemBaoCaoViPham",
                          Status = "Error",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(baoCaoViPham),
                          Type = "Error",
                          CreateDate = DateTime.Now,
                      }
                });
                throw new CustomMessageException(MessageSystem.DELETE_FAIL, ex.Message);
            }
        }
    }
}
