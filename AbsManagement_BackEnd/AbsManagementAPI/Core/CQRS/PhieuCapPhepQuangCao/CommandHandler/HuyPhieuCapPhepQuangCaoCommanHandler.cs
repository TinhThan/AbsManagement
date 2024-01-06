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
    public class HuyPhieuCapPhepQuangCaoCommanHandler : BaseHandler, IRequestHandler<HuyPhieuCapPhepQuangCaoCommand, string>
    {
        public HuyPhieuCapPhepQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(HuyPhieuCapPhepQuangCaoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var PhieuCapPhepQuangCao = await _dataContext.PhieuCapPhepQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
                var BangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == PhieuCapPhepQuangCao.IdBangQuangCao, cancellationToken);
                PhieuCapPhepQuangCao.IdTinhTrang = "KhongDuyet";

                BangQuangCao.IdTinhTrang = "KhongDuyet";

                _dataContext.Update(PhieuCapPhepQuangCao);
                _dataContext.Update(BangQuangCao);

                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    return MessageSystem.CANCEL_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.CANCEL_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.CANCEL_FAIL, ex.Message);
            }
        }
    }
}
