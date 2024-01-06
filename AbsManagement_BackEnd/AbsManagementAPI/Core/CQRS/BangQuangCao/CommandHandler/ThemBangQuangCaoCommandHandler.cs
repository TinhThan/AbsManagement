using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Log.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.Log;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class ThemBangQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemBangQuangCaoCommand, string>
    {
        public ThemBangQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var bangQuangCaoMoi = _mapper.Map<BangQuangCaoEntity>(request.ThemBangQuangCaoModel);

            try
            {
                bangQuangCaoMoi.NgayBatDau = DateTimeOffset.UtcNow;
                bangQuangCaoMoi.IdTinhTrang = "ChuaQuyHoach";
                await _dataContext.AddAsync(bangQuangCaoMoi);
                var resultThemMoi = await _dataContext.SaveChangesAsync();
                if (resultThemMoi > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                      new ThemLogModel
                      {
                          Controller = "BangQuangCaoController",
                          Method = "Create",
                          FunctionName = "ThemBangQuangCao",
                          Status = "Success",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(bangQuangCaoMoi),
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
                          Controller = "BangQuangCaoController",
                          Method = "Create",
                          FunctionName = "ThemBangQuangCao",
                          Status = "Fail",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(bangQuangCaoMoi),
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
                          Controller = "BangQuangCaoController",
                          Method = "Create",
                          FunctionName = "ThemBangQuangCao",
                          Status = "Error",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(bangQuangCaoMoi),
                          Type = "Error",
                          CreateDate = DateTime.Now,
                      }
                });
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
