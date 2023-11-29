using AbsManagementAPI.Core.Models.CanBo;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.CanBo.Query
{
    public class DanhSachCanBoQuery : IRequest<List<CanBoModel>>
    {
    }
}
