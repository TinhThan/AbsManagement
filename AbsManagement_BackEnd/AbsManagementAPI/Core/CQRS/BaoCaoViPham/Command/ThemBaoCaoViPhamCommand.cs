using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.BaoCaoViPham;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command
{
    public class ThemBaoCaoViPhamCommand : IRequest<string>
    {
        public ThemBaoCaoViPhamModel ThemBaoCaoViPhamModel { get; set; }
    }
}
