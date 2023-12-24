using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.CommandHandler
{
    public class CapNhatDiemDatQuangCaoCommanHandler : BaseHandler, IRequestHandler<CapNhatDiemDatQuangCaoCommand, string>
    {
        public CapNhatDiemDatQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatDiemDatQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var diemDatQuangCao = await _dataContext.DiemDatQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            try
            {
                var diemDatQuangCaoCapNhat = _mapper.Map(request.CapNhatDiemDatQuangCaoModel, diemDatQuangCao);

                diemDatQuangCaoCapNhat.IdTinhTrang = "UPDATE";
                _dataContext.Update(diemDatQuangCaoCapNhat);
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
