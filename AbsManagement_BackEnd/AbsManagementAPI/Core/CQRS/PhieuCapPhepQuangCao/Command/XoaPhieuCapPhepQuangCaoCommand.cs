﻿using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command
{
    public class XoaPhieuCapPhepQuangCaoCommand : IRequest<string>
    {
        public XoaPhieuCapPhepQuangCaoModel XoaPhieuCapPhepQuangCao { get; set; }

    }
}
