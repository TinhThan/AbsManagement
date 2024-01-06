using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class GuiBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<GuiBangQuangCaoCommand, string>
    {
        public GuiBangQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(GuiBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
                var phieucapphep = new PhieuCapPhepQuangCaoEntity();
                phieucapphep.IdBangQuangCao = request.Id;
                phieucapphep.IdCanBoDuyet = request.GuiBangQuangCaoModel.IdCanBoDuyet;
                phieucapphep.IdTinhTrang = "New";

                bangQuangCao.IdTinhTrang = "Approving";  
                await _dataContext.AddAsync(phieucapphep);
                _dataContext.Update(bangQuangCao);

                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    return MessageSystem.APPROVING_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.APPROVING_SUCCESS);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.APPROVING_FAIL, ex.Message);
            }
        }
    }
}

