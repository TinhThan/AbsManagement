using AbsManagementAPI.Core.Models.BaoCaoViPham;
using AbsManagementAPI.Core.Models.CanBo;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.CanBo.Query
{
    public class ChiTietCanBoQuery : IRequest<CanBoModel>
    {
        public int Id { get; set; }
    }
}
