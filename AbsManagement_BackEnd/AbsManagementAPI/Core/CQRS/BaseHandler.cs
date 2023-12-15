using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    }
}
