using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command
{
    public class CapNhatPhieuCapPhepQuangCaoCommand : IRequest<string>
    {
        public int Id { get; set; }
        public CapNhatPhieuCapPhepQuangCaoModel CapNhatPhieuCapPhepQuangCaoModel { get; set; }
    }
}
