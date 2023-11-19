﻿using AbsManagementAPI.Core.Models.BaoCaoViPham;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BaoCaoViPham.Query
{
    public class DanhSachBaoCaoViPhamQuery : IRequest<IEnumerable<BaoCaoViPhamModel>>
    {
    }
}