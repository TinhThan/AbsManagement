﻿using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Query;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.CanBo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbsManagementAPI.Controllers
{
    [ApiController]
    [Route("api/canbo")]
    public class CanBoController : BaseController
    {
        public CanBoController(IMediator mediator) : base(mediator)
        {
        }


        /// <summary>
        /// Danh sách cán bộ
        /// </summary>
        /// <response code="200">Lấy danh sách cán bộ thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("danhsach")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CanBoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<List<CanBoModel>> DanhSach()
        {
            return await _mediator.Send(new DanhSachCanBoQuery());
        }

        /// <summary>
        /// Thêm mới cán bộ
        /// </summary>
        /// <param name="taoCanBoModel"></param>
        /// <response code="200">Thêm mới cán bộ thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("taomoi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> TaoMoi(TaoCanBoModel taoCanBoModel)
        {
            return await _mediator.Send(new TaoCanBoCommand()
            {
                TaoCanBoModel = taoCanBoModel
            });
        }

        /// <summary>
        /// Cập nhật cán bộ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="capNhatCanBoModel"></param>
        /// <response code="200">Cập nhật cán bộ thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("capnhat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomException))]
        public async Task<string> CapNhat(int id, CapNhatCanBoModel capNhatCanBoModel)
        {
            return await _mediator.Send(new CanNhatCanBoCommand()
            {
                CapNhatCanBoModel = capNhatCanBoModel,
                Id = id
            });
        }
    }
}