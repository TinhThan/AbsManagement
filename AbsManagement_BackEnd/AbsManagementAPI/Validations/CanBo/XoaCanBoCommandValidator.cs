using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.CanBo
{
    public class XoaCanBoCommandValidator : AbstractValidator<XoaCanBoCommand>
    {
        private readonly DataContext _dataContext;

        public XoaCanBoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.XoaCanBoModel.Id)
                .GreaterThan(0).WithMessage(MessageCanBo.CANBO_ID_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id != 0)
                    {
                        return await _dataContext.BangQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageCanBo.CANBO_NOT_EXISTS);
        }
    }
}
