using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.BangQuangCao;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.CommandHandler
{
    public class DuyetPhieuCapPhepQuangCaoCommanHandler : BaseHandler, IRequestHandler<DuyetPhieuCapPhepQuangCaoCommand, string>
    {
        public DuyetPhieuCapPhepQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(DuyetPhieuCapPhepQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var phieuCapPhepQuangCao = await _dataContext.PhieuCapPhepQuangCaos.FirstOrDefaultAsync(t => t.Id == request.DuyetPhieuCapPhepQuangCao.Id, cancellationToken);
            try
            {
                phieuCapPhepQuangCao.IdTinhTrang = "DaDuyet";
                var bangQuangCao = new BangQuangCaoEntity();
                bangQuangCao.IdTinhTrang = "DaQuyHoach";

                 await _dataContext.AddAsync(bangQuangCao);
                _dataContext.Update(phieuCapPhepQuangCao);

                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    return MessageSystem.APPROVE_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.APPROVE_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.DELETE_FAIL, ex.Message);
            }
        }
    }
}
