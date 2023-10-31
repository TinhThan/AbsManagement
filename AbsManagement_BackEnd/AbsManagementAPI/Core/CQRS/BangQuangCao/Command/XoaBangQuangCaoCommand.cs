using AbsManagementAPI.Core.Models.BangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.Command
{
    public class XoaBangQuangCaoCommand : IRequest<string>
    {
        public XoaBangQuangCaoModel XoaBangQuangCaoModel { get; set; }
    }
}
