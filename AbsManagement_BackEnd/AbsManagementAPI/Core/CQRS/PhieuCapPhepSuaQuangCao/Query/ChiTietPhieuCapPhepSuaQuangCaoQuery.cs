using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Query
{

    public class ChiTietPhieuCapPhepSuaQuangCaoQuery : IRequest<PhieuCapPhepSuaQuangCaoModel>
    {
        public int Id { get; set; }
    }
}
