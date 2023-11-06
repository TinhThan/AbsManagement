using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.QueryHandler
{
    public class DanhSachBangQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachBangquangCaoQuery, List<BangQuangCaoModel>>
    {
        public DanhSachBangQuangCaoQueryHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<List<BangQuangCaoModel>> Handle(DanhSachBangquangCaoQuery request, CancellationToken cancellationToken)
        => await _dataContext.BangQuangCaos.ProjectTo<BangQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
