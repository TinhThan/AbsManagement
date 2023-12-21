using AbsManagementAPI.Core.Models.BaoCaoViPham;
using AbsManagementAPI.Core.Models.LoaiViTri;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiViTri.Query
{
    public class ChiTietLoaiViTriQuery : IRequest<LoaiViTriModel>
    {
        public int Id { get; set; }
    }
}
