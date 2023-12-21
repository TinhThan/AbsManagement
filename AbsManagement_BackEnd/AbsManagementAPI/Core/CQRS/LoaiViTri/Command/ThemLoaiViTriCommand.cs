using AbsManagementAPI.Core.Models.LoaiViTri;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiViTri.Command
{
    public class ThemLoaiViTriCommand : IRequest<string>
    {
        public ThemLoaiViTriModel ThemLoaiViTriModel { get; set; }
    }
}
