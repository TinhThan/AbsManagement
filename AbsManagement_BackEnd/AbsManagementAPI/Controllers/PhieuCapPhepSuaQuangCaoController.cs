using AbsManagementAPI.Core.Authentication;
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
    [Authorize]
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
        /// <response code="200">Lấy Chi tiết Phiếu cấp phép sửa Quảng Cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("chitiet/bangquangcao/{id}")]
        public async Task<PhieuCapPhepSuaBangQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietPhieuCapPhepSuaBangDatQuangCaoQuery()
            {
                Id = id
            });
        }


        /// <summary>
        /// Chi tiết Phiếu cấp phép sửa Quảng Cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy Chi tiết Phiếu cấp phép sửa Quảng Cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("chitiet/diemdatquangcao/{id}")]
        public async Task<PhieuCapPhepSuaDiemDatQuangCaoModel> ChiTietDiemDat(int id)
        {
            return await _mediator.Send(new ChiTietPhieuCapPhepSuaDiemDatQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sách Phiếu cấp phép sửa điểm đặt Quảng Cáo
        /// </summary>
        /// <response code="200">Lấy Danh sách Phiếu cấp phép sửa điểm đặt Quảng Cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("danhsach/diemdatquangcao")]
        public async Task<List<PhieuCapPhepSuaDiemDatQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachPhieuCapPhepDiemDatQuangCaoQuery());
        }

        /// <summary>
        /// Danh sách Phiếu cấp phép sửa bảng Quảng Cáo
        /// </summary>
        /// <response code="200">Lấy Danh sách Phiếu cấp phép sửa bảng Quảng Cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("danhsach/bangquangcao")]
        public async Task<List<PhieuCapPhepSuaBangQuangCaoModel>> DanhSachBQC()
        {
            return await _mediator.Send(new DanhSachPhieuCapPhepSuaBangQuangCaoQuery());
        }


        /// <summary>
        /// Thêm mới Phiếu cấp phép sửa Quảng Cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Lấy Thêm mới Phiếu cấp phép sửa Quảng Cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [Authorize]
        [HttpPost("taomoi")]
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
        /// <response code="200">Lấy Cập nhật Phiếu cấp phép sửa Quảng Cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [Authorize]
        [HttpPost("capnhat/{id}")]
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
        /// <response code="200">Lấy Xóa Phiếu cấp phép sửa Quảng Cáo thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("xoa/{id}")]
        public async Task<string> Xoa(int id)
        {
            return await _mediator.Send(new XoaPhieuCapPhepSuaQuangCaoCommand()
            {
                Id = id
            });
        }
    }
}
