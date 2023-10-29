using AbsManagementAPI.Core.Models.BangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.Command
{
    public class ThemBangQuangCaoCommand : IRequest<string>
    {
        public ThemBangQuangCaoModel ThemBangQuangCaoModel { get; set; }
    }
}
