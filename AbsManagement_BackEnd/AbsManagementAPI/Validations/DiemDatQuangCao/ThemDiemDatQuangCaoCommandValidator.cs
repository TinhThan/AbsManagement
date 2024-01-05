using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.CQRS.DiemDatBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.DiemDatQuangCao
{

    public class ThemDiemDatQuangCaoCommandValidator : AbstractValidator<ThemDiemDatQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public ThemDiemDatQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.ThemDiemDatQuangCaoModel.IdLoaiViTri)
                .GreaterThan(0).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_IDLOAIVITRI_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id > 0)
                    {
                        return await _dataContext.LoaiViTris.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_IDLOAIVITRI_NOT_EXISTS);

            RuleFor(t => t.ThemDiemDatQuangCaoModel.IdHinhThucQuangCao)
               .GreaterThan(0).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_IDHINHTHUC_QUANGCAO_IS_NULL_OR_EMPTY)
               .MustAsync(async (id, canncellationToken) =>
               {
                   if (id > 0)
                   {
                       return await _dataContext.HinhThucQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                   }
                   return true;
               }).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_IDHINHTHUC_QUANGCAO_NOT_EXISTS);

            RuleFor(t => t.ThemDiemDatQuangCaoModel.DiaChi)
               .GreaterThan("").WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_DIACHI_IS_NULL_OR_EMPTY);
            RuleFor(t => t.ThemDiemDatQuangCaoModel.Phuong)
               .GreaterThan("").WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_PHUONG_IS_NULL_OR_EMPTY);      
            RuleFor(t => t.ThemDiemDatQuangCaoModel.Quan)
               .GreaterThan("").WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_QUAN_IS_NULL_OR_EMPTY);


        }
    }
}
