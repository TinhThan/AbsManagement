using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.QueryHandler
{

    public class DanhSachPhieuCapPhepSuaQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachPhieuCapPhepSuaQuangCaoQuery, List<PhieuCapPhepSuaQuangCaoModel>>
    {
        private readonly INotifyService _notifyService;
        public DanhSachPhieuCapPhepSuaQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, INotifyService notifyService) : base(httpContextAccessor, dataContext, mapper)
        {
            _notifyService = notifyService;
        }

        public async Task<List<PhieuCapPhepSuaQuangCaoModel>> Handle(DanhSachPhieuCapPhepSuaQuangCaoQuery request, CancellationToken cancellationToken)
        {
            await _notifyService.SendMessageNotify("Thông báo", "Lấy danh sách thành công");
            return await _dataContext.PhieuCapPhepSuaQuangCaos.ProjectTo<PhieuCapPhepSuaQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
