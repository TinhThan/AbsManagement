using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Command;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.LoaiViTri;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller Bảng quảng cáo
    /// </summary>
    [ApiController]
    [Route("api/loaivitri")]
    public class LoaiViTriController : BaseController
    {
        public LoaiViTriController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiết loại vị trí
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy Chi tiết loại vị trí thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("chitiet/{id}")]
        public async Task<LoaiViTriModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietLoaiViTriQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sách loại vị trí
        /// </summary>
        /// <response code="200">Lấy Danh sách loại vị trí thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet()]
        public async Task<List<LoaiViTriModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachLoaiViTriQuery());
        }

        /// <summary>
        /// Thêm mới loại vị trí
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Lấy Thêm mới loại vị trí thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost()]
        [Authorize]
        public async Task<string> TaoMoi(ThemLoaiViTriModel model)
        {
            return await _mediator.Send(new ThemLoaiViTriCommand()
            {
                ThemLoaiViTriModel = model
            });
        }

        /// <summary>
        /// Cập nhật loại vị trí
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">Lấy Cập nhật loại vị trí thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("{id}")]
        [Authorize]
        public async Task<string> CapNhat(int id, CapNhatLoaiViTriModel model)
        {
            return await _mediator.Send(new CapNhatLoaiViTriCommand()
            {
                CapNhatLoaiViTriModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóa loại vị trí
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Lấy Xóa loại vị trí thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("xoa")]
        [Authorize]
        public async Task<string> Xoa(XoaLoaiViTriModel model)
        {
            return await _mediator.Send(new XoaLoaiViTriCommand()
            {
                XoaLoaiViTriModel = model
            });
        }
    }
}
