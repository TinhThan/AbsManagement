using AbsManagementAPI.Core.Models.HinhThucQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Query
{
    public class DanhSachHinhThucQuangCaoQuery : IRequest<List<HinhThucQuangCaoModel>>
    {
    }
}
