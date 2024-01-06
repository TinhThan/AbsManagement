using AbsManagementAPI.Core.Models;
using AbsManagementAPI.Core.Models.BangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.Query
{
    public class DanhSachBangquangCaoQuery : IRequest<List<BangQuangCaoModel>>
    {
        public AddressSearchModel addressSearchModel { get; set; }
    }
}
