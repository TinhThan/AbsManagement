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
        [HttpGet("chitiet/{id}")]
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
        [HttpGet("danhsach")]
        public async Task<List<PhieuCapPhepSuaQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachPhieuCapPhepSuaQuangCaoQuery());
        }


        /// <summary>
        /// Thêm mới Phiếu cấp phép sửa Quảng Cáo
        /// </summary>
        /// <param name="model"></param>
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
        [HttpPost("capnhat/{id}")]
        public async Task<string> CapNhat(int id, CapNhatPhieuCapPhepSuaQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatPhieuCapPhepSuaQuangCaoCommand()
            {
                CapNhatPhieuCapPhepSuaQuangCaoModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóa Phiếu cấp phép sửa Quảng Cáo
        /// </summary>
        /// <param name="id"></param>
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
