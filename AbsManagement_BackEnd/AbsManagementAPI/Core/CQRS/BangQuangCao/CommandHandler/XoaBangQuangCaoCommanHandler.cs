using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class XoaBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<XoaBangQuangCaoCommand, string>
    {
        public XoaBangQuangCaoCommanHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var bangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == request.XoaBangQuangCaoModel.Id, cancellationToken);
            if (bangQuangCao.NgayCapNhat != request.XoaBangQuangCaoModel.NgayCapNhat)
            {
                throw new CustomMessageException(MessageSystem.VERSION_UPDATE, MessageSystem.VERSION_UPDATE, new object[]
                {
                    bangQuangCao.NgayCapNhat
                });
            }
            try
            {
                _dataContext.Remove(bangQuangCao);
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
