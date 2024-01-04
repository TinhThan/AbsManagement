using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.LoaiBangQuangCao
{
    public class CapNhatLoaiBangQuangCaoCommandValidator : AbstractValidator<CapNhatLoaiBangQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public CapNhatLoaiBangQuangCaoCommandValidator(DataContext dataContext)
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

           
            RuleFor(t => t.CapNhatLoaiBangQuangCaoModel.Ma)
               .GreaterThan("").WithMessage(MessageLoaiBangQuangCao.LOAIBANGQUANGCAO_MA_IS_NULL_OR_EMPTY);
            RuleFor(t => t.CapNhatLoaiBangQuangCaoModel.Ten)
               .GreaterThan("").WithMessage(MessageLoaiBangQuangCao.LOAIBANGQUANGCAO_TEN_IS_NULL_OR_EMPTY);

        }
    }
}
