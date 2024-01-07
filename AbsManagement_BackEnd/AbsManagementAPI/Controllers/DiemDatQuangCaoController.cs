using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.DiemDatBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller điểm đặt quảng cáo
    /// </summary>
    [ApiController]
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
        /// <response code="200">Lấy chi tiết điểm đặt quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("chitiet/{id}")]
        public async Task<DiemDatQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietDiemDatQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sách điểm đặt quảng cáo
        /// </summary>
        /// <param name="addressSearchModel"></param>
        /// <response code="200">Lấy Danh sách điểm đặt quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet()]
        public async Task<List<DiemDatQuangCaoModel>> DanhSach([FromQuery]AddressSearchModel addressSearchModel)
        {
            return await _mediator.Send(new DanhSachDiemDatQuangCaoQuery()
            {
                AddressSearchModel = addressSearchModel
            });
        }

        /// <summary>
        /// Thêm mới điểm đặt quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Lấy Thêm mới điểm đặt quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("taomoi")]
        [Authorize]
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
        /// <response code="200">Lấy Cập nhật điểm đặt quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("capnhat/{id}")]
        [Authorize]
        public async Task<string> CapNhat(int id, CapNhatDiemDatQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatDiemDatQuangCaoCommand()
            {
                CapNhatDiemDatQuangCaoModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóa điểm đặt quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Lấy Xóa điểm đặt quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("xoa")]
        public async Task<string> Xoa(XoaDiemDatQuangCaoModel model)
        {
            return await _mediator.Send(new XoaDiemDatQuangCaoCommand()
            {
                XoaDiemDatQuangCaoModel = model
            });
        }
    }
}
