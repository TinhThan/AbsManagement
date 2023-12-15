using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.CanBo.CommandHandler
{
    public class CanNhatCanBoCommandHandler : BaseHandler, IRequestHandler<CanNhatCanBoCommand, string>
    {
        public CanNhatCanBoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CanNhatCanBoCommand request, CancellationToken cancellationToken)
        {
            var canBo = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (canBo == null)
            {
                throw new CustomMessageException(MessageSystem.VERSION_UPDATE, MessageSystem.VERSION_UPDATE);
            }
            var canBoCapNhat = _mapper.Map(request.CapNhatCanBoModel, canBo);
            canBoCapNhat.NgayCapNhat = DateTimeOffset.UtcNow;
            try
            {
                _dataContext.CanBos.Update(canBoCapNhat);
                var result = await _dataContext.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return "Cập nhật cán bộ thành công";
                }
                return "Cập nhật cán bộ thất bại";
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(ex.Message);
            }
        }
    }
}
