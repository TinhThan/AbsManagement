using AbsManagementAPI.Core.Models.BangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.Command
{
    public class GuiBangQuangCaoCommand : IRequest<string>
    {
        public int Id { get; set; }
        public GuiBangQuangCaoModel GuiBangQuangCaoModel { get; set; }
    }
}
