using AbsManagementAPI.Core.Authentication;
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
    [Authorize]
    [Route("api/canbo")]
    public class CanBoController : BaseController
    {
        public CanBoController(IMediator mediator) : base(mediator)
        {
        }


        /// <summary>
        /// Danh sách cán bộ
        /// </summary>
        /// <response code="200">Lấy danh sách cán bộ thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("danhsach")]
        public async Task<List<CanBoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachCanBoQuery());
        }

        /// <summary>
        /// Thêm mới cán bộ
        /// </summary>
        /// <param name="taoCanBoModel"></param>
        /// <response code="200">Lấy thêm mới cán bộ thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
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
        /// <response code="200">Lấy cập nhật cán bộ thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
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
        /// <response code="200">Lấy xóa cán bộ thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
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
