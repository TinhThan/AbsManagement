using AbsManagementAPI.Core.Models.CanBo;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.CanBo.Command
{
    public class TaoCanBoCommand : IRequest<string>
    {
        public TaoCanBoModel TaoCanBoModel { get; set; }
    }
}
