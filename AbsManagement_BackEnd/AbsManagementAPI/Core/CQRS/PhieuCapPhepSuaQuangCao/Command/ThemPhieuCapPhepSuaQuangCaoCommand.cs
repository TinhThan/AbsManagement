using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Command
{
    public class ThemPhieuCapPhepSuaQuangCaoCommand : IRequest<string>
    {
        public ThemPhieuCapPhepSuaQuangCaoModel ThemPhieuCapPhepSuaQuangCaoModel { get; set; }
    }
}
