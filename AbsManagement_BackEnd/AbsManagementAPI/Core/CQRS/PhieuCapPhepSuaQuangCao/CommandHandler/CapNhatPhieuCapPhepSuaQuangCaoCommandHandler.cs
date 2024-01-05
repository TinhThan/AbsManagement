﻿using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.CommandHandler
{
    public class CapNhatPhieuCapPhepSuaQuangCaoCommandHandler : BaseHandler, IRequestHandler<CapNhatPhieuCapPhepSuaQuangCaoCommand, string>
    {
        public CapNhatPhieuCapPhepSuaQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatPhieuCapPhepSuaQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var phieuCapPhepSuaQuangCao = await _dataContext.PhieuCapPhepSuaQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (phieuCapPhepSuaQuangCao == null)
            {
                return MessageSystem.UPDATE_FAIL;
            }
            try
            {
                if (request.TinhTrang != "DaHuy")
                {
                    if (phieuCapPhepSuaQuangCao.IdBangQuangCao != null)
                    {
                        var bangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == phieuCapPhepSuaQuangCao.IdBangQuangCao);
                        var capNhatBangQuangCaoModel = JsonConvert.DeserializeObject<CapNhatBangQuangCaoModel>(phieuCapPhepSuaQuangCao.NoiDung);
                        var bangQuangCaoCapNhat = _mapper.Map(capNhatBangQuangCaoModel, bangQuangCao);
                        bangQuangCaoCapNhat.IdTinhTrang = "HoanThanh";
                        _dataContext.Update<BangQuangCaoEntity>(bangQuangCaoCapNhat);
                    }

                    if (phieuCapPhepSuaQuangCao.IdDiemDat != null)
                    {
                        var diemDatQuangCao = await _dataContext.DiemDatQuangCaos.FirstOrDefaultAsync(t => t.Id == phieuCapPhepSuaQuangCao.IdDiemDat);
                        var capNhatDiemDatQuangCaoModel = JsonConvert.DeserializeObject<CapNhatBangQuangCaoModel>(phieuCapPhepSuaQuangCao.NoiDung);
                        var diemQuangCaoCapNhat = _mapper.Map(capNhatDiemDatQuangCaoModel, diemDatQuangCao);
                        diemQuangCaoCapNhat.IdTinhTrang = "DaQuyHoach";
                        _dataContext.Update<DiemDatQuangCaoEntity>(diemQuangCaoCapNhat);
                    }
                }

                phieuCapPhepSuaQuangCao.TinhTrang = request.TinhTrang;
                _dataContext.Update(phieuCapPhepSuaQuangCao);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    return MessageSystem.UPDATE_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.UPDATE_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.UPDATE_FAIL, ex.Message);
            }
        }
    }
}
