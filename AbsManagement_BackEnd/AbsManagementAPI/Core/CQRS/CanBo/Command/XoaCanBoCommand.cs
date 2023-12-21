using AbsManagementAPI.Core.Models.BaoCaoViPham;
using AbsManagementAPI.Core.Models.CanBo;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.CanBo.Command
{
    public class XoaCanBoCommand : IRequest<string>
    {
        public XoaCanBoModel XoaCanBoModel { get; set; }

    }
}
