using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Query
{
    public class ChiTietLoaiBangQuangCaoQuery : IRequest<LoaiBangQuangCaoModel>
    {
        public int Id { get; set; }
    }
}
