using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command
{
    public class XoaLoaiBangQuangCaoCommand : IRequest<string>
    {
        public XoaLoaiBangQuangCaoModel XoaLoaiBangQuangCaoModel { get; set; }
    }
}
