using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Log;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Log.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class CapNhatBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<CapNhatBangQuangCaoCommand, string>
    {
        public static string DataOld = "";
        public CapNhatBangQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var bangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            try
            {
                DataOld = JsonConvert.SerializeObject(bangQuangCao);
                var bangQuangCaoCapNhat = _mapper.Map(request.CapNhatBangQuangCaoModel, bangQuangCao);
                _dataContext.Update(bangQuangCaoCapNhat);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {

                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                     new ThemLogModel
                     {
                         Controller = "BangQuangCaoController",
                         Method = "Update",
                         FunctionName = "CapNhatBangQuangCao",
                         Status = "Success",
                         OleValue = DataOld,
                         NewValue = JsonConvert.SerializeObject(bangQuangCaoCapNhat),
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
                        Controller = "BangQuangCaoController",
                        Method = "Update",
                        FunctionName = "CapNhatBangQuangCao",
                        Status = "Fail",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(bangQuangCaoCapNhat),
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
                        Controller = "BangQuangCaoController",
                        Method = "Update",
                        FunctionName = "CapNhatBangQuangCao",
                        Status = "Error",
                        OleValue = DataOld,
                        NewValue = JsonConvert.SerializeObject(bangQuangCao),
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.UPDATE_FAIL, ex.Message);
            }
        }
    }
}
