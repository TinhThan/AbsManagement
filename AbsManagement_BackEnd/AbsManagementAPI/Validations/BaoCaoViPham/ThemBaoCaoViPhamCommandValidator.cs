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
        }
    }
}
