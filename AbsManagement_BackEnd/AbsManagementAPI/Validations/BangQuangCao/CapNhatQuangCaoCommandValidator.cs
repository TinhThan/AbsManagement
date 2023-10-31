using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.BangQuangCao
{
    public class CapNhatQuangCaoCommandValidator : AbstractValidator<CapNhatBangQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public CapNhatQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.CapNhatBangQuangCaoModel.DiaChi)
                .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_DIACHI_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_DIACHI_IS_NULL_OR_EMPTY);

            RuleFor(t => t.CapNhatBangQuangCaoModel.Phuong)
                .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_PHUONG_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_PHUONG_IS_NULL_OR_EMPTY);

            RuleFor(t => t.CapNhatBangQuangCaoModel.Quan)
                .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_QUAN_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_QUAN_IS_NULL_OR_EMPTY);

            RuleFor(t => t.CapNhatBangQuangCaoModel.DanhSachViTri)
                .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_VITRI_IS_NULL_OR_EMPTY)
                .Must((vitris) =>
                {
                    if (vitris.Count == 2)
                    {
                        return true;
                    }
                    return false;
                }).WithMessage(MessageBangQuangCao.BANGQUANGCAO_VITRI_INVALID);

            RuleFor(t => t.CapNhatBangQuangCaoModel.MaLoaiViTri)
                .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_MALOAIVITRI_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_MALOAIVITRI_IS_NULL_OR_EMPTY)
                .MustAsync(async (ma, canncellationToken) =>
                {
                    if (!string.IsNullOrEmpty(ma))
                    {
                        return await _dataContext.LoaiViTris.AnyAsync(t => t.Ma == ma, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageBangQuangCao.BANGQUANGCAO_MALOAIVITRI_NOT_EXISTS);

            RuleFor(t => t.CapNhatBangQuangCaoModel.MaHinhThucQuangCao)
                .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_MAHINHTHUC_QUANGCAO_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_MAHINHTHUC_QUANGCAO_IS_NULL_OR_EMPTY)
                .MustAsync(async (ma, canncellationToken) =>
                {
                    if (!string.IsNullOrEmpty(ma))
                    {
                        return await _dataContext.HinhThucQuangCaos.AnyAsync(t => t.Ma == ma, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageBangQuangCao.BANGQUANGCAO_MAHINHTHUC_QUANGCAO_NOT_EXISTS);

            RuleFor(t => t.CapNhatBangQuangCaoModel.MaLoaiBangQuangCao)
                .NotNull().WithMessage(MessageBangQuangCao.BANGQUANGCAO_MALOAI_BANGQUANGCAO_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageBangQuangCao.BANGQUANGCAO_MALOAI_BANGQUANGCAO_IS_NULL_OR_EMPTY)
                .MustAsync(async (ma, canncellationToken) =>
                {
                    if (!string.IsNullOrEmpty(ma))
                    {
                        return await _dataContext.LoaiBangQuangCaos.AnyAsync(t => t.Ma == ma, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageBangQuangCao.BANGQUANGCAO_MALOAI_BANGQUANGCAO_NOT_EXISTS);
        }
    }
}
