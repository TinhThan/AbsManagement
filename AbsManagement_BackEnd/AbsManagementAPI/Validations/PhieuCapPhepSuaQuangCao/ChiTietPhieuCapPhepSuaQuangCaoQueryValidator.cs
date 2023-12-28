using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.PhieuCapPhepSuaQuangCao
{
    public class ChiTietPhieuCapPhepSuaQuangCaoQueryValidator : AbstractValidator<ChiTietPhieuCapPhepSuaQuangCaoQuery>
    {
        private readonly DataContext _dataContext;

        public ChiTietPhieuCapPhepSuaQuangCaoQueryValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessagePhieuCapPhepSuaQuangCao.PHIEUCAPPHEPSUAQUANGCAO_ID_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, cancellationToken) =>
                {
                    if (id > 0)
                    {
                        return await _dataContext.PhieuCapPhepSuaQuangCaos.AnyAsync(x => x.Id == id, cancellationToken);
                    }
                    return false;
                }).WithMessage(MessagePhieuCapPhepSuaQuangCao.PHIEUCAPPHEPSUAQUANGCAO_DIEMDATQUANGCAO_NOT_EXISTS);
        }
    }
}
