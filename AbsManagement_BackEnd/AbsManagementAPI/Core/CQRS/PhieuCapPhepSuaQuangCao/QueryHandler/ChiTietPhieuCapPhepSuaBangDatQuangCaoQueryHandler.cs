using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.QueryHandler
{
    public class ChiTietPhieuCapPhepSuaBangDatQuangCaoQueryHandler : BaseHandler, IRequestHandler<ChiTietPhieuCapPhepSuaBangDatQuangCaoQuery, PhieuCapPhepSuaBangQuangCaoModel>
    {
        public ChiTietPhieuCapPhepSuaBangDatQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<PhieuCapPhepSuaBangQuangCaoModel> Handle(ChiTietPhieuCapPhepSuaBangDatQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.PhieuCapPhepSuaQuangCaos
                                                    .AsSplitQuery()
                                                    .ProjectTo<PhieuCapPhepSuaBangQuangCaoModel>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        }
    }
}
