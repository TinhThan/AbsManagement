using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.CommandHandler
{
    public class CapNhatBaoCaoViPhamCommandHandler : BaseHandler, IRequestHandler<CapNhatBaoCaoViPhamCommand, string>
    {
        public CapNhatBaoCaoViPhamCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }
        public async Task<string> Handle(CapNhatBaoCaoViPhamCommand request, CancellationToken cancellationToken)
        {
            var baoCaoViPham = await _dataContext.BaoCaoViPhams.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (baoCaoViPham.Id!=null)
            {
                throw new CustomMessageException(MessageSystem.DATA_INVALID);

            }
            try
            {
                var baoCaoViPhamCapNhat = _mapper.Map(request.CapNhatBaoCaoViPhamModel, baoCaoViPham);

                _dataContext.Update(baoCaoViPhamCapNhat);
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
