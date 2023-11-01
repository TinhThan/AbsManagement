using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AbsManagementAPI.Validations.BangQuangCao
{
    public class XoaBangQuangCaoCommandValidator : AbstractValidator<XoaBangQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public XoaBangQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.XoaBangQuangCaoModel.Id)
                .GreaterThan(0).WithMessage(MessageBangQuangCao.BANGQUANGCAO_ID_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id != 0)
                    {
                        return await _dataContext.BangQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageBangQuangCao.BANGQUANGCAO_NOT_EXISTS);
        }
    }
}
