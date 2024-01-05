using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.DiemDatBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.DiemDatQuangCao.CommandHandler
{
    public class ThemDiemDatQuangCaoCommandHandler : BaseHandler, IRequestHandler<ThemDiemDatQuangCaoCommand, string>
    {
        public ThemDiemDatQuangCaoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(ThemDiemDatQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var diemDatQuangCaoMoi = _mapper.Map<DiemDatQuangCaoEntity>(request.ThemDiemDatQuangCaoModel);

            try
            {
                diemDatQuangCaoMoi.IdTinhTrang = "DaQuyHoach";
                await _dataContext.AddAsync(diemDatQuangCaoMoi);
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
