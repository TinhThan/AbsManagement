using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.CommandHandler
{
    public class ThemPhieuCapPhepQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemPhieuCapPhepQuangCaoCommand, string>
    {
        public ThemPhieuCapPhepQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemPhieuCapPhepQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var PhieuCapPhepQuangCaoMoi = _mapper.Map<PhieuCapPhepQuangCaoEntity>(request.ThemPhieuCapPhepQuangCaoModel);
            try
            {
                PhieuCapPhepQuangCaoMoi.IdTinhTrang = "ChoDuyet";
                await _dataContext.AddAsync(PhieuCapPhepQuangCaoMoi);
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
