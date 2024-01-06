using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Command;
using AbsManagementAPI.Core.CQRS.Log.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.Log;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.LoaiViTri.CommandHandler
{
    public class XoaLoaiViTriCommandHandler : BaseHandler, IRequestHandler<XoaLoaiViTriCommand, string>
    {
        public XoaLoaiViTriCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaLoaiViTriCommand request, CancellationToken cancellationToken)
        {
            var loaiViTri = await _dataContext.LoaiViTris.FirstOrDefaultAsync(t => t.Id == request.XoaLoaiViTriModel.Id, cancellationToken);
            try
            {
                _dataContext.Remove(loaiViTri);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                        new ThemLogModel
                        {
                            Controller = "LoaiViTriController",
                            Method = "Delete",
                            FunctionName = "XoaLoaiVitri",
                            Status = "Success",
                            OleValue = JsonConvert.SerializeObject(loaiViTri),
                            NewValue = "",
                            Type = "Debug",
                            CreateDate = DateTime.Now,
                        }
                    });
                    return MessageSystem.DELETE_SUCCESS;
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "LoaiViTriController",
                        Method = "Delete",
                        FunctionName = "XoaLoaiVitri",
                        Status = "Fail",
                        OleValue = "",
                        NewValue = JsonConvert.SerializeObject(loaiViTri),
                        Type = "Debug",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.DELETE_FAIL);
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "LoaiViTriController",
                        Method = "Delete",
                        FunctionName = "XoaLoaiVitri",
                        Status = "Error",
                        OleValue = "",
                        NewValue = JsonConvert.SerializeObject(loaiViTri),
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.DELETE_FAIL, ex.Message);
            }
        }
    }
}
