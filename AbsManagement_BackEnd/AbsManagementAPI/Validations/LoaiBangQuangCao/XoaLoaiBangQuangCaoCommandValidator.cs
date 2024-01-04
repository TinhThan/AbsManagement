using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiBangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.LoaiBangQuangCao
{

    public class XoaLoaiBangQuangCaoCommandValidator : AbstractValidator<XoaLoaiBangQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public XoaLoaiBangQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.XoaLoaiBangQuangCaoModel.Id)
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
