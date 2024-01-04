using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.LoaiBangQuangCao
{
    public class ChiTietLoaiBangQuangCaoQueryValidator : AbstractValidator<ChiTietLoaiBangQuangCaoQuery>
    {
        private readonly DataContext _dataContext;

        public ChiTietLoaiBangQuangCaoQueryValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessageLoaiBangQuangCao.ID_LOAIBANGQUANGCAO_NOT_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id != 0)
                    {
                        return await _dataContext.LoaiBangQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageLoaiBangQuangCao.LOAIBANGQUANGCAO_NOT_EXISTS);
        }
    }
}
