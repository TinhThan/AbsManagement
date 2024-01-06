using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Query
{

    public class ChiTietPhieuCapPhepSuaDiemDatQuangCaoQuery : IRequest<PhieuCapPhepSuaDiemDatQuangCaoModel>
    {
        public int Id { get; set; }
    }
}
