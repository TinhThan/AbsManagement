﻿using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.PhieuCapPhepQuangCao
{

    public class ThemPhieuCapPhepQuangCaoCommandValidator : AbstractValidator<ThemPhieuCapPhepQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public ThemPhieuCapPhepQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;
            //RuleFor(t => t.ThemPhieuCapPhepQuangCaoModel.IdDiemDatQuangCao)
            //                .GreaterThan(0).WithMessage(MessagePhieuCapPhepQuangCao.PHIEUCAPPHEPQUANGCAO_DIEMDATQUANGCAO_NOT_EMPTY)
            //                .MustAsync(async (id, canncellationToken) =>
            //                {
            //                    if (id > 0)
            //                    {
            //                        return await _dataContext.DiemDatQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
            //                    }
            //                    return true;
            //                }).WithMessage(MessagePhieuCapPhepQuangCao.PHIEUCAPPHEPQUANGCAO_DIEMDATQUANGCAO_NOT_EXISTS);

            //RuleFor(t => t.ThemPhieuCapPhepQuangCaoModel.IdLoaiBangQuangCao)
            //   .GreaterThan(0).WithMessage(MessagePhieuCapPhepQuangCao.PHIEUCAPPHEPQUANGCAO_IDLOAIBANGQUANGCAO_ID_IS_NULL_OR_EMPTY)
            //   .MustAsync(async (id, canncellationToken) =>
            //   {
            //       if (id > 0)
            //       {
            //           return await _dataContext.LoaiBangQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
            //       }
            //       return true;
            //   }).WithMessage(MessagePhieuCapPhepQuangCao.PHIEUCAPPHEPQUANGCAO_IDLOAIBANGQUANGCAO_NOT_EXISTS);

        }
    }
}
