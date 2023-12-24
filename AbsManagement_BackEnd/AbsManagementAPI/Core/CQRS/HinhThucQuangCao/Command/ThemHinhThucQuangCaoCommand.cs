using AbsManagementAPI.Core.Models.HinhThucQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.HinhThucBangQuangCao.Command
{
    public class ThemHinhThucQuangCaoCommand : IRequest<string>
    {
        public ThemHinhThucQuangCaoModel ThemHinhThucQuangCaoModel { get; set; }
    }
}
