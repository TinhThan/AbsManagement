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
        /// Báo cáo vi phạm
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<BaoCaoViPhamModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ChiTietBaoCaoViPhamQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Báo cáo vi phạm
        /// </summary>
        /// <param name="addressSearchModel"></param>
        [HttpGet()]
        public async Task<List<BaoCaoViPhamModel>> DanhSach([FromQuery] AddressSearchModel addressSearchModel)
        {
            return await _mediator.Send(new DanhSachBaoCaoViPhamQuery()
            {
                addressSearchModel = addressSearchModel
            });
        }


        /// <summary>
        /// Thêm mới báo cáo vi phạm
        /// </summary>
        /// <param name="model"></param>
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