using AbsManagementAPI.Core.Models.BaoCaoViPham;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.CanBo.Command
{
    public class CapNhatCanBoCommand : IRequest<string>
    {
        public int Id { get; set; }
        public CapNhatBaoCaoViPhamModel CapNhatBaoCaoViPhamModel { get; set; }
    }
}
