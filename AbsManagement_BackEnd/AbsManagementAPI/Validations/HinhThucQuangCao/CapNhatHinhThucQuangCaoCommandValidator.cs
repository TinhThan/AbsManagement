using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.HinhThucQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.HinhThucQuangCao
{
    public class CapNhatHinhThucQuangCaoCommandValidator : AbstractValidator<CapNhatHinhThucQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public CapNhatHinhThucQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessageHinhThucQuangCao.ID_HINHTHUCQUANGCAO_NOT_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id != 0)
                    {
                        return await _dataContext.HinhThucQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageHinhThucQuangCao.HINHTHUCQUANGCAO_NOT_EXISTS);

           
            RuleFor(t => t.CapNhatHinhThucQuangCaoModel.Ma)
               .GreaterThan("").WithMessage(MessageHinhThucQuangCao.HINHTHUCQUANGCAO_MA_IS_NULL_OR_EMPTY);
            RuleFor(t => t.CapNhatHinhThucQuangCaoModel.Ten)
               .GreaterThan("").WithMessage(MessageHinhThucQuangCao.HINHTHUCQUANGCAO_TEN_IS_NULL_OR_EMPTY);

        }
    }
}
