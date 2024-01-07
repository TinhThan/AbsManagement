using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.HubSignalR;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.CommandHandler
{
    public class ThemPhieuCapPhepQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemPhieuCapPhepQuangCaoCommand, string>
    {
        private readonly INotifyService _notifyService;
        public ThemPhieuCapPhepQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, INotifyService notifyService, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
            _notifyService = notifyService;
        }

        public async Task<string> Handle(ThemPhieuCapPhepQuangCaoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var phieuCapPhepQuangCaoMoi = new PhieuCapPhepQuangCaoEntity()
                {
                    IdTinhTrang = "ChoDuyet",
                    IdBangQuangCao = request.ThemPhieuCapPhepQuangCaoModel.IdBangQuangCao,
                    IdCanBoGui = authInfo.Id,
                    NgayGui = DateTimeOffset.Now
                };
                await _dataContext.AddAsync(phieuCapPhepQuangCaoMoi);
                var resultThemMoi = await _dataContext.SaveChangesAsync();
                if (resultThemMoi > 0)
                {
                    _notifyService.SendMessageNotifyByEmail("PhieuCapPhep", "Bạn có một phiếu cấp phép bảng quảng cáo mới.", phieuCapPhepQuangCaoMoi.IdCanBoGui);
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
