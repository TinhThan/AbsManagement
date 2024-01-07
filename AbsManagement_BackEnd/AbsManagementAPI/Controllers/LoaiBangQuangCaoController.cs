using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.LoaiBangBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller loại bảng quảng cáo
    /// </summary>
    [ApiController]
    [Route("api/loaibangquangcao")]
    public class LoaiBangQuangCaoController : BaseController
    {
        public LoaiBangQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiết loại bản gquảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy Chi tiết loại bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("chitiet/{id}")]
        public async Task<LoaiBangQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietLoaiBangQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sách loại bảng quảng cáo
        /// </summary>
        /// <response code="200">Lấy Danh sách loại bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet()]
        public async Task<List<LoaiBangQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachLoaiBangQuangCaoQuery());
        }

        /// <summary>
        /// Thêm mới loại bảng quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Lấy Thêm mới loại bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("taomoi")]
        [Authorize]
        public async Task<string> TaoMoi(ThemLoaiBangQuangCaoModel model)
        {
            return await _mediator.Send(new ThemLoaiBangQuangCaoCommand()
            {
                ThemLoaiBangQuangCaoModel = model
            });
        }

        /// <summary>
        /// Cập nhật loại bảng quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">Lấy Cập nhật loại bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("capnhat/{id}")]
        [Authorize]
        public async Task<string> CapNhat(int id, CapNhatLoaiBangQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatLoaiBangQuangCaoCommand()
            {
                CapNhatLoaiBangQuangCaoModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóa loại bảng quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Lấy Xóa loại bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("xoa")]
        public async Task<string> Xoa(XoaLoaiBangQuangCaoModel model)
        {
            return await _mediator.Send(new XoaLoaiBangQuangCaoCommand()
            {
                XoaLoaiBangQuangCaoModel = model
            });
        }
    }
}
