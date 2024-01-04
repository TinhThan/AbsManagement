using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.PhieuCapPhepQuangCao
{
    public class CapNhatPhieuCapPhepQuangCaoCommandValidator : AbstractValidator<CapNhatPhieuCapPhepQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public CapNhatPhieuCapPhepQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessagePhieuCapPhepQuangCao.PHIEUCAPPHEPQUANGCAO_ID_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id != 0)
                    {
                        return await _dataContext.PhieuCapPhepQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessagePhieuCapPhepQuangCao.PHIEUCAPPHEPQUANGCAO_NOT_EXISTS);

            RuleFor(t => t.CapNhatPhieuCapPhepQuangCaoModel.IdDiemDatQuangCao)
                .GreaterThan(0).WithMessage(MessagePhieuCapPhepQuangCao.PHIEUCAPPHEPQUANGCAO_IDBANGQUANGCAO_ID_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id > 0)
                    {
                        return await _dataContext.DiemDatQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessagePhieuCapPhepQuangCao.PHIEUCAPPHEPQUANGCAO_IDBANGQUANGCAO_NOT_EXISTS);

            RuleFor(t => t.CapNhatPhieuCapPhepQuangCaoModel.IdLoaiBangQuangCao)
               .GreaterThan(0).WithMessage(MessagePhieuCapPhepQuangCao.PHIEUCAPPHEPQUANGCAO_DIEMDATQUANGCAO_NOT_EMPTY)
               .MustAsync(async (id, canncellationToken) =>
               {
                   if (id > 0)
                   {
                       return await _dataContext.LoaiBangQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                   }
                   return true;
               }).WithMessage(MessagePhieuCapPhepQuangCao.PHIEUCAPPHEPQUANGCAO_DIEMDATQUANGCAO_NOT_EXISTS);

        }
    }
}
