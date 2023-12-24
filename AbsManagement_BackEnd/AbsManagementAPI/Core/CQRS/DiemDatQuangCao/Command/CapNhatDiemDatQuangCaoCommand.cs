using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command
{
    public class CapNhatDiemDatQuangCaoCommand : IRequest<string>
    {
        public int Id { get; set; }
        public CapNhatDiemDatQuangCaoModel CapNhatDiemDatQuangCaoModel { get; set; }
    }
}
