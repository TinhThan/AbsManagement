using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AbsManagementAPI.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.CommandHandler
{

    public class ThemPhieuCapPhepSuaQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemPhieuCapPhepSuaQuangCaoCommand, string>
    {
        public ThemPhieuCapPhepSuaQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemPhieuCapPhepSuaQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var phieuCapPhepSuaQuangCao = new PhieuCapPhepSuaQuangCaoEntity()
            {
                IdBangQuangCao = request.ThemPhieuCapPhepSuaQuangCaoModel.IdBangQuangCao,
                IdDiemDat = request.ThemPhieuCapPhepSuaQuangCaoModel.IdDiemDat
            };
            try
            {
                phieuCapPhepSuaQuangCao.NgayGui = DateTimeOffset.UtcNow;
                if (phieuCapPhepSuaQuangCao.IdBangQuangCao != null)
                {
                    phieuCapPhepSuaQuangCao.NoiDung = JsonConvert.SerializeObject(request.ThemPhieuCapPhepSuaQuangCaoModel.CapNhatBangQuangCao);
                    var bangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == phieuCapPhepSuaQuangCao.IdBangQuangCao);
                    bangQuangCao.IdTinhTrang = "ChoDuyet";
                    _dataContext.Update<BangQuangCaoEntity>(bangQuangCao);
                }
                if (phieuCapPhepSuaQuangCao.IdDiemDat != null)
                {
                    phieuCapPhepSuaQuangCao.NoiDung = JsonConvert.SerializeObject(request.ThemPhieuCapPhepSuaQuangCaoModel.CapNhatDiemQuangCao);
                    var diemDat = await _dataContext.DiemDatQuangCaos.FirstOrDefaultAsync(t => t.Id == phieuCapPhepSuaQuangCao.IdDiemDat);
                    diemDat.IdTinhTrang = "ChoDuyet";
                    _dataContext.Update<DiemDatQuangCaoEntity>(diemDat);
                }
                phieuCapPhepSuaQuangCao.TinhTrang = "ChoDuyet";
                await _dataContext.AddAsync(phieuCapPhepSuaQuangCao);
                var resultThemMoi = await _dataContext.SaveChangesAsync();
                if (resultThemMoi > 0)
                {
                    return MessageSystem.ADD_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.ADD_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
