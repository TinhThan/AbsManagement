using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.DiemDatBangQuangCao.Command
{
    public class ThemDiemDatQuangCaoCommand : IRequest<string>
    {
        public ThemDiemDatQuangCaoModel ThemDiemDatQuangCaoModel { get; set; }
    }
}
