using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.CQRS.LoaiBangBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.LoaiBangQuangCao
{

    public class ThemLoaiBangQuangCaoCommandValidator : AbstractValidator<ThemLoaiBangQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public ThemLoaiBangQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.ThemLoaiBangQuangCaoModel.Ma)
            .GreaterThan("").WithMessage(MessageLoaiBangQuangCao.LOAIBANGQUANGCAO_MA_IS_NULL_OR_EMPTY);
            RuleFor(t => t.ThemLoaiBangQuangCaoModel.Ten)
               .GreaterThan("").WithMessage(MessageLoaiBangQuangCao.LOAIBANGQUANGCAO_TEN_IS_NULL_OR_EMPTY);


        }
    }
}
