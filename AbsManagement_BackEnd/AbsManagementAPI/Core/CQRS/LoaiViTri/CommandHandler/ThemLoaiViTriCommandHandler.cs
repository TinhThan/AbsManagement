﻿using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.LoaiViTri.CommandHandler
{
    public class ThemLoaiViTriCommandHandler : BaseHandler, IRequestHandler<ThemLoaiViTriCommand, string>
    {
        public ThemLoaiViTriCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }
        public async Task<string> Handle(ThemLoaiViTriCommand request, CancellationToken cancellationToken)
        {
            var loaiViTri = _mapper.Map<LoaiViTriEntity>(request.ThemLoaiViTriModel);

            try
            {
                await _dataContext.AddAsync(loaiViTri);
                var resultThemMoi = await _dataContext.SaveChangesAsync();
                if (resultThemMoi > 0)
                {
                    return MessageSystem.ADD_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.ADD_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}