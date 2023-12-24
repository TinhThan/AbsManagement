using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Query
{
    public class DanhSachLoaiBangQuangCaoQuery : IRequest<List<LoaiBangQuangCaoModel>>
    {
    }
}
