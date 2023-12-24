using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.QueryHandler
{
    public class DanhSachDiemDatQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachDiemDatQuangCaoQuery, List<DiemDatQuangCaoModel>>
    {
        private readonly INotifyService _notifyService;
        public DanhSachDiemDatQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, INotifyService notifyService) : base(httpContextAccessor, dataContext, mapper)
        {
            _notifyService = notifyService;
        }

        public async Task<List<DiemDatQuangCaoModel>> Handle(DanhSachDiemDatQuangCaoQuery request, CancellationToken cancellationToken)
        {
            await _notifyService.SendMessageNotify("Thông báo", "Lấy danh sách thành công");
            return await _dataContext.DiemDatQuangCaos.ProjectTo<DiemDatQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
