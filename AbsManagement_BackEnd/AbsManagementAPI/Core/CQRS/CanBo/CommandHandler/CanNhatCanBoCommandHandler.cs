using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.CanBo.Command;
using AbsManagementAPI.Core.CQRS.Log;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Log.Command;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AbsManagementAPI.Core.CQRS.CanBo.CommandHandler
{
    public class CanNhatCanBoCommandHandler : BaseHandler, IRequestHandler<CanNhatCanBoCommand, string>
    {
        public static string DataOld = "";
        public CanNhatCanBoCommandHandler(IHttpContextAccessor httpContextAccessor, DataContext dataContext, IMapper mapper) : base(httpContextAccessor, dataContext, mapper)
        {
        }

        public async Task<string> Handle(CanNhatCanBoCommand request, CancellationToken cancellationToken)
        {
            var canBo = await _dataContext.CanBos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (canBo == null)
            {
                throw new CustomMessageException(MessageSystem.VERSION_UPDATE, MessageSystem.VERSION_UPDATE);
            }
            DataOld = JsonConvert.SerializeObject(canBo);
            var canBoCapNhat = _mapper.Map(request.CapNhatCanBoModel, canBo);
            try
            {
                _dataContext.CanBos.Update(canBoCapNhat);
                var result = await _dataContext.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    await AddLog(new ThemLogCommand
                    {
                        ThemLogModel =
                      new ThemLogModel
                      {
                          Controller = "CanBoController",
                          Method = "Update",
                          FunctionName = "CapNhatCanBo",
                          Status = "Success",
                          OleValue = DataOld,
                          NewValue = JsonConvert.SerializeObject(canBoCapNhat),
                          Type = "Debug",
                          CreateDate = DateTime.Now,
                      }
                    });
                    return "Cập nhật cán bộ thành công";
                }
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                     new ThemLogModel
                     {
                         Controller = "CanBoController",
                         Method = "Update",
                         FunctionName = "CapNhatCanBo",
                         Status = "Fail",
                         OleValue = DataOld,
                         NewValue = JsonConvert.SerializeObject(canBoCapNhat),
                         Type = "Debug",
                         CreateDate = DateTime.Now,
                     }
                });
                return "Cập nhật cán bộ thất bại";
            }
            catch (Exception ex)
            {
                await AddLog(new ThemLogCommand
                {
                    ThemLogModel =
                     new ThemLogModel
                     {
                         Controller = "CanBoController",
                         Method = "Update",
                         FunctionName = "CapNhatCanBo",
                         Status = "Error",
                         OleValue = DataOld,
                         NewValue = JsonConvert.SerializeObject(canBo),
                         Type = "Error",
                         CreateDate = DateTime.Now,
                     }
                });
                throw new CustomMessageException(ex.Message);
            }
        }
    }
}
