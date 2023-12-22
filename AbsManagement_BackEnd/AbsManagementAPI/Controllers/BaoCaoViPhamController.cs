using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
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

        private readonly IWebHostEnvironment _environment;
        private readonly DataContext _dataContext;
        public BaoCaoViPhamController(IMediator mediator, IWebHostEnvironment webHostEnvironment, DataContext dataContext) : base(mediator)
        {
            _environment = webHostEnvironment;
            _dataContext = dataContext;
        }

        ///// <summary>
        ///// Báo cáo vi phạm
        ///// </summary>
        ///// <param name="id"></param>
        ///// <response code="200">Báo cáo vi phạm thành công</response>
        ///// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        ///// <response code="500">Lỗi đến từ server</response>
        //[HttpGet("chitiet/{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaoCaoViPhamModel))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        //public async Task<BaoCaoViPhamModel> ChiTiet(int id)
        //{
        //    return await _mediator.Send(new ChiTietBaoCaoViPhamQuery()
        //    {
        //        Id = id
        //    });
        //}

        /// <summary>
        ///// Báo cáo vi phạm
        ///// </summary>
        ///// <response code="200">Danh sách báo cáo vi phạm</response>
        ///// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        ///// <response code="500">Lỗi đến từ server</response>
        //[HttpGet("danhsach")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaoCaoViPhamModel))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        //public async Task<IEnumerable<BaoCaoViPhamModel>> DanhSach()
        //{
        //    return await _mediator.Send(new DanhSachBaoCaoViPhamQuery());
        //}


        /// <summary>
        /// Thêm mới báo cáo vi phạm
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới báo cáo vi phạm</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("taomoi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
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
        /// <response code="200">cập nhật báo cáo vi phạm thành công</response>
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
        /// Xóa báo cáo vi phạm
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Xóa báo cáo vi phạm thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("xoa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> Xoa(XoaBaoCaoViPhamModel model)
        {
            return await _mediator.Send(new XoaBaoCaoViPhamCommand()
            {
                XoaBaoCaoViPhamModel = model
            });
        }
    }
}