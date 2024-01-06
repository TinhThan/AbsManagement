using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.DiemDatBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models;
using AbsManagementAPI.Core.Models.DiemDatQuangCao;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{
    /// <summary>
    /// Controller điểm đặt quảng cáo
    /// </summary>
    [ApiController]
    [Route("api/diemdatquangcao")]
    public class DiemDatQuangCaoController : BaseController
    {
        public DiemDatQuangCaoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Chi tiếtđiểm đặt quảng cáo
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("chitiet/{id}")]
        public async Task<DiemDatQuangCaoModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietDiemDatQuangCaoQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Danh sáchđiểm đặt quảng cáo
        /// </summary>
        /// <param name="addressSearchModel"></param>
        [HttpGet()]
        public async Task<List<DiemDatQuangCaoModel>> DanhSach([FromQuery]AddressSearchModel addressSearchModel)
        {
            return await _mediator.Send(new DanhSachDiemDatQuangCaoQuery()
            {
                AddressSearchModel = addressSearchModel
            });
        }

        /// <summary>
        /// Thêm mới điểm đặt quảng cáo
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("taomoi")]
        [Authorize]
        public async Task<string> TaoMoi(ThemDiemDatQuangCaoModel model)
        {
            return await _mediator.Send(new ThemDiemDatQuangCaoCommand()
            {
                ThemDiemDatQuangCaoModel = model
            });
        }

        /// <summary>
        /// Cập nhật điểm đặt quảng cáo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [HttpPost("capnhat/{id}")]
        [Authorize]
        public async Task<string> CapNhat(int id, CapNhatDiemDatQuangCaoModel model)
        {
            return await _mediator.Send(new CapNhatDiemDatQuangCaoCommand()
            {
                CapNhatDiemDatQuangCaoModel = model,
                Id = id
            });
        }

        /// <summary>
        /// Xóađiểm đặt quảng cáo
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("xoa")]
        public async Task<string> Xoa(XoaDiemDatQuangCaoModel model)
        {
            return await _mediator.Send(new XoaDiemDatQuangCaoCommand()
            {
                XoaDiemDatQuangCaoModel = model
            });
        }
    }
}
