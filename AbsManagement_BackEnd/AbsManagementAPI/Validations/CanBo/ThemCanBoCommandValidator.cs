using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;

namespace AbsManagementAPI.Validations.CanBo
{
    public class ThemCanBoCommandValidator : AbstractValidator<ThemMoiCanBoCommand>
    {
        private readonly DataContext _dataContext;

        public ThemCanBoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.ThemMoiCanBoModel.HoTen)
                .NotNull().WithMessage(MessageCanBo.CANBO_HOTEN_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageCanBo.CANBO_HOTEN_IS_NULL_OR_EMPTY);

            RuleFor(t => t.ThemMoiCanBoModel.Email)
                .NotNull().WithMessage(MessageCanBo.CANBO_EMAIL_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageCanBo.CANBO_EMAIL_IS_NULL_OR_EMPTY);

            RuleFor(t => t.ThemMoiCanBoModel.TaiKhoan)
                .NotNull().WithMessage(MessageCanBo.CANBO_TAIKHOAN_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageCanBo.CANBO_TAIKHOAN_IS_NULL_OR_EMPTY);

            RuleFor(t => t.ThemMoiCanBoModel.MatKhau)
                .NotNull().WithMessage(MessageCanBo.CANBO_MATKHAU_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageCanBo.CANBO_MATKHAU_IS_NULL_OR_EMPTY);

            RuleFor(t => t.ThemMoiCanBoModel.Role)
                .NotNull().WithMessage(MessageCanBo.CANBO_ROLE_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageCanBo.CANBO_ROLE_IS_NULL_OR_EMPTY);
        }
    }
}
