﻿using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Models.PhieuCapPhepQuangCao;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.PhieuCapPhepQuangCao.Command
{
    public class DuyetPhieuCapPhepQuangCaoCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
