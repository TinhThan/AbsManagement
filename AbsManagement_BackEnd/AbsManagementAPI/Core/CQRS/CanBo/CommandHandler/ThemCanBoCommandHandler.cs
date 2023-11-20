using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.CanBo.CommandHandler
{
    public class ThemCanBoCommandHandler : BaseHandler, IRequestHandler<ThemMoiCanBoCommand, string>
    {
        public ThemCanBoCommandHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemMoiCanBoCommand request, CancellationToken cancellationToken)
        {
            var canBo = _mapper.Map<CanBoEntity>(request.ThemMoiCanBoModel);

            try
            {
                await _dataContext.AddAsync(canBo);
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
