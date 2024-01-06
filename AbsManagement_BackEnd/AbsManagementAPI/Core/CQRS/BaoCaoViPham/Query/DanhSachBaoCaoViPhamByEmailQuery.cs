using AbsManagementAPI.Core.Models.BaoCaoViPham;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.Query
{
    public class DanhSachBaoCaoViPhamByEmailQuery : IRequest<List<BaoCaoViPhamModel>>
    {
        public string Email { get; set; }
    }
}
