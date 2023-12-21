using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.LoaiViTri.CommandHandler
{
    public class XoaLoaiViTriCommandHandler : BaseHandler, IRequestHandler<XoaLoaiViTriCommand, string>
    {
        public XoaLoaiViTriCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaLoaiViTriCommand request, CancellationToken cancellationToken)
        {
            var loaiViTri = await _dataContext.LoaiViTris.FirstOrDefaultAsync(t => t.Id == request.XoaLoaiViTriModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(loaiViTri);
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
