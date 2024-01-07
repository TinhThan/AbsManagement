using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.BaoCaoViPham;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.QueryHandler
{
    public class DanhSachBaoCaoViPhamByEmailQueryHandler : BaseHandler, IRequestHandler<DanhSachBaoCaoViPhamByEmailQuery, List<BaoCaoViPhamModel>>
    {
        public DanhSachBaoCaoViPhamByEmailQueryHandler(IHttpContextAccessor context, DataContext dataContext, IMapper mapper) : base(context, dataContext, mapper)
        {
        }

        public async Task<List<BaoCaoViPhamModel>> Handle(DanhSachBaoCaoViPhamByEmailQuery request, CancellationToken cancellationToken)
        => await _dataContext.BaoCaoViPhams.Include(t => t.CanBoXuLy).Where(t => t.Email == request.Email)
            .ProjectTo<BaoCaoViPhamModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
