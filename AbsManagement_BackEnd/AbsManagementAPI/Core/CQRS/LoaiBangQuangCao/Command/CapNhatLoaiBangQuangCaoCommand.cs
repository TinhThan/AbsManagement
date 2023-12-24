using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command
{
    public class CapNhatLoaiBangQuangCaoCommand : IRequest<string>
    {
        public int Id { get; set; }
        public CapNhatLoaiBangQuangCaoModel CapNhatLoaiBangQuangCaoModel { get; set; }
    }
}
