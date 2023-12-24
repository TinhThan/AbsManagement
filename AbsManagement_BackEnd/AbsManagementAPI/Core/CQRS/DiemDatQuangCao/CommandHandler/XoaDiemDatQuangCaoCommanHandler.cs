using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class XoaDiemDatQuangCaoCommanHandler : BaseHandler, IRequestHandler<XoaDiemDatQuangCaoCommand, string>
    {
        public XoaDiemDatQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaDiemDatQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var diemDatQuangCao = await _dataContext.DiemDatQuangCaos.FirstOrDefaultAsync(t => t.Id == request.XoaDiemDatQuangCaoModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(diemDatQuangCao);
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
