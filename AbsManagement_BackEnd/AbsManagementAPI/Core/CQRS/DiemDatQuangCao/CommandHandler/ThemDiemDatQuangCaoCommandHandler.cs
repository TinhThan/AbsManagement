using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.DiemDatBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Logged;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Logged.Command;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.CommandHandler
{
    public class ThemDiemDatQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemDiemDatQuangCaoCommand, string>
    {
        public ThemDiemDatQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemDiemDatQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var diemDatQuangCaoMoi = _mapper.Map<DiemDatQuangCaoEntity>(request.ThemDiemDatQuangCaoModel);

            try
            {
                diemDatQuangCaoMoi.IdTinhTrang = "ChuaQuyHoach";
                var json = JsonConvert.SerializeObject(diemDatQuangCaoMoi);
                await _dataContext.AddAsync(diemDatQuangCaoMoi);
                var resultThemMoi = await _dataContext.SaveChangesAsync();
                if (resultThemMoi > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                        new ThemLogModel
                        {
                            Controller = "DiemDatQuangCaoController",
                            Method = "Create",
                            FunctionName = "ThemDiemDatQuangCao",
                            Status = "Success",
                            OleValue = "",
                            NewValue = json,
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
                        Controller = "DiemDatQuangCaoController",
                        Method = "Create",
                        FunctionName = "ThemDiemDatQuangCao",
                        Status = "Success",
                        OleValue = "",
                        NewValue = json,
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
                        Controller = "DiemDatQuangCaoController",
                        Method = "Create",
                        FunctionName = "ThemDiemDatQuangCao",
                        Status = "Error",
                        OleValue = "",
                        NewValue = JsonConvert.SerializeObject(diemDatQuangCaoMoi),
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
