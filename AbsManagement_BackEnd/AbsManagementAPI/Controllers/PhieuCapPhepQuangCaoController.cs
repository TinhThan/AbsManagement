using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
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
    public class PhieuCapPhepQuangCaoController : BaseController
    {
        public PhieuCapPhepQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiết phiếu cấp phép quảng cáo
        /// </summary>
        /// <param name="id"></param>g hợp lệ</response>
        [HttpGet("chitiet/{id}")]
        public async Task<PhieuCapPhepQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietPhieuCapPhepQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sách phiếu cấp phép quảng cáo
        /// </summary>
        /// <param name="id"></param>
        [HttpGet()]
        public async Task<List<PhieuCapPhepQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachPhieuCapPhepQuangCaoQuery());
        }

        /// <summary>
        /// Thêm mới phiếu cấp phép quảng cáo
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("taomoi")]
        [Authorize]
        public async Task<string> TaoMoi(ThemPhieuCapPhepQuangCaoModel model)
        {
            return await _mediator.Send(new ThemPhieuCapPhepQuangCaoCommand()
            {
                ThemPhieuCapPhepQuangCaoModel = model
            });
        }

        /// <summary>
        /// Cập nhật phiếu cấp phép quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [HttpPost("capnhat/{id}")]
        [Authorize]
        public async Task<string> CapNhat(int id, CapNhatPhieuCapPhepQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatPhieuCapPhepQuangCaoCommand()
            {
                CapNhatPhieuCapPhepQuangCaoModel = model,
                Id = id
            });
        }


        /// <summary>
        /// Xóa phiếu cấp phép quảng cáo
        /// </summary>
        /// <param name="id"></param>
        [HttpPost("xoa/{id}")]
        public async Task<string> Xoa(int id)
        {
            return await _mediator.Send(new XoaPhieuCapPhepQuangCaoCommand()
            {
                Id = id
            });
        }

        /// <summary>
        /// duyệt phiếu quảng cáo
        /// </summary>
        /// /// <param name="id"></param>
        [HttpPost("duyet/{id}")]
        [Authorize]
        public async Task<string> Duyet(int id)
        {
            return await _mediator.Send(new DuyetPhieuCapPhepQuangCaoCommand
            {
                Id = id
            });
        }

        /// <summary>
        /// Hủy phiếu quảng cáo
        /// </summary>
        /// /// <param name="id"></param>
        [HttpPost("huy/{id}")]
        [Authorize]
        public async Task<string> Huy(int id)
        {
            return await _mediator.Send(new HuyPhieuCapPhepQuangCaoCommand
            {
                Id = id
            });
        }
    }
}
