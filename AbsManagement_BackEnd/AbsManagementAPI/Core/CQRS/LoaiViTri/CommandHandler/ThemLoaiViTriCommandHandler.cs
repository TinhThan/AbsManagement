using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BaoCaoViPham.Command;
using AbsManagementAPI.Core.CQRS.LoaiViTri.Command;
using AbsManagementAPI.Core.CQRS.Log;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Log.Command;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

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
                    await AddLog(new ThemLogCommand 
                    { ThemLogModel = 
                        new ThemLogModel 
                        { 
                            Controller = "LoaiViTriController",
                            Method = "Create",
                            FunctionName ="ThemLoaiVitri",
                            Status = "Success",
                            OleValue = "",
                            NewValue = JsonConvert.SerializeObject(loaiViTri),
                            Type ="Debug",
                            CreateDate = DateTime.Now,
                        }           
                    });
                    return MessageSystem.ADD_SUCCESS;
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "LoaiViTriController",
                        Method = "Create",
                        FunctionName = "ThemLoaiVitri",
                        Status ="Fail",
                        OleValue = "",
                        NewValue = JsonConvert.SerializeObject(loaiViTri),
                        Type = "Debug",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.ADD_FAIL);
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                    new ThemLogModel
                    {
                        Controller = "LoaiViTriController",
                        Method = "Create",
                        FunctionName = "ThemLoaiVitri",
                        Status = "Error",
                        OleValue = "",
                        NewValue = JsonConvert.SerializeObject(loaiViTri),
                        Type = "Error",
                        CreateDate = DateTime.Now,
                    }
                });
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
