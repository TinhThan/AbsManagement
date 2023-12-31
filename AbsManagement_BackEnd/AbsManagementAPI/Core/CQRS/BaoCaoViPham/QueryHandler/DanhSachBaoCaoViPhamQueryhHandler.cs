﻿using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Query;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.BaoCaoViPham;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.QueryHandler
{
    public class DanhSachBaoCaoViPhamQueryhHandler : BaseHandler, IRequestHandler<DanhSachBaoCaoViPhamQuery, List<BaoCaoViPhamModel>>
    {
        public DanhSachBaoCaoViPhamQueryhHandler(IHttpContextAccessor context, DataContext dataContext, IMapper mapper) : base(context, dataContext, mapper)
        {
        }

        public async Task<List<BaoCaoViPhamModel>> Handle(DanhSachBaoCaoViPhamQuery request, CancellationToken cancellationToken)
        => await _dataContext.BaoCaoViPhams.Include(t=>t.CanBoXuLy).Where(t=> (string.IsNullOrEmpty(request.addressSearchModel.Quan) || t.Quan == request.addressSearchModel.Quan) &&
                                (string.IsNullOrEmpty(request.addressSearchModel.Phuong) || t.Phuong == request.addressSearchModel.Phuong))
            .ProjectTo<BaoCaoViPhamModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
