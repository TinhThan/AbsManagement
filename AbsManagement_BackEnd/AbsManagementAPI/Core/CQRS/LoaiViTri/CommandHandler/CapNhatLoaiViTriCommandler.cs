using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Command;
using AbsManagementAPI.Core.CQRS.Log;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Log.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.LoaiViTri.CommandHandler
{
    public class CapNhatLoaiViTriCommandler : BaseHandler, IRequestHandler<CapNhatLoaiViTriCommand, string>
    {
        public static string DataOld = "";
        public CapNhatLoaiViTriCommandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }
        public async Task<string> Handle(CapNhatLoaiViTriCommand request, CancellationToken cancellationToken)
        {
            var loaiViTri = await _dataContext.LoaiViTris.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (loaiViTri == null)
            {
                throw new CustomMessageException(MessageSystem.DATA_INVALID);

            }
            try
            {
                DataOld = JsonConvert.SerializeObject(loaiViTri);
                var loaiViTriCapNhat = _mapper.Map(request.CapNhatLoaiViTriModel, loaiViTri);

                _dataContext.Update(loaiViTriCapNhat);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                        new ThemLogModel
                        {
                            Controller = "LoaiViTriController",
                            Method = "Update",
                            FunctionName = "CapNhatLoaiVitri",
                            Status = "Success",
                            OleValue = DataOld,
                            NewValue = JsonConvert.SerializeObject(loaiViTriCapNhat),
                            Type = "Debug",
                            CreateDate = DateTime.Now,
                        }
                    });
                    return MessageSystem.UPDATE_SUCCESS;
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                       new ThemLogModel
                       {
                           Controller = "LoaiViTriController",
                           Method = "Update",
                           FunctionName = "CapNhatLoaiVitri",
                           Status = "Fail",
                           OleValue = DataOld,
                           NewValue = JsonConvert.SerializeObject(loaiViTriCapNhat),
                           Type = "Debug",
                           CreateDate = DateTime.Now,
                       }
                });
                throw new CustomMessageException(MessageSystem.UPDATE_FAIL);
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                   new ThemLogModel
                   {
                       Controller = "LoaiViTriController",
                       Method = "Update",
                       FunctionName = "CapNhatLoaiVitri",
                       Status = "Error",
                       OleValue = JsonConvert.SerializeObject(loaiViTri),
                       NewValue = "",
                       Type = "Error",
                       CreateDate = DateTime.Now,
                   }
                            });
                throw new CustomMessageException(MessageSystem.UPDATE_FAIL, ex.Message);
            }
        }
    }
}
