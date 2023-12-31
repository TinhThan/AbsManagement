﻿using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.Logged;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Logged.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class GuiBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<GuiBangQuangCaoCommand, string>
    {
        public static string DataOld = "";

        public GuiBangQuangCaoCommanHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(GuiBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bangQuangCao = await _dataContext.BangQuangCaos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
                DataOld = JsonConvert.SerializeObject(bangQuangCao);

                var phieucapphep = new PhieuCapPhepQuangCaoEntity();
                phieucapphep.IdBangQuangCao = request.Id;
                phieucapphep.IdTinhTrang = "ChoDuyet";
                phieucapphep.IdCanBoGui = authInfo.Id;
                phieucapphep.NgayGui = DateTimeOffset.Now;

                bangQuangCao.IdTinhTrang = "ChoCapPhep";
                var jsonBangQuangCao = JsonConvert.SerializeObject(bangQuangCao);
                await _dataContext.AddAsync(phieucapphep);
                _dataContext.Update(bangQuangCao);

                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                     new ThemLogModel
                     {
                         Controller = "BangQuangCaoController",
                         Method = "Update",
                         FunctionName = "CapNhatGuiBangQuangCao",
                         Status = "Success",
                         OleValue = DataOld,
                         NewValue = jsonBangQuangCao,
                         Type = "Debug",
                         CreateDate = DateTime.Now,
                     }
                    });
                    return MessageSystem.APPROVING_SUCCESS;
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "BangQuangCaoController",
                        Method = "Update",
                        FunctionName = "CapNhatGuiBangQuangCao",
                        Status = "Fail",
                        OleValue = DataOld,
                        NewValue = jsonBangQuangCao,
                        Type = "Debug",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.APPROVING_SUCCESS);
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "BangQuangCaoController",
                        Method = "Update",
                        FunctionName = "CapNhatGuiBangQuangCao",
                        Status = "Error",
                        OleValue = DataOld,
                        NewValue = "",
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.APPROVING_FAIL, ex.Message);
            }
        }
    }
}

