using AbsManagementAPI.Core.Models.BangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.Command
{
    public class CapNhatBangQuangCaoCommand : IRequest<string>
    {
        public int Id { get; set; }
        public CapNhatBangQuangCaoModel CapNhatBangQuangCaoModel { get; set; }
    }
}
