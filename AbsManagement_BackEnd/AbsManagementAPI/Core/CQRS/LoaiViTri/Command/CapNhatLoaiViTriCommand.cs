using AbsManagementAPI.Core.Models.LoaiViTri;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiViTri.Command
{
    public class CapNhatLoaiViTriCommand : IRequest<string>
    {
        public int Id { get; set; }
        public CapNhatLoaiViTriModel CapNhatLoaiViTriModel { get; set; }
    }
}
