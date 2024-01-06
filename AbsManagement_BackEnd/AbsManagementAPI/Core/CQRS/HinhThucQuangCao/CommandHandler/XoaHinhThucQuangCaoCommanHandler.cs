using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Log;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Log.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.HinhThucBangQuangCao.CommandHandler
{
    public class XoaHinhThucQuangCaoCommanHandler : BaseHandler, IRequestHandler<XoaHinhThucQuangCaoCommand, string>
    {
        public XoaHinhThucQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaHinhThucQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var HinhThucQuangCao = await _dataContext.HinhThucQuangCaos.FirstOrDefaultAsync(t => t.Id == request.XoaHinhThucQuangCaoModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(HinhThucQuangCao);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                       new ThemLogModel
                       {
                           Controller = "HinhThucQuangCaoController",
                           Method = "Delete",
                           FunctionName = "XoaHinhThucQuangCao",
                           Status = "Success",
                           OleValue = JsonConvert.SerializeObject(HinhThucQuangCao),
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
                           Controller = "HinhThucQuangCaoController",
                           Method = "Delete",
                           FunctionName = "XoaHinhThucQuangCao",
                           Status = "Fail",
                           OleValue = "",
                           NewValue = JsonConvert.SerializeObject(HinhThucQuangCao),
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
                          Controller = "HinhThucQuangCaoController",
                          Method = "Delete",
                          FunctionName = "XoaHinhThucQuangCao",
                          Status = "Error",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(HinhThucQuangCao),
                          Type = "Error",
                          CreateDate = DateTime.Now,
                      }
                });
                throw new CustomMessageException(MessageSystem.DELETE_FAIL, ex.Message);
            }
        }
    }
}
