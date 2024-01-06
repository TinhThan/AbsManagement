using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Log.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.Log;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.CommandHandler
{
    public class XoaLoaiBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<XoaLoaiBangQuangCaoCommand, string>
    {
        public XoaLoaiBangQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaLoaiBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var LoaiBangQuangCao = await _dataContext.LoaiBangQuangCaos.FirstOrDefaultAsync(t => t.Id == request.XoaLoaiBangQuangCaoModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(LoaiBangQuangCao);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                        new ThemLogModel
                        {
                            Controller = "LoaiBangQuangCaoController",
                            Method = "Delete",
                            FunctionName = "XoaLoaiBangQuangCao",
                            Status = "Success",
                            OleValue = JsonConvert.SerializeObject(LoaiBangQuangCao),
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
                        Controller = "LoaiBangQuangCaoController",
                        Method = "Delete",
                        FunctionName = "XoaLoaiBangQuangCao",
                        Status = "Fail",
                        OleValue = "",
                        NewValue = JsonConvert.SerializeObject(LoaiBangQuangCao),
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
                        Controller = "LoaiBangQuangCaoController",
                        Method = "Delete",
                        FunctionName = "XoaLoaiBangQuangCao",
                        Status = "Error",
                        OleValue = "",
                        NewValue = JsonConvert.SerializeObject(LoaiBangQuangCao),
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.DELETE_FAIL, ex.Message);
            }
        }
    }
}
