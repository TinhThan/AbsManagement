using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.CQRS.Log.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.Log;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.CanBo.CommandHandler
{
    public class XoaCanBoCommandHandler : BaseHandler, IRequestHandler<XoaCanBoCommand, string>
    {
        public XoaCanBoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaCanBoCommand request, CancellationToken cancellationToken)
        {
            var canBo = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Id == request.XoaCanBoModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(canBo);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                      new ThemLogModel
                      {
                          Controller = "CanBoController",
                          Method = "Delete",
                          FunctionName = "XoaCanBo",
                          Status = "Success",
                          OleValue = JsonConvert.SerializeObject(canBo),
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
                          Controller = "CanBoController",
                          Method = "Delete",
                          FunctionName = "XoaCanBo",
                          Status = "Fail",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(canBo),
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
                          Controller = "CanBoController",
                          Method = "Delete",
                          FunctionName = "XoaCanBo",
                          Status = "Error",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(canBo),
                          Type = "Error",
                          CreateDate = DateTime.Now,
                      }
                });
                throw new CustomMessageException(MessageSystem.DELETE_FAIL, ex.Message);
            }
        }
    }
}
