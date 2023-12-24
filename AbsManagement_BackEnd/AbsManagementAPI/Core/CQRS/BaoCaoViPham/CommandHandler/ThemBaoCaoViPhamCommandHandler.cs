using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.CommandHandler
{
    public class ThemBaoCaoViPhamCommandHandler : BaseHandler, IRequestHandler<ThemBaoCaoViPhamCommand, string>
    {
        public ThemBaoCaoViPhamCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }
        public async Task<string> Handle(ThemBaoCaoViPhamCommand request, CancellationToken cancellationToken)
        {
            var baoCaoViPham = _mapper.Map<BaoCaoViPhamEntity>(request.ThemBaoCaoViPhamModel);

            try
            {
                baoCaoViPham.IdTinhTrang = "ChuaXuLy";
                baoCaoViPham.CreateDate = DateTime.Now;
                await _dataContext.AddAsync(baoCaoViPham);
                var resultThemMoi = await _dataContext.SaveChangesAsync();
                if (resultThemMoi > 0)
                {
                    return MessageSystem.ADD_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.ADD_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
