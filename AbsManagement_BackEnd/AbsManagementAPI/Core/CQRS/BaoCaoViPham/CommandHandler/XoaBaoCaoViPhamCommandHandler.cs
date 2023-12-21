﻿using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.CommandHandler
{
    public class XoaBaoCaoViPhamCommandHandler : BaseHandler, IRequestHandler<XoaBaoCaoViPhamCommand, string>
    {
        public XoaBaoCaoViPhamCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaBaoCaoViPhamCommand request, CancellationToken cancellationToken)
        {
            var baoCaoViPham = await _dataContext.BaoCaoViPhams.FirstOrDefaultAsync(t => t.Id == request.XoaBaoCaoViPhamModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(baoCaoViPham);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    return MessageSystem.DELETE_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.DELETE_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.DELETE_FAIL, ex.Message);
            }
        }
    }
}
