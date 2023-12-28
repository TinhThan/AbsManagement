using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class CapNhatBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<CapNhatBangQuangCaoCommand, string>
    {
        public CapNhatBangQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var bangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (bangQuangCao.NgayBatDau != request.CapNhatBangQuangCaoModel.NgayBatDau)
            {
                throw new CustomMessageException(MessageSystem.VERSION_UPDATE, MessageSystem.VERSION_UPDATE, new object[]
                {
                    bangQuangCao.NgayBatDau
                });
            }
            try
            {
                var bangQuangCaoCapNhat = _mapper.Map(request.CapNhatBangQuangCaoModel, bangQuangCao);

                bangQuangCaoCapNhat.NgayCapNhat = DateTimeOffset.UtcNow;
                _dataContext.Update(bangQuangCaoCapNhat);
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
