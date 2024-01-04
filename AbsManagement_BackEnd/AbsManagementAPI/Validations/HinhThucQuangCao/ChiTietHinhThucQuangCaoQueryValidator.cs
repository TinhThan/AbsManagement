using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.HinhThucQuangCao
{
    public class ChiTietHinhThucQuangCaoQueryValidator : AbstractValidator<ChiTietHinhThucQuangCaoQuery>
    {
        private readonly DataContext _dataContext;

        public ChiTietHinhThucQuangCaoQueryValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessageHinhThucQuangCao.ID_HINHTHUCQUANGCAO_NOT_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id != 0)
                    {
                        return await _dataContext.HinhThucQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageHinhThucQuangCao.HINHTHUCQUANGCAO_NOT_EXISTS);
        }
    }
}
