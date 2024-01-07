using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Logged;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Logged.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class XoaBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<XoaBangQuangCaoCommand, string>
    {
        public XoaBangQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var bangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            try
            {
                _dataContext.Remove(bangQuangCao);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                      new ThemLogModel
                      {
                          Controller = "BangQuangCaoController",
                          Method = "Delete",
                          FunctionName = "XoaBangQuangCao",
                          Status = "Success",
                          OleValue = JsonConvert.SerializeObject(bangQuangCao),
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
                          Controller = "BangQuangCaoController",
                          Method = "Delete",
                          FunctionName = "XoaBangQuangCao",
                          Status = "Fail",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(bangQuangCao),
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
                          Controller = "BangQuangCaoController",
                          Method = "Deltete",
                          FunctionName = "XoaBangQuangCao",
                          Status = "Error",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(bangQuangCao),
                          Type = "Error",
                          CreateDate = DateTime.Now,
                      }
                });
                throw new CustomMessageException(MessageSystem.DELETE_FAIL, ex.Message);
            }
        }
    }
}
