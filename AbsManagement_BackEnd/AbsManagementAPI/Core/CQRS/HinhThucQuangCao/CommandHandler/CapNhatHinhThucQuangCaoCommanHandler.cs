using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Log.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.Log;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.HinhThucQuangCao.CommandHandler
{
    public class CapNhatHinhThucQuangCaoCommanHandler : BaseHandler, IRequestHandler<CapNhatHinhThucQuangCaoCommand, string>
    {
        public static string DataOld = "";
        public CapNhatHinhThucQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatHinhThucQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var HinhThucQuangCao = await _dataContext.HinhThucQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            try
            {
                DataOld = JsonConvert.SerializeObject(HinhThucQuangCao);

                var HinhThucQuangCaoCapNhat = _mapper.Map(request.CapNhatHinhThucQuangCaoModel, HinhThucQuangCao);

                _dataContext.Update(HinhThucQuangCaoCapNhat);
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
                            NewValue = JsonConvert.SerializeObject(HinhThucQuangCaoCapNhat),
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
                            NewValue = JsonConvert.SerializeObject(HinhThucQuangCaoCapNhat),
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
                            NewValue = JsonConvert.SerializeObject(HinhThucQuangCao),
                            Type = "Error",
                            CreateDate = DateTime.Now,
                        }
                });
                throw new CustomMessageException(MessageSystem.UPDATE_FAIL, ex.Message);
            }
        }
    }
}
