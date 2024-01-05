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
            try
            {
                return await _dataContext.DiemDatQuangCaos.Where(t =>
                                    (string.IsNullOrEmpty(request.AddressSearchModel.Quan) || t.Quan == request.AddressSearchModel.Quan) &&
                                    (string.IsNullOrEmpty(request.AddressSearchModel.Phuong) || t.Phuong == request.AddressSearchModel.Phuong))
                                    .ProjectTo<DiemDatQuangCaoModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                var x = ex.Message;
            }
            return null;
        }
    }
}
