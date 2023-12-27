using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.LoaiViTri.CommandHandler
{
    public class CapNhatLoaiViTriCommandler : BaseHandler, IRequestHandler<CapNhatLoaiViTriCommand, string>
    {
        public CapNhatLoaiViTriCommandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }
        public async Task<string> Handle(CapNhatLoaiViTriCommand request, CancellationToken cancellationToken)
        {
            var loaiViTri = await _dataContext.LoaiViTris.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (loaiViTri == null)
            {
                throw new CustomMessageException(MessageSystem.DATA_INVALID);

            }
            try
            {
                var loaiViTriCapNhat = _mapper.Map(request.CapNhatLoaiViTriModel, loaiViTri);

                _dataContext.Update(loaiViTriCapNhat);
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
