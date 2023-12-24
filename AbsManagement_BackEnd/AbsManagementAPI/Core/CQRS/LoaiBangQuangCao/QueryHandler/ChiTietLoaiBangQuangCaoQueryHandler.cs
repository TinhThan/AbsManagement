using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.LoaiBangBangQuangCao.QueryHandler
{
    public class ChiTietLoaiBangQuangCaoQueryHandler : BaseHandler, IRequestHandler<ChiTietLoaiBangQuangCaoQuery, LoaiBangQuangCaoModel>
    {
        public ChiTietLoaiBangQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<LoaiBangQuangCaoModel> Handle(ChiTietLoaiBangQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.LoaiBangQuangCaos.Include(t => t.BangQuangCaos)
                                                    .AsSplitQuery()
                                                    .ProjectTo<LoaiBangQuangCaoModel>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        }
    }
}
