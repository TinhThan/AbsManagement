using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Query;
using AbsManagementAPI.Core.CQRS.HinhThucBangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.HinhThucQuangCao
{

    public class ThemHinhThucQuangCaoCommandValidator : AbstractValidator<ThemHinhThucQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public ThemHinhThucQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.ThemHinhThucQuangCaoModel.Ma)
            .GreaterThan("").WithMessage(MessageHinhThucQuangCao.HINHTHUCQUANGCAO_MA_IS_NULL_OR_EMPTY);
            RuleFor(t => t.ThemHinhThucQuangCaoModel.Ten)
               .GreaterThan("").WithMessage(MessageHinhThucQuangCao.HINHTHUCQUANGCAO_TEN_IS_NULL_OR_EMPTY);


        }
    }
}
