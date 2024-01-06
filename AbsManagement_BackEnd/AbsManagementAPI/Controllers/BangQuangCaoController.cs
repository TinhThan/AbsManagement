using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
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
        [HttpGet()]
        public async Task<List<BangQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachBangquangCaoQuery());
        }

        /// <summary>
        /// Danh sách bảng quảng cáo theo điểm đặt quảng cáo
        /// </summary>
        /// <param name="id"></param>
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
        [HttpPost("taomoi")]
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
        [HttpPost("capnhat/{id}")]
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
        [HttpPost("xoa/{id}")]
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
        /// <param name="model"></param>
        [HttpPost("gui/{id}")]
        public async Task<string> GuiDuyet(int id, GuiBangQuangCaoModel model)
        {
            return await _mediator.Send(new GuiBangQuangCaoCommand()
            {
                GuiBangQuangCaoModel = model,
                Id = id
            });
        }
    }
}
