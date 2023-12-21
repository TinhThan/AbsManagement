using AbsManagementAPI.Core.Models.LoaiViTri;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiViTri.Command
{
    public class XoaLoaiViTriCommand : IRequest<string>
    {
        public XoaLoaiViTriModel XoaLoaiViTriModel { get; set; }
    }
}
