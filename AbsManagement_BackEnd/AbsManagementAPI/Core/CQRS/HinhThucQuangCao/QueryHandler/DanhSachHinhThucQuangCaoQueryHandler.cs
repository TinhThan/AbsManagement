using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.HinhThucQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.HinhThucQuangCao.QueryHandler
{
    public class DanhSachHinhThucQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachHinhThucQuangCaoQuery, List<HinhThucQuangCaoModel>>
    {
        private readonly INotifyService _notifyService;
        public DanhSachHinhThucQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, INotifyService notifyService) : base(httpContextAccessor, dataContext, mapper)
        {
            _notifyService = notifyService;
        }

        public async Task<List<HinhThucQuangCaoModel>> Handle(DanhSachHinhThucQuangCaoQuery request, CancellationToken cancellationToken)
        {
            await _notifyService.SendMessageNotify("Thông báo", "Lấy danh sách thành công");
            return await _dataContext.HinhThucQuangCaos.ProjectTo<HinhThucQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
