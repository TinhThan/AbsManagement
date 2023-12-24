using AbsManagementAPI.Core.Models.HinhThucQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command
{
    public class XoaHinhThucQuangCaoCommand : IRequest<string>
    {
        public XoaHinhThucQuangCaoModel XoaHinhThucQuangCaoModel { get; set; }
    }
}
