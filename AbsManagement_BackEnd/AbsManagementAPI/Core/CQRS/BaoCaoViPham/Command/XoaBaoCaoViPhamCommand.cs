using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.BaoCaoViPham;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command
{
    public class XoaBaoCaoViPhamCommand : IRequest<string>
    {
        public XoaBaoCaoViPhamModel XoaBaoCaoViPhamModel { get; set; }
    }
}
