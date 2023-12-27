using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var phieuCapPhepSuaQuangCaoCapNhat = _mapper.Map(request.CapNhatPhieuCapPhepSuaQuangCaoModel, phieuCapPhepSuaQuangCao);

                phieuCapPhepSuaQuangCaoCapNhat.NgayGui = DateTimeOffset.UtcNow;
                _dataContext.Update(phieuCapPhepSuaQuangCaoCapNhat);
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
