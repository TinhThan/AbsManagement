using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.BaoCaoViPham;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command
{
    public class CapNhatBaoCaoViPhamCommand : IRequest<string>
    {
        public int Id { get; set; }
        public CapNhatBaoCaoViPhamModel CapNhatBaoCaoViPhamModel { get; set; }
    }
}
