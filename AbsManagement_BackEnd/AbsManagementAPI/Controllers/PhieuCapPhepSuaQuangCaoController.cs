using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Command;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.PhieuCapPhepSuaQuangCao;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{

    /// <summary>
    /// Controller Phiếu cấp phép sửa Quảng Cáo
    /// </summary>
    [ApiController]
    //[Authorize]
    [Route("api/phieucapphepsuaquangcao")]
    public class PhieuCapPhepSuaQuangCaoController : BaseController
    {
        public PhieuCapPhepSuaQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiết Phiếu cấp phép sửa Quảng Cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Chi tiết Phiếu cấp phép sửa Quảng Cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("chitiet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BangQuangCaoModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<PhieuCapPhepSuaQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietPhieuCapPhepSuaQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sách Phiếu cấp phép sửa Quảng Cáo
        /// </summary>
        /// <response code="200">Lấy danh sách Phiếu cấp phép sửa Quảng Cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("danhsach")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BangQuangCaoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<List<PhieuCapPhepSuaQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachPhieuCapPhepSuaQuangCaoQuery());
        }


        /// <summary>
        /// Thêm mới Phiếu cấp phép sửa Quảng Cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới Phiếu cấp phép sửa Quảng Cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("taomoi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> TaoMoi(ThemPhieuCapPhepSuaQuangCaoModel model)
        {
            return await _mediator.Send(new ThemPhieuCapPhepSuaQuangCaoCommand()
            {
                ThemPhieuCapPhepSuaQuangCaoModel = model
            });
        }

        /// <summary>
        /// Cập nhật Phiếu cấp phép sửa Quảng Cáo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới Phiếu cấp phép sửa Quảng Cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("capnhat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> CapNhat(int id, CapNhatPhieuCapPhepSuaQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatPhieuCapPhepSuaQuangCaoCommand()
            {
                TinhTrang = model.TinhTrang,
                Id = id
            });
        }

        /// <summary>
        /// Xóa Phiếu cấp phép sửa Quảng Cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Xóa Phiếu cấp phép sửa Quảng Cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("xoa/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> Xoa(int id)
        {
            return await _mediator.Send(new XoaPhieuCapPhepSuaQuangCaoCommand()
            {
                Id = id
            });
        }
    }
}
