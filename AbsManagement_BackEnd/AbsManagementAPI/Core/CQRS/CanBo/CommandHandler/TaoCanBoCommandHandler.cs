using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.CanBo.CommandHandler
{
    public class TaoCanBoCommandHandler : BaseHandler, IRequestHandler<TaoCanBoCommand, string>
    {
        public TaoCanBoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(TaoCanBoCommand request, CancellationToken cancellationToken)
        {
            var canBoMoi = _mapper.Map<CanBoEntity>(request.TaoCanBoModel);

            canBoMoi.MatKhau = HelperIdentity.HashPasswordSalt("1");
            try
            {
                await _dataContext.CanBos.AddAsync(canBoMoi);
                var result = await _dataContext.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return "Thêm cán bộ thành công";
                }
                return "Thêm cán bộ thất bại";
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(ex.Message);
            }
        }
    }
}
