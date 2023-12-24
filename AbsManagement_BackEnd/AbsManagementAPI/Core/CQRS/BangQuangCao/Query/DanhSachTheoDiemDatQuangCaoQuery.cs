using AbsManagementAPI.Core.Models.BangQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.Query
{
    public class DanhSachTheoDiemDatQuangCaoQuery : IRequest<List<BangQuangCaoModel>>
    {
        public int IdDiemDatQuangCao { get; set; }
    }
}
