using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.BaoCaoViPham;
using AbsManagementAPI.Core.Models.CanBo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller Bảng quảng cáo
    /// </summary>
    [ApiController]
    [Route("api/canbo")]
    public class CanBoController : BaseController
    {
        public CanBoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Thêm mới cán bộ
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới cán bộ</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("taomoi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> TaoMoi(ThemMoiCanBoModel model)
        {
            return await _mediator.Send(new ThemMoiCanBoCommand()
            {
                ThemMoiCanBoModel = model
            });
        }


        /// <summary>
        /// Cập nhật cán bộ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">cập nhật cán bộ thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("capnhat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> CapNhat(int id, CapNhatBangQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatBangQuangCaoCommand()
            {
                CapNhatBangQuangCaoModel = model,
                Id = id
            });


        }


        /// <summary>
        /// Xóa Cán Bộ
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Xóa cán bộ thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("xoa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> Xoa(XoaCanBoModel model)
        {
            return await _mediator.Send(new XoaCanBoCommand()
            {
                XoaCanBoModel = model
            });
        }
    }
}
