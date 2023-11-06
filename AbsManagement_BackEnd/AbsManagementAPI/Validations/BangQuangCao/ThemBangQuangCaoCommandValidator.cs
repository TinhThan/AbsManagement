using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Validations.BangQuangCao
{
    public class ThemBangQuangCaoCommandValidator : AbstractValidator<ThemBangQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public ThemBangQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            //RuleFor(t => t.ThemBangQuangCaoModel.DiaChi)
            //    .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_DIACHI_IS_NULL_OR_EMPTY)
            //    .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_DIACHI_IS_NULL_OR_EMPTY);

            //RuleFor(t => t.ThemBangQuangCaoModel.Phuong)
            //    .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_PHUONG_IS_NULL_OR_EMPTY)
            //    .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_PHUONG_IS_NULL_OR_EMPTY);

            //RuleFor(t => t.ThemBangQuangCaoModel.Quan)
            //    .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_QUAN_IS_NULL_OR_EMPTY)
            //    .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_QUAN_IS_NULL_OR_EMPTY);

            //RuleFor(t => t.ThemBangQuangCaoModel.DanhSachViTri)
            //    .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_VITRI_IS_NULL_OR_EMPTY)
            //    .Must((vitris) =>
            //    {
            //        if (vitris.Count == 2)
            //        {
            //            return true;
            //        }
            //        return false;
            //    }).WithMessage(MessageBangQuangCao.BANGQUANGCAO_VITRI_INVALID);

            //RuleFor(t => t.ThemBangQuangCaoModel.MaLoaiViTri)
            //    .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_MALOAIVITRI_IS_NULL_OR_EMPTY)
            //    .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_MALOAIVITRI_IS_NULL_OR_EMPTY)
            //    .MustAsync(async (ma, canncellationToken) =>
            //    {
            //        if (!string.IsNullOrEmpty(ma))
            //        {
            //            return await _dataContext.LoaiViTris.AnyAsync(t => t.Ma == ma, canncellationToken);
            //        }
            //        return true;
            //    }).WithMessage(MessageBangQuangCao.BANGQUANGCAO_MALOAIVITRI_NOT_EXISTS);

            //RuleFor(t => t.ThemBangQuangCaoModel.MaHinhThucQuangCao)
            //    .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_MAHINHTHUC_QUANGCAO_IS_NULL_OR_EMPTY)
            //    .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_MAHINHTHUC_QUANGCAO_IS_NULL_OR_EMPTY)
            //    .MustAsync(async (ma, canncellationToken) =>
            //    {
            //        if (!string.IsNullOrEmpty(ma))
            //        {
            //            return await _dataContext.HinhThucQuangCaos.AnyAsync(t => t.Ma == ma, canncellationToken);
            //        }
            //        return true;
            //    }).WithMessage(MessageBangQuangCao.BANGQUANGCAO_MAHINHTHUC_QUANGCAO_NOT_EXISTS);

            RuleFor(t => t.ThemBangQuangCaoModel.IdLoaiBangQuangCao)
                .GreaterThan(0).WithMessage(MessageBangQuangCao.BANGQUANGCAO_IDLOAI_BANGQUANGCAO_IS_NULL_OR_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id > 0)
                    {
                        return await _dataContext.LoaiBangQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageBangQuangCao.BANGQUANGCAO_IDLOAI_BANGQUANGCAO_NOT_EXISTS);

            RuleFor(t => t.ThemBangQuangCaoModel.IdDiemDatQuangCao)
               .GreaterThan(0).WithMessage(MessageDiemDatQuangCao.ID_DIEMDATQUANGCAO_NOT_EMPTY)
               .MustAsync(async (id, canncellationToken) =>
               {
                   if (id > 0)
                   {
                       return await _dataContext.DiemDatQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                   }
                   return true;
               }).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_NOT_EXISTS);
        }
    }
}
