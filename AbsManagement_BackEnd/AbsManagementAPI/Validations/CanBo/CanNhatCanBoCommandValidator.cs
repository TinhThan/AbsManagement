using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.CanBo
{
    public class CanNhatCanBoCommandValidator : AbstractValidator<CanNhatCanBoCommand>
    {
        public CanNhatCanBoCommandValidator()
        {
            RuleFor(t => t.CapNhatCanBoModel.HoTen)
                .NotEmpty().WithMessage("Họ tên không được rỗng.");

            RuleFor(t => t.CapNhatCanBoModel.SoDienThoai)
                .NotEmpty().WithMessage("Số điện thoại không được rỗng.");

            RuleFor(t => t.CapNhatCanBoModel.NgaySinh)
                .NotEmpty().WithMessage("Ngày sinh không được rỗng.");

            RuleFor(t => t.CapNhatCanBoModel.Role)
                .Must((role) =>
                {
                    if (RoleSystem.RoleCanBos.Contains(role))
                    {
                        return true;
                    }
                    return false;
                }).WithMessage("Quyền cán bộ không hợp lệ.")
                .NotEmpty().WithMessage("Chức vụ không được rỗng.");
        }
    }
}
