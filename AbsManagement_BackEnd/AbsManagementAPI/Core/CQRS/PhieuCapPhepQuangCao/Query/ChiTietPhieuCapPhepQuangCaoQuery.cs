using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Query
{
    public class ChiTietPhieuCapPhepQuangCaoQuery : IRequest<PhieuCapPhepQuangCaoModel>
    {
        public int Id { get; set; }
    }
}
