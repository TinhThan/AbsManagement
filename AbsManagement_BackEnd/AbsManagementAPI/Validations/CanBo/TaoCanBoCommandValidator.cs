using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.CanBo
{
    public class TaoCanBoCommandValidator : AbstractValidator<TaoCanBoCommand>
    {
        private readonly DataContext _context;
        public TaoCanBoCommandValidator(DataContext dataContext) {
            _context = dataContext;

            RuleFor(t => t.TaoCanBoModel.Email)
                .NotEmpty().WithMessage("Email không được rỗng.")
                .MustAsync(async (email, cancellationToken) =>
                {
                    if (string.IsNullOrEmpty(email))
                    {
                        return true;
                    }
                    return !(await _context.CanBos.AnyAsync(t => t.Email == email, cancellationToken));
                }).WithMessage("Email đã tồn tại.");

            RuleFor(t => t.TaoCanBoModel.HoTen)
                .NotEmpty().WithMessage("Họ tên không được rỗng.");

            RuleFor(t => t.TaoCanBoModel.SoDienThoai)
                .NotEmpty().WithMessage("Số điện thoại không được rỗng.");

            RuleFor(t => t.TaoCanBoModel.NgaySinh)
                .NotEmpty().WithMessage("Ngày sinh không được rỗng.");

            RuleFor(t => t.TaoCanBoModel.Role)
                .NotEmpty().WithMessage("Chức vụ không được rỗng.");

            RuleFor(t => t.TaoCanBoModel.MatKhau)
                .NotEmpty().WithMessage("Mật khẩu không được rỗng.");
        }
    }
}
