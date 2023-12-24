using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.DiemDatBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller điểm đặt quảng cáo
    /// </summary>
    [ApiController]
    //[Authorize]
    [Route("api/diemdatquangcao")]
    public class DiemDatQuangCaoController : BaseController
    {
        public DiemDatQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiếtđiểm đặt quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Chi tiếtđiểm đặt quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("chitiet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DiemDatQuangCaoModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<DiemDatQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietDiemDatQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sáchđiểm đặt quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy danh sáchđiểm đặt quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DiemDatQuangCaoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<List<DiemDatQuangCaoModel>> DanhSach(int id)
        {
            return await _mediator.Send(new DanhSachDiemDatQuangCaoQuery());
        }

        /// <summary>
        /// Thêm mới điểm đặt quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mớiđiểm đặt quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("taomoi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> TaoMoi(ThemDiemDatQuangCaoModel model)
        {
            return await _mediator.Send(new ThemDiemDatQuangCaoCommand()
            {
                ThemDiemDatQuangCaoModel = model
            });
        }

        /// <summary>
        /// Cập nhật điểm đặt quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">cập nhật điểm đặt quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("capnhat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> CapNhat(int id, CapNhatDiemDatQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatDiemDatQuangCaoCommand()
            {
                CapNhatDiemDatQuangCaoModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóađiểm đặt quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Xóađiểm đặt quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("xoa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> Xoa(XoaDiemDatQuangCaoModel model)
        {
            return await _mediator.Send(new XoaDiemDatQuangCaoCommand()
            {
                XoaDiemDatQuangCaoModel = model
            });
        }
    }
}
