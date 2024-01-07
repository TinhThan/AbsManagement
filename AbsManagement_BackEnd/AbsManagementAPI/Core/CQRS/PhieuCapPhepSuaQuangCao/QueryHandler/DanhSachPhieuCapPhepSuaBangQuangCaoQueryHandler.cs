using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.QueryHandler
{
    public class DanhSachPhieuCapPhepSuaBangQuangCaoQueryHandler : BaseHandler, IRequestHandler<DanhSachPhieuCapPhepSuaBangQuangCaoQuery, List<PhieuCapPhepSuaBangQuangCaoModel>>
    {
        public DanhSachPhieuCapPhepSuaBangQuangCaoQueryHandler(IHttpContextAccessor context, DataContext dataContext, IMapper mapper) : base(context, dataContext, mapper)
        {
        }

        public async Task<List<PhieuCapPhepSuaBangQuangCaoModel>> Handle(DanhSachPhieuCapPhepSuaBangQuangCaoQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.PhieuCapPhepSuaQuangCaos.Where(t => (authInfo.Role == "CanBoSo" || t.IdCanBoGui == authInfo.Id) && t.IdBangQuangCao != null).ProjectTo<PhieuCapPhepSuaBangQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
