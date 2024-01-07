using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models;
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
        /// <response code="200">Lấy chi tiết bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ExceptionResponse))]
        [HttpGet("chitiet/{id}")]
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
        /// <param name="addressSearchModel"></param>
        /// <response code="200">Lấy danh sách bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet()]
        public async Task<List<BangQuangCaoModel>> DanhSach([FromQuery] AddressSearchModel addressSearchModel)
        {
            return await _mediator.Send(new DanhSachBangquangCaoQuery()
            {
                addressSearchModel = addressSearchModel
            });
        }

        /// <summary>
        /// Danh sách bảng quảng cáo theo điểm đặt quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy danh sách bảng quảng cáo theo điểm đặt quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("diemdatquangcao/{id}")]
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
        /// <response code="200">Lấy tạo mới bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("taomoi")]
        [Authorize]
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
        /// <response code="200">Lấy cập nhật bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("capnhat/{id}")]
        [Authorize]
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
        /// <response code="200">Lấy xóa bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("xoa/{id}")]
        [Authorize]
        public async Task<string> Xoa(int id)
        {
            return await _mediator.Send(new XoaBangQuangCaoCommand()
            {
                Id = id
            });
        }

        /// <summary>
        /// Gửi duyệt bảng quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy gữi duyệt bảng quảng cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [HttpPost("gui/{id}")]
        [Authorize]
        public async Task<string> GuiDuyet(int id)
        {
            return await _mediator.Send(new GuiBangQuangCaoCommand()
            {
                Id = id
            });
        }
    }
}
