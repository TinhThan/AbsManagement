using AbsManagementAPI.Core.CQRS.CanBo.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.CanBo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.CanBo.QueryHandler
{
    public class DanhSachCanBoQueryHandler : BaseHandler, IRequestHandler<DanhSachCanBoQuery, List<CanBoModel>>
    {
        public DanhSachCanBoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<List<CanBoModel>> Handle(DanhSachCanBoQuery request, CancellationToken cancellationToken)
        => await _dataContext.CanBos.OrderBy(t => t.NgayCapNhat).ProjectTo<CanBoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
