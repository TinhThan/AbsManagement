using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.QueryHandler
{
    public class DanhSachBangQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachBangquangCaoQuery, List<BangQuangCaoModel>>
    {
        private readonly INotifyService _notifyService;
        public DanhSachBangQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, INotifyService notifyService) : base(httpContextAccessor, dataContext, mapper)
        {
            _notifyService = notifyService;
        }

        public async Task<List<BangQuangCaoModel>> Handle(DanhSachBangquangCaoQuery request, CancellationToken cancellationToken)
        {
            //await _notifyService.SendMessageNotify("Thông báo", "Lấy danh sách thành công");
            return await _dataContext.BangQuangCaos.Include(t=>t.DiemDatQuangCao).Where(t => (string.IsNullOrEmpty(request.addressSearchModel.Quan) || t.DiemDatQuangCao.Quan == request.addressSearchModel.Quan) &&
                                (string.IsNullOrEmpty(request.addressSearchModel.Phuong) || t.DiemDatQuangCao.Phuong == request.addressSearchModel.Phuong)).ProjectTo<BangQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
