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
    public class HuyPhieuCapPhepQuangCaoCommanHandler : BaseHandler, IRequestHandler<HuyPhieuCapPhepQuangCaoCommand, string>
    {
        private readonly INotifyService _notifyService;
        public HuyPhieuCapPhepQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext,INotifyService notifyService, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
            _notifyService = notifyService;
        }

        public async Task<string> Handle(HuyPhieuCapPhepQuangCaoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var PhieuCapPhepQuangCao = await _dataContext.PhieuCapPhepQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
                var BangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == PhieuCapPhepQuangCao.IdBangQuangCao, cancellationToken);
                PhieuCapPhepQuangCao.IdCanBoDuyet = authInfo.Id;
                PhieuCapPhepQuangCao.NgayDuyet = DateTimeOffset.Now;
                PhieuCapPhepQuangCao.IdTinhTrang = "DaHuy";

                BangQuangCao.IdTinhTrang = "DaHuy";

                _dataContext.Update(PhieuCapPhepQuangCao);
                _dataContext.Update(BangQuangCao);

                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    _notifyService.SendMessageNotifyByEmail("PhieuCapPhep", "Phiếu cấp phép của bạn đã bị hủy!", PhieuCapPhepQuangCao.IdCanBoGui);
                    return MessageSystem.CANCEL_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.CANCEL_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.CANCEL_FAIL, ex.Message);
            }
        }
    }
}
