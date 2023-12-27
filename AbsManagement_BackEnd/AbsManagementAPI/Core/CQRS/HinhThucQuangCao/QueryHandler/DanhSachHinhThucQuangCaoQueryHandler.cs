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
        public DanhSachHinhThucQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<List<HinhThucQuangCaoModel>> Handle(DanhSachHinhThucQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.HinhThucQuangCaos.ProjectTo<HinhThucQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
