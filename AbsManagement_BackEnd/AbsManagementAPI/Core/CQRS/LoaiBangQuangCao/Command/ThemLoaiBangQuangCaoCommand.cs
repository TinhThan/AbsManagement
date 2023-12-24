using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiBangBangQuangCao.Command
{
    public class ThemLoaiBangQuangCaoCommand : IRequest<string>
    {
        public ThemLoaiBangQuangCaoModel ThemLoaiBangQuangCaoModel { get; set; }
    }
}
