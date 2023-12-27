using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.LoaiBangBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller loại bảngquảng cáo
    /// </summary>
    [ApiController]
    [Route("api/loaibangquangcao")]
    public class LoaiBangQuangCaoController : BaseController
    {
        public LoaiBangQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiếtloại bảngquảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Chi tiếtloại bảngquảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("chitiet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoaiBangQuangCaoModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<LoaiBangQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietLoaiBangQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sáchloại bảngquảng cáo
        /// </summary>
        /// <response code="200">Lấy danh sáchloại bảngquảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LoaiBangQuangCaoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<List<LoaiBangQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachLoaiBangQuangCaoQuery());
        }

        /// <summary>
        /// Thêm mới loại bảngquảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mớiloại bảngquảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost()]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> TaoMoi(ThemLoaiBangQuangCaoModel model)
        {
            return await _mediator.Send(new ThemLoaiBangQuangCaoCommand()
            {
                ThemLoaiBangQuangCaoModel = model
            });
        }

        /// <summary>
        /// Cập nhật loại bảngquảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">cập nhật loại bảngquảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> CapNhat(int id, CapNhatLoaiBangQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatLoaiBangQuangCaoCommand()
            {
                CapNhatLoaiBangQuangCaoModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóaloại bảngquảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Xóaloại bảngquảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("xoa")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> Xoa(XoaLoaiBangQuangCaoModel model)
        {
            return await _mediator.Send(new XoaLoaiBangQuangCaoCommand()
            {
                XoaLoaiBangQuangCaoModel = model
            });
        }
    }
}
