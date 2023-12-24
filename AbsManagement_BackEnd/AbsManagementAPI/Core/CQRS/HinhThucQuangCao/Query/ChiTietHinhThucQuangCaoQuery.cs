using AbsManagementAPI.Core.Models.HinhThucQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Query
{
    public class ChiTietHinhThucQuangCaoQuery : IRequest<HinhThucQuangCaoModel>
    {
        public int Id { get; set; }
    }
}
