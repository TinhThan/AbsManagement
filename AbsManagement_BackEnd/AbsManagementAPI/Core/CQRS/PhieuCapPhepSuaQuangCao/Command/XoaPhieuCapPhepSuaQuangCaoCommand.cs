using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Command
{
    public class XoaPhieuCapPhepSuaQuangCaoCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
