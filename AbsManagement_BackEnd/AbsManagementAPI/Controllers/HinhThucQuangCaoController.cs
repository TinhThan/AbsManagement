using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.HinhThucBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.HinhThucQuangCao;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller hình thức quảng cáo
    /// </summary>
    [ApiController]
    [Route("api/hinhthucquangcao")]
    public class HinhThucQuangCaoController : BaseController
    {
        public HinhThucQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiết hình thức quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy Chi tiết hình thức quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("chitiet/{id}")]
        public async Task<HinhThucQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietHinhThucQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sách hình thức quảng cáo
        /// </summary>
        /// <response code="200">Lấy Danh sách hình thức quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet()]
        public async Task<List<HinhThucQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachHinhThucQuangCaoQuery());
        }

        /// <summary>
        /// Thêm mới hình thức quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Lấy Thêm mới hình thức quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("taomoi")]
        [Authorize]
        public async Task<string> TaoMoi(ThemHinhThucQuangCaoModel model)
        {
            return await _mediator.Send(new ThemHinhThucQuangCaoCommand()
            {
                ThemHinhThucQuangCaoModel = model
            });
        }

        /// <summary>
        /// Cập nhật hình thức quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">Lấy Cập nhật hình thức quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("capnhat/{id}")]
        [Authorize]
        public async Task<string> CapNhat(int id, CapNhatHinhThucQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatHinhThucQuangCaoCommand()
            {
                CapNhatHinhThucQuangCaoModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóa hình thức quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Lấy Xóa hình thức quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("xoa")]
        public async Task<string> Xoa(XoaHinhThucQuangCaoModel model)
        {
            return await _mediator.Send(new XoaHinhThucQuangCaoCommand()
            {
                XoaHinhThucQuangCaoModel = model
            });
        }
    }
}
