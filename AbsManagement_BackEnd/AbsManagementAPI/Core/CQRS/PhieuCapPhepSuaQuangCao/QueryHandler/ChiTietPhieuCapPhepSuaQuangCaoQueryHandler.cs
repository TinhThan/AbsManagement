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
    public class ChiTietPhieuCapPhepSuaQuangCaoQueryHandler : BaseHandler, IRequestHandler<ChiTietPhieuCapPhepSuaQuangCaoQuery, PhieuCapPhepSuaQuangCaoModel>
    {
        public ChiTietPhieuCapPhepSuaQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<PhieuCapPhepSuaQuangCaoModel> Handle(ChiTietPhieuCapPhepSuaQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.PhieuCapPhepSuaQuangCaos
                                                    .AsSplitQuery()
                                                    .ProjectTo<PhieuCapPhepSuaQuangCaoModel>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        }
    }
}
