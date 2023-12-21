using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;

namespace AbsManagementAPI.Validations.LoaiViTri
{
    public class CapNhatLoaiViTriCommandValidator : AbstractValidator<CapNhatLoaiViTriCommand>
    {
        private readonly DataContext _dataContext;
        public CapNhatLoaiViTriCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;


            RuleFor(t => t.CapNhatLoaiViTriModel.Ma)
                .NotNull().WithMessage(MessageLoaiViTri.LOAIVITRI_MA_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageLoaiViTri.LOAIVITRI_MA_IS_NULL_OR_EMPTY);

            RuleFor(t => t.CapNhatLoaiViTriModel.Ten)
                .NotNull().WithMessage(MessageLoaiViTri.LOAIVITRI_TEN_IS_NULL_OR_EMPTY)
                .NotEmpty().WithMessage(MessageLoaiViTri.LOAIVITRI_TEN_IS_NULL_OR_EMPTY);
        }

    }
}
