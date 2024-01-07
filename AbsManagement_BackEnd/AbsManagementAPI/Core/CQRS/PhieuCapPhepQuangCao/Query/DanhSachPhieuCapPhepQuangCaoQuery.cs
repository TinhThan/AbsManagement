using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Query
{
    public class DanhSachPhieuCapPhepQuangCaoQuery : IRequest<List<PhieuCapPhepQuangCaoModel>>
    {
    }
}
