using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.BangQuangCao;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AbsManagementAPI.Controllers
{    
    /// <summary>
    /// Controller Bảng quảng cáo
    /// </summary>
    [ApiController]
    [Route("api/bangquangcao")]
    public class BangQuangCaoController : BaseController
    {
        public BangQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiết bảng quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Chi tiết bảng quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("chitiet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BangQuangCaoModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<BangQuangCaoModel> TaoMoi(int id)
        {
            return await _mediator.Send(new ChiTietBangQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Thêm mới bảng quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới bảng quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("taomoi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> TaoMoi(ThemBangQuangCaoModel model)
        {
            return await _mediator.Send(new ThemBangQuangCaoCommand()
            {
                ThemBangQuangCaoModel = model
            });
        }

        /// <summary>
        /// Cập nhật bảng quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới bảng quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("capnhat")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> CapNhat(CapNhatBangQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatBangQuangCaoCommand()
            {
                CapNhatBangQuangCaoModel = model
            });
        }

    }
}
