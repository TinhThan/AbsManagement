using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.QueryHandler
{
    public class ChiTietBangQuangCaoQueryHandler : BaseHandler, IRequestHandler<ChiTietBangQuangCaoQuery, BangQuangCaoModel>
    {
        public ChiTietBangQuangCaoQueryHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<BangQuangCaoModel> Handle(ChiTietBangQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.BangQuangCaos.Include(t => t.DiemDatQuangCao).Include(t => t.LoaiBangQuangCao)
                                                    .AsSplitQuery()
                                                    .ProjectTo<BangQuangCaoModel>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        }
    }
}
