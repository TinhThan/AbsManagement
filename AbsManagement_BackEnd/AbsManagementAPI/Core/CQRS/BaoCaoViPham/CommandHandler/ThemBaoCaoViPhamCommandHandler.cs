using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.HubSignalR;
using AutoMapper;
using MediatR;

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
                    return MessageSystem.ADD_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.ADD_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
