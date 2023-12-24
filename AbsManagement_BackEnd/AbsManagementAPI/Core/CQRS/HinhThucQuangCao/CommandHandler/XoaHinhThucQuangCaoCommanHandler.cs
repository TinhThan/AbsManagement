using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class XoaHinhThucQuangCaoCommanHandler : BaseHandler, IRequestHandler<XoaHinhThucQuangCaoCommand, string>
    {
        public XoaHinhThucQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaHinhThucQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var HinhThucQuangCao = await _dataContext.HinhThucQuangCaos.FirstOrDefaultAsync(t => t.Id == request.XoaHinhThucQuangCaoModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(HinhThucQuangCao);
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
