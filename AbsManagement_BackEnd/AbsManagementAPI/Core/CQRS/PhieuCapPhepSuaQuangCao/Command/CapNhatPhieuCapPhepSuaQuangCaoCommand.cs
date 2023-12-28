using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Command
{
    public class CapNhatPhieuCapPhepSuaQuangCaoCommand : IRequest<string>
    {
        public int Id { get; set; }
        public CapNhatPhieuCapPhepSuaQuangCaoModel CapNhatPhieuCapPhepSuaQuangCaoModel { get; set; }
    }
}
