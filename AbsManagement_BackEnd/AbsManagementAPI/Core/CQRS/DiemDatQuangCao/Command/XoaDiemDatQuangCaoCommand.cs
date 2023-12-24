using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command
{
    public class XoaDiemDatQuangCaoCommand : IRequest<string>
    {
        public XoaDiemDatQuangCaoModel XoaDiemDatQuangCaoModel { get; set; }
    }
}
