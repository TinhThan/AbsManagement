using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.HubSignalR;
using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.QueryHandler
{
    public class DanhSachPhieuCapPhepQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachPhieuCapPhepQuangCaoQuery, List<PhieuCapPhepQuangCaoModel>>
    {
        public DanhSachPhieuCapPhepQuangCaoQueryHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<List<PhieuCapPhepQuangCaoModel>> Handle(DanhSachPhieuCapPhepQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.PhieuCapPhepQuangCaos.Include(t => t.BangQuangCao)
                .Include(t => t.CanBoGui)
                .Include(t => t.CanBoDuyet)
                .Include(t => t.BangQuangCao.DiemDatQuangCao)
                .Include(t => t.BangQuangCao.LoaiBangQuangCao)
                .Where(t => authInfo.Role == "CanBoSo" || t.IdCanBoGui == authInfo.Id).ProjectTo<PhieuCapPhepQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
