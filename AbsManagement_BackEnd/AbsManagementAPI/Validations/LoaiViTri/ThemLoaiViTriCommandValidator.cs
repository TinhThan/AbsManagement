using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;

namespace AbsManagementAPI.Validations.LoaiViTri
{
    public class ThemLoaiViTriCommandValidator : AbstractValidator<ThemLoaiViTriCommand>
    {
        private readonly DataContext _dataContext;
        public ThemLoaiViTriCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;


            RuleFor(t => t.ThemLoaiViTriModel.Ma)
                .NotNull().WithMessage(MessageLoaiViTri.LOAIVITRI_MA_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageLoaiViTri.LOAIVITRI_MA_IS_NULL_OR_EMPTY);

            RuleFor(t => t.ThemLoaiViTriModel.Ten)
                .NotNull().WithMessage(MessageLoaiViTri.LOAIVITRI_TEN_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageLoaiViTri.LOAIVITRI_TEN_IS_NULL_OR_EMPTY);
        }
    }
}
