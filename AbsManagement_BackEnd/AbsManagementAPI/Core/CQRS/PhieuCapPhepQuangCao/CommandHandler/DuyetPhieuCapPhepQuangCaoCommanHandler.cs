using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.CommandHandler
{
    public class DuyetPhieuCapPhepQuangCaoCommanHandler : BaseHandler, IRequestHandler<DuyetPhieuCapPhepQuangCaoCommand, string>
    {
        private readonly INotifyService _notifyService;
        public DuyetPhieuCapPhepQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext,INotifyService notifyService, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
            _notifyService = notifyService;
        }

        public async Task<string> Handle(DuyetPhieuCapPhepQuangCaoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var PhieuCapPhepQuangCao = await _dataContext.PhieuCapPhepQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
                var BangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == PhieuCapPhepQuangCao.IdBangQuangCao, cancellationToken);
                PhieuCapPhepQuangCao.IdCanBoDuyet = authInfo.Id;
                PhieuCapPhepQuangCao.NgayDuyet = DateTimeOffset.Now;
                PhieuCapPhepQuangCao.IdTinhTrang = "DaDuyet";

                BangQuangCao.IdTinhTrang = "DaQuyHoach";

                _dataContext.Update(PhieuCapPhepQuangCao);
                _dataContext.Update(BangQuangCao);

                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    _notifyService.SendMessageNotifyByEmail("PhieuCapPhep", "Phiếu cấp phép của bạn đã được duyệt thành công!",PhieuCapPhepQuangCao.IdCanBoGui);
                    return MessageSystem.APPROVE_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.APPROVE_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.APPROVE_FAIL, ex.Message);
            }
        }
    }
}
