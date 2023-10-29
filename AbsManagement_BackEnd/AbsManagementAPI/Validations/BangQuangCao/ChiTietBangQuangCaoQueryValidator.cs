using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.BangQuangCao
{
    public class ChiTietBangQuangCaoQueryValidator : AbstractValidator<ChiTietBangQuangCaoQuery>
    {
        private readonly DataContext _dataContext;

        public ChiTietBangQuangCaoQueryValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessageBangQuangCao.BANGQUANGCAO_ID_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, cancellationToken) =>
                {
                    if (id > 0)
                    {
                        return await _dataContext.BangQuangCaos.AnyAsync(x => x.Id == id, cancellationToken);
                    }
                    return false;
                }).WithMessage(MessageBangQuangCao.BANGQUANGCAO_NOT_EXISTS);
        }
    }
}
