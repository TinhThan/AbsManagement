using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.QueryHandler
{
    public class ChiTietPhieuCapPhepSuaDiemDatQuangCaoQueryHandler : BaseHandler, IRequestHandler<ChiTietPhieuCapPhepSuaDiemDatQuangCaoQuery, PhieuCapPhepSuaDiemDatQuangCaoModel>
    {
        public ChiTietPhieuCapPhepSuaDiemDatQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<PhieuCapPhepSuaDiemDatQuangCaoModel> Handle(ChiTietPhieuCapPhepSuaDiemDatQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.PhieuCapPhepSuaQuangCaos
                                                    .AsSplitQuery()
                                                    .ProjectTo<PhieuCapPhepSuaDiemDatQuangCaoModel>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        }
    }
}
