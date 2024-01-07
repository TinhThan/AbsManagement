using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Logged;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Logged.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.CommandHandler
{
    public class CapNhatLoaiBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<CapNhatLoaiBangQuangCaoCommand, string>
    {
        public static string DataOld = "";
        public CapNhatLoaiBangQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatLoaiBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var LoaiBangQuangCao = await _dataContext.LoaiBangQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            try
            {
                DataOld = JsonConvert.SerializeObject(LoaiBangQuangCao);
                var LoaiBangQuangCaoCapNhat = _mapper.Map(request.CapNhatLoaiBangQuangCaoModel, LoaiBangQuangCao);

                _dataContext.Update(LoaiBangQuangCaoCapNhat);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                        new ThemLogModel
                        {
                            Controller = "LoaiBangQuangCaoController",
                            Method = "Update",
                            FunctionName = "CapNhatLoaiBangQuangCao",
                            Status = "Success",
                            OleValue = DataOld,
                            NewValue = JsonConvert.SerializeObject(LoaiBangQuangCaoCapNhat),
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
                        Controller = "LoaiBangQuangCaoController",
                        Method = "Update",
                        FunctionName = "CapNhatLoaiBangQuangCao",
                        Status = "Fail",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(LoaiBangQuangCaoCapNhat),
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
                        Controller = "LoaiBangQuangCaoController",
                        Method = "Update",
                        FunctionName = "CapNhatLoaiBangQuangCao",
                        Status = "Error",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(LoaiBangQuangCao),
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.UPDATE_FAIL, ex.Message);
            }
        }
    }
}
