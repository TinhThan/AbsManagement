using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller PhieuCapPhep quảng cáo
    /// </summary>
    [ApiController]
    //[Authorize]
    [Route("api/phieucapphepquangcao")]
    public class PhieiCapPhepQuangCaoController : BaseController
    {
        public PhieiCapPhepQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiết PhieuCapPhep quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Chi tiết PhieuCapPhep quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("chitiet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhieuCapPhepQuangCaoModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<PhieuCapPhepQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietPhieuCapPhepQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sách PhieuCapPhep quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy danh sách PhieuCapPhep quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PhieuCapPhepQuangCaoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<List<PhieuCapPhepQuangCaoModel>> DanhSach(int id)
        {
            return await _mediator.Send(new DanhSachPhieuCapPhepQuangCaoQuery());
        }

        /// <summary>
        /// Thêm mới PhieuCapPhep quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới PhieuCapPhep quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("taomoi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> TaoMoi(ThemPhieuCapPhepQuangCaoModel model)
        {
            return await _mediator.Send(new ThemPhieuCapPhepQuangCaoCommand()
            {
                ThemPhieuCapPhepQuangCaoModel = model
            });
        }

        /// <summary>
        /// Cập nhật PhieuCapPhep quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới PhieuCapPhep quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("capnhat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> CapNhat(int id, CapNhatPhieuCapPhepQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatPhieuCapPhepQuangCaoCommand()
            {
                CapNhatPhieuCapPhepQuangCaoModel = model,
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
        public async Task<string> Xoa(XoaPhieuCapPhepQuangCaoModel model)
        {
            return await _mediator.Send(new XoaPhieuCapPhepQuangCaoCommand()
            {
                XoaPhieuCapPhepQuangCao = model
            });
        }

        /// <summary>
        /// Xóađiểm đặt quảng cáo
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Xóađiểm đặt quảng cáo thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("duyet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> Duyet(XoaPhieuCapPhepQuangCaoModel model)
        {
            return await _mediator.Send(new DuyetPhieuCapPhepQuangCaoCommand()
            {
                DuyetPhieuCapPhepQuangCao = model
            });
        }
    }
}
