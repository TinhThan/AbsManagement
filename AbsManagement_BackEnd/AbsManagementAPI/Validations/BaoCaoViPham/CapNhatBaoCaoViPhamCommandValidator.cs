using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Query;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.BaoCaoViPham
{
    public class CapNhatBaoCaoViPhamCommandValidator : AbstractValidator<ChiTietBaoCaoViPhamQuery>
    {

        private readonly DataContext _dataContext;

        public CapNhatBaoCaoViPhamCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessageBaoCaoViPham.BAOCAOVIPHAM_ID_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, cancellationToken) =>
                {
                    if (id > 0)
                    {
                        return await _dataContext.BaoCaoViPhams.AnyAsync(x => x.Id == id, cancellationToken);
                    }
                    return false;
                }).WithMessage(MessageBaoCaoViPham.BAOCAOVIPHAM_NOT_EXISTS);
        }
    }
}
