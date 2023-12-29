using AbsManagementAPI.Core.Models;
using AbsManagementAPI.Core.Models.BaoCaoViPham;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.Query
{
    public class DanhSachBaoCaoViPhamQuery : IRequest<List<BaoCaoViPhamModel>>
    {
        public AddressSearchModel addressSearchModel { get; set; }
    }
}
