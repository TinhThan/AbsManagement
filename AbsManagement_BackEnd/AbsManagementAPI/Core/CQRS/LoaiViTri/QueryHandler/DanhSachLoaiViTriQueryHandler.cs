using AbsManagementAPI.Core.CQRS.LoaiViTri.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.LoaiViTri;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.LoaiViTri.QueryHandler
{
    public class DanhSachLoaiViTriQueryHandler : BaseHandler, IRequestHandler<DanhSachLoaiViTriQuery, List<LoaiViTriModel>>
    {
        public DanhSachLoaiViTriQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<List<LoaiViTriModel>> Handle(DanhSachLoaiViTriQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.LoaiViTris.ProjectTo<LoaiViTriModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
