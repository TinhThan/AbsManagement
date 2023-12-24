using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Query
{
    public class ChiTietDiemDatQuangCaoQuery : IRequest<DiemDatQuangCaoModel>
    {
        public int Id { get; set; }
    }
}
