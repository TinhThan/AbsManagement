using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.BaoCaoViPham;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller Báo cáo vi phạm
    /// </summary>
    [ApiController]
    [Route("api/baocaovipham")]
    public class BaoCaoViPhamController : BaseController
    {

        public BaoCaoViPhamController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// chi tiết báo cáo vi phạm
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy chi tiết báo cáo vi phạm thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("{id}")]
        public async Task<BaoCaoViPhamModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietBaoCaoViPhamQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// danh sách báo cáo vi phạm
        /// </summary>
        /// <param name="addressSearchModel"></param>
        /// <response code="200">Lấy danh sách báo cáo vi phạm thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet()]
        public async Task<List<BaoCaoViPhamModel>> DanhSach([FromQuery] AddressSearchModel addressSearchModel)
        {
            return await _mediator.Send(new DanhSachBaoCaoViPhamQuery()
            {
                addressSearchModel = addressSearchModel
            });
        }

        /// <summary>
        /// danh sách báo cáo vi phạm theo email
        /// </summary>
        /// <param name="email"></param>        
        /// <response code="200">Lấy danh sách báo cáo vi phạm theo email thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpGet("email/{email}")]
        public async Task<List<BaoCaoViPhamModel>> DanhSachByEmail(string email)
        {
            return await _mediator.Send(new DanhSachBaoCaoViPhamByEmailQuery()
            {
                Email = email
            });
        }

        /// <summary>
        /// Thêm mới báo cáo vi phạm
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới báo cáo vi phạm thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("taomoi")]
        public async Task<string> TaoMoi(ThemBaoCaoViPhamModel model)
        {
            return await _mediator.Send(new ThemBaoCaoViPhamCommand()
            {
                ThemBaoCaoViPhamModel = model
            });
        }

        /// <summary>
        /// Cập nhật báo cáo vi phạm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">Cập nhật báo cáo vi phạm thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("capnhat/{id}")]
        [Authorize]
        public async Task<string> CapNhat(int id, CapNhatBaoCaoViPhamModel model)
        {
            return await _mediator.Send(new CapNhatBaoCaoViPhamCommand()
            {
                CapNhatBaoCaoViPhamModel = model,
                Id = id
            });
        }


        /// <summary>
        /// Xóa báo cáo vi phạm
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Xóa báo cáo vi phạm thành công</response>
        /// <response code="400">1 vài thông tin truyền vào không hợp lệ</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [HttpPost("xoa")]
        public async Task<string> Xoa(XoaBaoCaoViPhamModel model)
        {
            return await _mediator.Send(new XoaBaoCaoViPhamCommand()
            {
                XoaBaoCaoViPhamModel = model
            });
        }
    }
}