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
    public class XoaPhieuCapPhepSuaQuangCaoCommandHandler : BaseHandler, IRequestHandler<XoaPhieuCapPhepSuaQuangCaoCommand, string>
    {
        public XoaPhieuCapPhepSuaQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaPhieuCapPhepSuaQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var phieuCapPhepSuaQuangCao = await _dataContext.PhieuCapPhepSuaQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            try
            {
                _dataContext.Remove(phieuCapPhepSuaQuangCao);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    return MessageSystem.DELETE_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.DELETE_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.DELETE_FAIL, ex.Message);
            }
        }
    }
}
