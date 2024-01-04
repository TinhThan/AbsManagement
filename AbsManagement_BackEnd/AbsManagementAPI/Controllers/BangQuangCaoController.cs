using AbsManagementAPI.Core.Authentication;
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
    //[Authorize]
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
        public async Task<BangQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietBangQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sách bảng quảng cáo
        /// </summary>
        /// <response code="200">Lấy danh sách bảng quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BangQuangCaoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<List<BangQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachBangquangCaoQuery());
        }

        /// <summary>
        /// Danh sách bảng quảng cáo theo điểm đặt quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy danh sách bảng quảng cáo theo điểm đặt quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("diemdatquangcao/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BangQuangCaoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<List<BangQuangCaoModel>> DanhSachDiemDatQuangCao(int id)
        {
            return await _mediator.Send(new DanhSachTheoDiemDatQuangCaoQuery()
            {
                IdDiemDatQuangCao = id
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
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới bảng quảng cáo thành công</response>
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
        /// Xóa bảng quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Xóa bảng quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("xoa/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> Xoa(int id)
        {
            return await _mediator.Send(new XoaBangQuangCaoCommand()
            {
                Id = id
            });
        }
    }
}
