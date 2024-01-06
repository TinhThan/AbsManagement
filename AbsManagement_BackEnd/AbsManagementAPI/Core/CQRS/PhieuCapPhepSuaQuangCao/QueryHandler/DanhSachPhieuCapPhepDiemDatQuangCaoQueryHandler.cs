using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.QueryHandler
{

    public class DanhSachPhieuCapPhepDiemDatQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachPhieuCapPhepDiemDatQuangCaoQuery, List<PhieuCapPhepSuaDiemDatQuangCaoModel>>
    {
        public DanhSachPhieuCapPhepDiemDatQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper, INotifyService notifyService) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<List<PhieuCapPhepSuaDiemDatQuangCaoModel>> Handle(DanhSachPhieuCapPhepDiemDatQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.PhieuCapPhepSuaQuangCaos.Where(t=>t.IdDiemDat != null).ProjectTo<PhieuCapPhepSuaDiemDatQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
