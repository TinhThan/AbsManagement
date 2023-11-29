using AbsManagementAPI.Core.Models.CanBo;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.CanBo.Command
{
    public class CanNhatCanBoCommand : IRequest<string>
    {
        public CapNhatCanBoModel CapNhatCanBoModel { get; set; }
        public int Id { get; set; }
    }
}
