using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.HinhThucQuangCao.CommandHandler
{
    public class CapNhatHinhThucQuangCaoCommanHandler : BaseHandler, IRequestHandler<CapNhatHinhThucQuangCaoCommand, string>
    {
        public CapNhatHinhThucQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatHinhThucQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var HinhThucQuangCao = await _dataContext.HinhThucQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            try
            {
                var HinhThucQuangCaoCapNhat = _mapper.Map(request.CapNhatHinhThucQuangCaoModel, HinhThucQuangCao);

                _dataContext.Update(HinhThucQuangCaoCapNhat);
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
