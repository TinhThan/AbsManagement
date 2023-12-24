using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.DiemDatBangQuangCao.QueryHandler
{
    public class ChiTietDiemDatQuangCaoQueryHandler : BaseHandler, IRequestHandler<ChiTietDiemDatQuangCaoQuery, DiemDatQuangCaoModel>
    {
        public ChiTietDiemDatQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<DiemDatQuangCaoModel> Handle(ChiTietDiemDatQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.DiemDatQuangCaos.Include(t => t.BangQuangCaos)
                                                    .AsSplitQuery()
                                                    .ProjectTo<DiemDatQuangCaoModel>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        }
    }
}
