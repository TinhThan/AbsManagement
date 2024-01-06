using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Log;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Log.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.CommandHandler
{
    public class CapNhatDiemDatQuangCaoCommanHandler : BaseHandler, IRequestHandler<CapNhatDiemDatQuangCaoCommand, string>
    {
        public static string DataOld = "";
        public CapNhatDiemDatQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatDiemDatQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var diemDatQuangCao = await _dataContext.DiemDatQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            try
            {
                DataOld = JsonConvert.SerializeObject(diemDatQuangCao);
                var diemDatQuangCaoCapNhat = _mapper.Map(request.CapNhatDiemDatQuangCaoModel, diemDatQuangCao);
                _dataContext.Update(diemDatQuangCaoCapNhat);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                       new ThemLogModel
                       {
                           Controller = "DiemDatQuangCaoController",
                           Method = "Update",
                           FunctionName = "CapNhatDiemDatQuangCao",
                           Status = "Success",
                           OleValue = DataOld,
                           NewValue = JsonConvert.SerializeObject(diemDatQuangCaoCapNhat),
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
                           Controller = "DiemDatQuangCaoController",
                           Method = "Update",
                           FunctionName = "CapNhatDiemDatQuangCao",
                           Status = "Fail",
                           OleValue = DataOld,
                           NewValue = JsonConvert.SerializeObject(diemDatQuangCaoCapNhat),
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
                          Controller = "DiemDatQuangCaoController",
                          Method = "Update",
                          FunctionName = "CapNhatDiemDatQuangCao",
                          Status = "Error",
                          OleValue = DataOld,
                          NewValue = JsonConvert.SerializeObject(diemDatQuangCao),
                          Type = "Error",
                          CreateDate = DateTime.Now,
                      }
                });
                throw new CustomMessageException(MessageSystem.UPDATE_FAIL, ex.Message);
            }
        }
    }
}
