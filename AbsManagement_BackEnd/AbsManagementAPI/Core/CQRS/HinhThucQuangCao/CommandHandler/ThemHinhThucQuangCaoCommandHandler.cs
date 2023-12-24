using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.HinhThucBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.HinhThucBangQuangCao.CommandHandler
{
    public class ThemHinhThucQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemHinhThucQuangCaoCommand, string>
    {
        public ThemHinhThucQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemHinhThucQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var HinhThucQuangCaoMoi = _mapper.Map<HinhThucQuangCaoEntity>(request.ThemHinhThucQuangCaoModel);

            try
            {
                await _dataContext.AddAsync(HinhThucQuangCaoMoi);
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
