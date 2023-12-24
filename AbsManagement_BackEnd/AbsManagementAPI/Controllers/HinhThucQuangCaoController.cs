using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.HinhThucBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.HinhThucQuangCao;
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
    //[Authorize]
    [Route("api/hinhthucquangcao")]
    public class HinhThucQuangCaoController : BaseController
    {
        public HinhThucQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiếthình thức quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Chi tiếthình thức quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("chitiet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HinhThucQuangCaoModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<HinhThucQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietHinhThucQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sáchhình thức quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy danh sáchhình thức quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<HinhThucQuangCaoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<List<HinhThucQuangCaoModel>> DanhSach(int id)
        {
            return await _mediator.Send(new DanhSachHinhThucQuangCaoQuery());
        }

        /// <summary>
        /// Thêm mới hình thức quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mớihình thức quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("taomoi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
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
        /// <response code="200">cập nhật hình thức quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("capnhat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> CapNhat(int id, CapNhatHinhThucQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatHinhThucQuangCaoCommand()
            {
                CapNhatHinhThucQuangCaoModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóahình thức quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Xóahình thức quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("xoa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> Xoa(XoaHinhThucQuangCaoModel model)
        {
            return await _mediator.Send(new XoaHinhThucQuangCaoCommand()
            {
                XoaHinhThucQuangCaoModel = model
            });
        }
    }
}
