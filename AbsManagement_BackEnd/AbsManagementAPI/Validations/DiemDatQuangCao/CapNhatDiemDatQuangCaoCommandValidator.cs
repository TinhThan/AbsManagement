using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.DiemDatQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Validations.DiemDatQuangCao
{
    public class CapNhatDiemDatQuangCaoCommandValidator : AbstractValidator<CapNhatDiemDatQuangCaoCommand>
    {
        private readonly DataContext _dataContext;

        public CapNhatDiemDatQuangCaoCommandValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage(MessageDiemDatQuangCao.ID_DIEMDATQUANGCAO_NOT_EMPTY)
                .MustAsync(async (id, canncellationToken) =>
                {
                    if (id != 0)
                    {
                        return await _dataContext.DiemDatQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                    }
                    return true;
                }).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_NOT_EXISTS);

            RuleFor(t => t.CapNhatDiemDatQuangCaoModel.IdLoaiViTri)
                 .GreaterThan(0).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_IDLOAIVITRI_IS_NULL_OR_EMPTY)
                 .MustAsync(async (id, canncellationToken) =>
                 {
                     if (id > 0)
                     {
                         return await _dataContext.LoaiViTris.AnyAsync(t => t.Id == id, canncellationToken);
                     }
                     return true;
                 }).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_IDLOAIVITRI_NOT_EXISTS);

            RuleFor(t => t.CapNhatDiemDatQuangCaoModel.IdHinhThucQuangCao)
               .GreaterThan(0).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_IDHINHTHUC_QUANGCAO_IS_NULL_OR_EMPTY)
               .MustAsync(async (id, canncellationToken) =>
               {
                   if (id > 0)
                   {
                       return await _dataContext.DiemDatQuangCaos.AnyAsync(t => t.Id == id, canncellationToken);
                   }
                   return true;
               }).WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_IDHINHTHUC_QUANGCAO_NOT_EXISTS);

            RuleFor(t => t.CapNhatDiemDatQuangCaoModel.DiaChi)
               .GreaterThan("").WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_DIACHI_IS_NULL_OR_EMPTY);
            RuleFor(t => t.CapNhatDiemDatQuangCaoModel.Phuong)
               .GreaterThan("").WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_PHUONG_IS_NULL_OR_EMPTY);
            RuleFor(t => t.CapNhatDiemDatQuangCaoModel.Quan)
               .GreaterThan("").WithMessage(MessageDiemDatQuangCao.DIEMDATQUANGCAO_QUAN_IS_NULL_OR_EMPTY);



        }
    }
}
