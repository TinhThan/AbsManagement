using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.QueryHandler
{
    public class DanhSachTheoDiemDatQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachTheoDiemDatQuangCaoQuery, List<BangQuangCaoModel>>
    {
        public DanhSachTheoDiemDatQuangCaoQueryHandler(IHttpContextAccessor context, DataContext dataContext, IMapper mapper) : base(context, dataContext, mapper)
        {
        }

        public async Task<List<BangQuangCaoModel>> Handle(DanhSachTheoDiemDatQuangCaoQuery request, CancellationToken cancellationToken)
        => await _dataContext.BangQuangCaos.Where(t => t.IdDiemDatQuangCao == request.IdDiemDatQuangCao).ProjectTo<BangQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
