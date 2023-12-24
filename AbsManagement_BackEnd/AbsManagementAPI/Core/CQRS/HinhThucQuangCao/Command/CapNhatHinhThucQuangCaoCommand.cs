using AbsManagementAPI.Core.Models.HinhThucQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command
{
    public class CapNhatHinhThucQuangCaoCommand : IRequest<string>
    {
        public int Id { get; set; }
        public CapNhatHinhThucQuangCaoModel CapNhatHinhThucQuangCaoModel { get; set; }
    }
}
