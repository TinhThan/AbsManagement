using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command
{
    public class ThemPhieuCapPhepQuangCaoCommand : IRequest<string>
    {
        public ThemPhieuCapPhepQuangCaoModel ThemPhieuCapPhepQuangCaoModel { get; set; }
    }
}
