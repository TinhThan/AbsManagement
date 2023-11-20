using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.CanBo.CommandHandler
{
    public class XoaCanBoCommanHandler : BaseHandler, IRequestHandler<XoaCanBoCommand, string>
    {
        public XoaCanBoCommanHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
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
