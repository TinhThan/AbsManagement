using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.LoaiBangBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.LoaiBangQuangCao;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller loại bảngquảng cáo
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/loaibangquangcao")]
    public class LoaiBangQuangCaoController : BaseController
    {
        public LoaiBangQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiếtloại bảngquảng cáo
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("chitiet/{id}")]
        public async Task<LoaiBangQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietLoaiBangQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sáchloại bảngquảng cáo
        /// </summary>
        [HttpGet()]
        public async Task<List<LoaiBangQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachLoaiBangQuangCaoQuery());
        }

        /// <summary>
        /// Thêm mới loại bảngquảng cáo
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("taomoi")]
        public async Task<string> TaoMoi(ThemLoaiBangQuangCaoModel model)
        {
            return await _mediator.Send(new ThemLoaiBangQuangCaoCommand()
            {
                ThemLoaiBangQuangCaoModel = model
            });
        }

        /// <summary>
        /// Cập nhật loại bảngquảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [HttpPost("capnhat/{id}")]
        public async Task<string> CapNhat(int id, CapNhatLoaiBangQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatLoaiBangQuangCaoCommand()
            {
                CapNhatLoaiBangQuangCaoModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóaloại bảngquảng cáo
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("xoa")]
        public async Task<string> Xoa(XoaLoaiBangQuangCaoModel model)
        {
            return await _mediator.Send(new XoaLoaiBangQuangCaoCommand()
            {
                XoaLoaiBangQuangCaoModel = model
            });
        }
    }
}
