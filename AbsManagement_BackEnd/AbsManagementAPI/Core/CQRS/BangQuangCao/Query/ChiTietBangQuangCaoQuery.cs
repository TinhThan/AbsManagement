using AbsManagementAPI.Core.Models.BangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.Query
{
    public class ChiTietBangQuangCaoQuery : IRequest<BangQuangCaoModel>
    {
        public int Id { get; set; }
    }
}
