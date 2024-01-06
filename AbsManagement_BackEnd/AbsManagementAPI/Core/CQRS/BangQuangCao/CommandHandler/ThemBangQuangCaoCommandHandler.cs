using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class ThemBangQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemBangQuangCaoCommand, string>
    {
        public ThemBangQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var bangQuangCaoMoi = _mapper.Map<BangQuangCaoEntity>(request.ThemBangQuangCaoModel);

            try
            {
                bangQuangCaoMoi.NgayBatDau = DateTimeOffset.UtcNow;
                bangQuangCaoMoi.IdTinhTrang = "DaQuyHoach";
                await _dataContext.AddAsync(bangQuangCaoMoi);
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
