using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.QueryHandler
{
    public class DanhSachLoaiBangQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachLoaiBangQuangCaoQuery, List<LoaiBangQuangCaoModel>>
    {
        private readonly INotifyService _notifyService;
        public DanhSachLoaiBangQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, INotifyService notifyService) : base(httpContextAccessor, dataContext, mapper)
        {
            _notifyService = notifyService;
        }

        public async Task<List<LoaiBangQuangCaoModel>> Handle(DanhSachLoaiBangQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.LoaiBangQuangCaos.ProjectTo<LoaiBangQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
