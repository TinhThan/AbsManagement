using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Query;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.CanBo
{
    public class CapNhatCanBoCommandValidator : AbstractValidator<ChiTietBaoCaoViPhamQuery>
    {
        private readonly DataContext _dataContext;

        public CapNhatCanBoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessageCanBo.CANBO_ID_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, cancellationToken) =>
                {
                    if (id > 0)
                    {
                        return await _dataContext.CanBos.AnyAsync(x => x.Id == id, cancellationToken);
                    }
                    return false;
                }).WithMessage(MessageCanBo.CANBO_NOT_EXISTS);
        }
    }
}
