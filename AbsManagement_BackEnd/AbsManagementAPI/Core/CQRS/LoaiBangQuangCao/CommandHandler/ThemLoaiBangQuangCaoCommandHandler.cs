using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using AbsManagementAPI.Core.Logged.Command;
using AbsManagementAPI.Core.CQRS.Logged;

namespace AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.CommandHandler
{
    public class ThemLoaiBangQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemLoaiBangQuangCaoCommand, string>
    {
        public ThemLoaiBangQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemLoaiBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var LoaiBangQuangCaoMoi = _mapper.Map<LoaiBangQuangCaoEntity>(request.ThemLoaiBangQuangCaoModel);

            try
            {
                await _dataContext.AddAsync(LoaiBangQuangCaoMoi);
                var resultThemMoi = await _dataContext.SaveChangesAsync();
                if (resultThemMoi > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                        new ThemLogModel
                        {
                            Controller = "LoaiBangQuangCaoController",
                            Method = "Create",
                            FunctionName = "ThemLoaiBangQuangCao",
                            Status = "Success",
                            OleValue = "",
                            NewValue = JsonConvert.SerializeObject(LoaiBangQuangCaoMoi),
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
                        Controller = "LoaiBangQuangCaoController",
                        Method = "Create",
                        FunctionName = "ThemLoaiBangQuangCao",
                        Status = "Fail",
                        OleValue = "",
                        NewValue = JsonConvert.SerializeObject(LoaiBangQuangCaoMoi),
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
                        Controller = "LoaiBangQuangCaoController",
                        Method = "Create",
                        FunctionName = "ThemLoaiBangQuangCao",
                        Status = "Error",
                        OleValue = "",
                        NewValue = JsonConvert.SerializeObject(LoaiBangQuangCaoMoi),
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                 });
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
