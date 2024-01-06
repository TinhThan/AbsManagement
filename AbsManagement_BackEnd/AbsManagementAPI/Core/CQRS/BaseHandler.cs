using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.PhieuCapPhepSuaQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Log.Command;
using AbsManagementAPI.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace AbsManagementAPI.Core.CQRS
{
    public class BaseHandler
    {
        protected readonly DataContext _dataContext;
        protected readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;
        public AuthInfo authInfo;
        public BaseHandler(IHttpContextAccessor context, DataContext dataContext, IMapper mapper)
        {
            _context = context;
            _dataContext = dataContext;
            _mapper = mapper;
            authInfo = HelperIdentity.GetUserContext(_context.HttpContext);
        }

        public async Task<string> AddLog(ThemLogCommand request)
        {
            //var log = _mapper.Map<LogEntity>(request.ThemLogModel);
            var log = new LogEntity
            {
                UserId = authInfo.Id,
                Controller = request.ThemLogModel.Controller,
                Method = request.ThemLogModel.Method,
                FunctionName = request.ThemLogModel.FunctionName,
                Status = request.ThemLogModel.Status,
                OleValue = request.ThemLogModel.OleValue,
                NewValue = request.ThemLogModel.NewValue,
                Type = request.ThemLogModel.Type,
                CreateDate = DateTime.Now,
            };
            //log.UserId = authInfo.Id;
            //log.CreateDate = DateTime.Now;
            try
            {
                await _dataContext.AddAsync(log);
                var resultThemMoi = await _dataContext.SaveChangesAsync();
                if (resultThemMoi > 0)
                {
                    return MessageSystem.ADD_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.ADD_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
