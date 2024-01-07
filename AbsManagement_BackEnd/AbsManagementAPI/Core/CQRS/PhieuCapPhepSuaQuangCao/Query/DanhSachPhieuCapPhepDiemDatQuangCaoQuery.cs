using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Query
{

    public class DanhSachPhieuCapPhepDiemDatQuangCaoQuery : IRequest<List<PhieuCapPhepSuaDiemDatQuangCaoModel>>
    {
    }
}
