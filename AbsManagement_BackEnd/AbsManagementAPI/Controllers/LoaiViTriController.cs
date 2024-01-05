using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Command;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Core.Models.LoaiViTri;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller Bảng quảng cáo
    /// </summary>
    [ApiController]
    [Route("api/loaivitri")]
    public class LoaiViTriController : BaseController
    {
        public LoaiViTriController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiết loại vị trí
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("chitiet/{id}")]
        public async Task<LoaiViTriModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietLoaiViTriQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sách loại vị trí
        /// </summary>
        [HttpGet()]
        public async Task<List<LoaiViTriModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachLoaiViTriQuery());
        }

        /// <summary>
        /// Thêm mới loại vị trí
        /// </summary>
        /// <param name="model"></param>
        [HttpPost()]
        [Authorize]
        public async Task<string> TaoMoi(ThemLoaiViTriModel model)
        {
            return await _mediator.Send(new ThemLoaiViTriCommand()
            {
                ThemLoaiViTriModel = model
            });
        }

        /// <summary>
        /// Cập nhật loại vị trí
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [HttpPost("{id}")]
        [Authorize]
        public async Task<string> CapNhat(int id, CapNhatLoaiViTriModel model)
        {
            return await _mediator.Send(new CapNhatLoaiViTriCommand()
            {
                CapNhatLoaiViTriModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóa loại vị trí
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("xoa")]
        [Authorize]
        public async Task<string> Xoa(XoaLoaiViTriModel model)
        {
            return await _mediator.Send(new XoaLoaiViTriCommand()
            {
                XoaLoaiViTriModel = model
            });
        }
    }
}
