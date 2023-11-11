using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;

namespace AbsManagementAPI.Validations.BaoCaoViPham
{
    public class ThemBaoCaoViPhamCommandValidator : AbstractValidator<ThemBaoCaoViPhamCommand>
    {
        private readonly DataContext _dataContext;

        public ThemBaoCaoViPhamCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;


            RuleFor(t => t.ThemBaoCaoViPhamModel.HoTen)
                .NotNull().WithMessage(MessageBaoCaoViPham.BAOCAOVIPHAM_HOTEN_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageBaoCaoViPham.BAOCAOVIPHAM_HOTEN_IS_NULL_OR_EMPTY);

            RuleFor(t => t.ThemBaoCaoViPhamModel.SDT)
                .NotNull().WithMessage(MessageBaoCaoViPham.BAOCAOVIPHAM_SDT_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageBaoCaoViPham.BAOCAOVIPHAM_SDT_IS_NULL_OR_EMPTY);

            RuleFor(t => t.ThemBaoCaoViPhamModel.NoiDungXyLy)
                .NotNull().WithMessage(MessageBaoCaoViPham.BAOCAOVIPHAM_NOIDUNG_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageBaoCaoViPham.BAOCAOVIPHAM_NOIDUNG_IS_NULL_OR_EMPTY);

            RuleFor(t => t.ThemBaoCaoViPhamModel.ViTri)
                .NotNull().WithMessage(MessageBaoCaoViPham.BAOCAOVIPHAM_VITRI_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageBaoCaoViPham.BAOCAOVIPHAM_VITRI_IS_NULL_OR_EMPTY);
        }
    }
}
