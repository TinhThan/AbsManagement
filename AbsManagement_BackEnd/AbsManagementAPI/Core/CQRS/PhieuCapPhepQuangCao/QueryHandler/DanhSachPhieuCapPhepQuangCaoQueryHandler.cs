using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.QueryHandler
{
    public class DanhSachPhieuCapPhepQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachPhieuCapPhepQuangCaoQuery, List<PhieuCapPhepQuangCaoModel>>
    {
        private readonly INotifyService _notifyService;
        public DanhSachPhieuCapPhepQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, INotifyService notifyService) : base(httpContextAccessor, dataContext, mapper)
        {
            _notifyService = notifyService;
        }

        public async Task<List<PhieuCapPhepQuangCaoModel>> Handle(DanhSachPhieuCapPhepQuangCaoQuery request, CancellationToken cancellationToken)
        {
            await _notifyService.SendMessageNotify("Thông báo", "Lấy danh sách thành công");
            return await _dataContext.PhieuCapPhepQuangCaos.ProjectTo<PhieuCapPhepQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
