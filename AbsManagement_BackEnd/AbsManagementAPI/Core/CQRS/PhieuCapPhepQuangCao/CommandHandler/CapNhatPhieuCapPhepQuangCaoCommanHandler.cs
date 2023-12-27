using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.CommandHandler
{
    public class CapNhatPhieuCapPhepQuangCaoCommanHandler : BaseHandler, IRequestHandler<CapNhatPhieuCapPhepQuangCaoCommand, string>
    {
        public CapNhatPhieuCapPhepQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatPhieuCapPhepQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var PhieuCapPhepQuangCao = await _dataContext.PhieuCapPhepQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            try
            {
                var PhieuCapPhepQuangCaoCapNhat = _mapper.Map(request.CapNhatPhieuCapPhepQuangCaoModel, PhieuCapPhepQuangCao);
                
                PhieuCapPhepQuangCaoCapNhat.IdTinhTrang = "Update";
                _dataContext.Update(PhieuCapPhepQuangCaoCapNhat);
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
