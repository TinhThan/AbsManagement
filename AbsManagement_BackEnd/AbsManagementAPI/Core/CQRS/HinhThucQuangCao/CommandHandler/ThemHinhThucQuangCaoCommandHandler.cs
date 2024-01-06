using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.HinhThucBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Log.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.Log;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.HinhThucBangQuangCao.CommandHandler
{
    public class ThemHinhThucQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemHinhThucQuangCaoCommand, string>
    {
        public ThemHinhThucQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemHinhThucQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var HinhThucQuangCaoMoi = _mapper.Map<HinhThucQuangCaoEntity>(request.ThemHinhThucQuangCaoModel);

            try
            {
                await _dataContext.AddAsync(HinhThucQuangCaoMoi);
                var resultThemMoi = await _dataContext.SaveChangesAsync();
                if (resultThemMoi > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                        new ThemLogModel
                        {
                            Controller = "HinhThucQuangCaoController",
                            Method = "Create",
                            FunctionName = "ThemHinhThucQuangCao",
                            Status = "Success",
                            OleValue = "",
                            NewValue = JsonConvert.SerializeObject(HinhThucQuangCaoMoi),
                            Type = "Debug",
                            CreateDate = DateTime.Now,
                        }
                    });
                    return MessageSystem.ADD_SUCCESS;
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                       new ThemLogModel
                       {
                           Controller = "HinhThucQuangCaoController",
                           Method = "Create",
                           FunctionName = "ThemHinhThucQuangCao",
                           Status = "Fail",
                           OleValue = "",
                           NewValue = JsonConvert.SerializeObject(HinhThucQuangCaoMoi),
                           Type = "Debug",
                           CreateDate = DateTime.Now,
                       }
                });
                throw new CustomMessageException(MessageSystem.ADD_FAIL);
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                       new ThemLogModel
                       {
                           Controller = "HinhThucQuangCaoController",
                           Method = "Create",
                           FunctionName = "ThemHinhThucQuangCao",
                           Status = "Error",
                           OleValue = "",
                           NewValue = JsonConvert.SerializeObject(HinhThucQuangCaoMoi),
                           Type = "Error",
                           CreateDate = DateTime.Now,
                       }
                });
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
