using AbsManagementAPI.Core.Models.BaoCaoViPham;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.Query
{
    public class ChiTietBaoCaoViPhamQuery: IRequest<BaoCaoViPhamModel>
    {
        public int Id { get; set; }
    }
}
