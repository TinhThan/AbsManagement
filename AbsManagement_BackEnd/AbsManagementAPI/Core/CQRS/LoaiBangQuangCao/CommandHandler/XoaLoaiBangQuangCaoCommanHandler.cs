using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.CommandHandler
{
    public class XoaLoaiBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<XoaLoaiBangQuangCaoCommand, string>
    {
        public XoaLoaiBangQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaLoaiBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var LoaiBangQuangCao = await _dataContext.LoaiBangQuangCaos.FirstOrDefaultAsync(t => t.Id == request.XoaLoaiBangQuangCaoModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(LoaiBangQuangCao);
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
