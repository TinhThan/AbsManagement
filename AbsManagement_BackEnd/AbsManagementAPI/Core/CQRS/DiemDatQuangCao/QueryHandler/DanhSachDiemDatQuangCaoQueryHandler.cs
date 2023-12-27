using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.QueryHandler
{
    public class DanhSachDiemDatQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachDiemDatQuangCaoQuery, List<DiemDatQuangCaoModel>>
    {
        public DanhSachDiemDatQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<List<DiemDatQuangCaoModel>> Handle(DanhSachDiemDatQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.DiemDatQuangCaos.ProjectTo<DiemDatQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
