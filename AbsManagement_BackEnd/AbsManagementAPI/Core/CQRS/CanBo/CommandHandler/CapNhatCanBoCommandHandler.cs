using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.CanBo.CommandHandler
{
    public class CapNhatCanBoCommandHandler : BaseHandler, IRequestHandler<CapNhatCanBoCommand, string>
    {
        public CapNhatCanBoCommandHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatCanBoCommand request, CancellationToken cancellationToken)
        {
            var canBo = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (canBo.Id != null)
            {
                throw new CustomMessageException(MessageSystem.DATA_INVALID);

            }
            try
            {
                var canBoCapNhat = _mapper.Map(request.CapNhatBaoCaoViPhamModel, canBo);

                _dataContext.Update(canBoCapNhat);
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
