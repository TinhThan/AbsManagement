using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.PhieuCapPhepSuaQuangCao
{
    public class CapNhatPhieuCapPhepSuaQuangCaoCommandValidator : AbstractValidator<CapNhatPhieuCapPhepSuaQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public CapNhatPhieuCapPhepSuaQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessagePhieuCapPhepSuaQuangCao.PHIEUCAPPHEPSUAQUANGCAO_ID_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id != 0)
                    {
                        return await _dataContext.PhieuCapPhepSuaQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessagePhieuCapPhepSuaQuangCao.PHIEUCAPPHEPSUAQUANGCAO_NOT_EXISTS);

            RuleFor(t => t.CapNhatPhieuCapPhepSuaQuangCaoModel.IdBangQuangCao)
                .GreaterThan(0).WithMessage(MessagePhieuCapPhepSuaQuangCao.PHIEUCAPPHEPSUAQUANGCAO_IDBANGQUANGCAO_ID_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id > 0)
                    {
                        return await _dataContext.BangQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessagePhieuCapPhepSuaQuangCao.PHIEUCAPPHEPSUAQUANGCAO_IDBANGQUANGCAO_NOT_EXISTS);

            RuleFor(t => t.CapNhatPhieuCapPhepSuaQuangCaoModel.IdDiemDat)
               .GreaterThan(0).WithMessage(MessagePhieuCapPhepSuaQuangCao.PHIEUCAPPHEPSUAQUANGCAO_DIEMDATQUANGCAO_NOT_EMPTY)
               .MustAsync(async (id, canncellationToken) =>
               {
                   if (id > 0)
                   {
                       return await _dataContext.DiemDatQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                   }
                   return true;
               }).WithMessage(MessagePhieuCapPhepSuaQuangCao.PHIEUCAPPHEPSUAQUANGCAO_DIEMDATQUANGCAO_NOT_EMPTY);

        }
    }
}
