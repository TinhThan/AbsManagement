using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.DiemDatQuangCao
{
    public class ChiTietDiemDatQuangCaoQueryValidator : AbstractValidator<ChiTietDiemDatQuangCaoQuery>
    {
        private readonly DataContext _dataContext;

        public ChiTietDiemDatQuangCaoQueryValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessageDiemDatQuangCao.ID_DIEMDATQUANGCAO_NOT_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id != 0)
                    {
                        return await _dataContext.DiemDatQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_NOT_EXISTS);
        }
    }
}
