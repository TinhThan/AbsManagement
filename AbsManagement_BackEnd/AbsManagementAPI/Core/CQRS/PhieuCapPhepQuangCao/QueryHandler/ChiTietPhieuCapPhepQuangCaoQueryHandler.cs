using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.QueryHandler
{
    public class ChiTietPhieuCapPhepQuangCaoQueryHandler : BaseHandler, IRequestHandler<ChiTietPhieuCapPhepQuangCaoQuery, PhieuCapPhepQuangCaoModel>
    {
        public ChiTietPhieuCapPhepQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<PhieuCapPhepQuangCaoModel> Handle(ChiTietPhieuCapPhepQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.PhieuCapPhepQuangCaos.AsSplitQuery()
                                                    .ProjectTo<PhieuCapPhepQuangCaoModel>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        }
    }
}
