using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.Log;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Log.Command;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.CommandHandler
{
    public class ThemBaoCaoViPhamCommandHandler : BaseHandler, IRequestHandler<ThemBaoCaoViPhamCommand, string>
    {
        private readonly INotifyService _notifyService;
        public ThemBaoCaoViPhamCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper,INotifyService notifyService)
            : base(httpContextAccessor, dataContext, mapper)
        {
            _notifyService = notifyService;
        }
        public async Task<string> Handle(ThemBaoCaoViPhamCommand request, CancellationToken cancellationToken)
        {
            var baoCaoViPham = _mapper.Map<BaoCaoViPhamEntity>(request.ThemBaoCaoViPhamModel);

            try
            {
                baoCaoViPham.IdTinhTrang = "ChuaXuLy";
                baoCaoViPham.CreateDate = DateTime.Now;
                await _dataContext.AddAsync(baoCaoViPham);
                var resultThemMoi = await _dataContext.SaveChangesAsync();
                if (resultThemMoi > 0)
                {
                    await _notifyService.SendMessageNotifyOnPhuongQuan("ThemBaoCaoViPham", "Bạn có báo cáo vi phạm mới ở địa chỉ: " + baoCaoViPham.DiaChi,baoCaoViPham.Phuong,baoCaoViPham.Quan);
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                      new ThemLogModel
                      {
                          Controller = "BaoCaoViPhamController",
                          Method = "Create",
                          FunctionName = "ThemBaoCaoViPham",
                          Status = "Success",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(baoCaoViPham),
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
                          Controller = "BaoCaoViPhamController",
                          Method = "Create",
                          FunctionName = "ThemBaoCaoViPham",
                          Status = "Fail",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(baoCaoViPham),
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
                          Controller = "BaoCaoViPhamController",
                          Method = "Create",
                          FunctionName = "ThemBaoCaoViPham",
                          Status = "Error",
                          OleValue = "",
                          NewValue = JsonConvert.SerializeObject(baoCaoViPham),
                          Type = "Error",
                          CreateDate = DateTime.Now,
                      }
                });
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
