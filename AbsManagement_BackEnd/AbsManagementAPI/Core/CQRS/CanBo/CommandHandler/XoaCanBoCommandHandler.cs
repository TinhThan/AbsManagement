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
    public class XoaCanBoCommandHandler : BaseHandler, IRequestHandler<XoaCanBoCommand, string>
    {
        public XoaCanBoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaCanBoCommand request, CancellationToken cancellationToken)
        {
            var canBo = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Id == request.XoaCanBoModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(canBo);
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
