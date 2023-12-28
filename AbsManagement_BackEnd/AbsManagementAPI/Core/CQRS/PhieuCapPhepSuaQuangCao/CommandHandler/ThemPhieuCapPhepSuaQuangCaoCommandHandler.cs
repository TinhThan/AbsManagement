using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Entities;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.CommandHandler
{

    public class ThemPhieuCapPhepSuaQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemPhieuCapPhepSuaQuangCaoCommand, string>
    {
        public ThemPhieuCapPhepSuaQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemPhieuCapPhepSuaQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var phieuCapPhepSuaQuangCao = _mapper.Map<PhieuCapPhepSuaQuangCaoEntity>(request.ThemPhieuCapPhepSuaQuangCaoModel);

            try
            {
                phieuCapPhepSuaQuangCao.NgayGui = DateTimeOffset.UtcNow;
                phieuCapPhepSuaQuangCao.TinhTrang = "Mới tạo phiếu";
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
