using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.BaoCaoViPham;
using AbsManagementAPI.Core.Models.CanBo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{
    [ApiController]
    [Route("api/canbo")]
    public class CanBoController : BaseController
    {
        public CanBoController(IMediator mediator) : base(mediator)
        {
        }


        /// <summary>
        /// Danh sách cán bộ
        /// </summary>
        [HttpGet("danhsach")]
        public async Task<List<CanBoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachCanBoQuery());
        }

        /// <summary>
        /// Thêm mới cán bộ
        /// </summary>
        /// <param name="taoCanBoModel"></param>
        [HttpPost("taomoi")]
        public async Task<string> TaoMoi(TaoCanBoModel taoCanBoModel)
        {
            return await _mediator.Send(new TaoCanBoCommand()
            {
                TaoCanBoModel = taoCanBoModel
            });
        }

        /// <summary>
        /// Cập nhật cán bộ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="capNhatCanBoModel"></param>
        [HttpPost("capnhat/{id}")]
        public async Task<string> CapNhat(int id, CapNhatCanBoModel capNhatCanBoModel)
        {
            return await _mediator.Send(new CanNhatCanBoCommand()
            {
                CapNhatCanBoModel = capNhatCanBoModel,
                Id = id
            });
        }

        /// <summary>
        /// Xóa cán bộ
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("xoa")]
        public async Task<string> Xoa(XoaCanBoModel model)
        {
            return await _mediator.Send(new XoaCanBoCommand()
            {
                XoaCanBoModel = model
            });
        }
    }
}
