using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.HinhThucQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.HinhThucBangQuangCao.QueryHandler
{
    public class ChiTietHinhThucQuangCaoQueryHandler : BaseHandler, IRequestHandler<ChiTietHinhThucQuangCaoQuery, HinhThucQuangCaoModel>
    {
        public ChiTietHinhThucQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<HinhThucQuangCaoModel> Handle(ChiTietHinhThucQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.HinhThucQuangCaos.Include(t => t.DiemDatQuangCaos)
                                                    .AsSplitQuery()
                                                    .ProjectTo<HinhThucQuangCaoModel>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        }
    }
}
