using AbsManagementAPI.Core.Models.BaoCaoViPham;
using AbsManagementAPI.Core.Models.CanBo;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.CanBo.Command
{
    public class ThemMoiCanBoCommand : IRequest<string>
    {
        public ThemMoiCanBoModel ThemMoiCanBoModel { get; set; }

    }
}
