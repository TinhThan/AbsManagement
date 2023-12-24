using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.CommandHandler
{
    public class ThemLoaiBangQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemLoaiBangQuangCaoCommand, string>
    {
        public ThemLoaiBangQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemLoaiBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var LoaiBangQuangCaoMoi = _mapper.Map<LoaiBangQuangCaoEntity>(request.ThemLoaiBangQuangCaoModel);

            try
            {
                await _dataContext.AddAsync(LoaiBangQuangCaoMoi);
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
