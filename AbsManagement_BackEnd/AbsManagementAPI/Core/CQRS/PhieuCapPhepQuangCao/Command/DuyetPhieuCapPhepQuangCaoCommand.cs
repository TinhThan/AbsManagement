using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command
{
    public class DuyetPhieuCapPhepQuangCaoCommand : IRequest<string>
    {
        public XoaPhieuCapPhepQuangCaoModel DuyetPhieuCapPhepQuangCao { get; set; }
    }
}
