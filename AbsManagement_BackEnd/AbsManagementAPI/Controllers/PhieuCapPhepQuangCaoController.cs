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
    public class PhieuCapPhepQuangCaoController : BaseController
    {
        public PhieuCapPhepQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiết PhieuCapPhep quảng cáo
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
        /// Danh sách PhieuCapPhep quảng cáo
        /// </summary>
        /// <param name="id"></param>
        [HttpGet()]
        public async Task<List<PhieuCapPhepQuangCaoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachPhieuCapPhepQuangCaoQuery());
        }

        /// <summary>
        /// Thêm mới PhieuCapPhep quảng cáo
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("taomoi")]
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
        [HttpPost("capnhat/{id}")]
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
        [HttpPost("xoa")]
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
        [HttpPost("duyet")]
        public async Task<string> Duyet(XoaPhieuCapPhepQuangCaoModel model)
        {
            return await _mediator.Send(new DuyetPhieuCapPhepQuangCaoCommand()
            {
                DuyetPhieuCapPhepQuangCao = model
            });
        }
    }
}
