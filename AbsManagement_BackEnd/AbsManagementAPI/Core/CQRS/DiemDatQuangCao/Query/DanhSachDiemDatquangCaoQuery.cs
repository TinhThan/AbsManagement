using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Query
{
    public class DanhSachDiemDatQuangCaoQuery : IRequest<List<DiemDatQuangCaoModel>>
    {
    }
}
