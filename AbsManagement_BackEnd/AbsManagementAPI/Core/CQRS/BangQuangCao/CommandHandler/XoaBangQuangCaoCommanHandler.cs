using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class XoaBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<XoaBangQuangCaoCommand, string>
    {
        public XoaBangQuangCaoCommanHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<string> Handle(XoaBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var bangQuangCao= _mapper.Map<BangQuangCaoEntity>(request.XoaBangQuangCaoModel);

            try
            {
                var thongTinBangBaoCap = await _dataContext.BangQuangCaos.FindAsync(bangQuangCao.Id);
                if(!string.IsNullOrEmpty(thongTinBangBaoCap?.Id.ToString()))
                {
                    return MessageSystem.ADD_FAIL;
                }
                thongTinBangBaoCap.TrangThai = TrangThaiBangQuangCao.DAQUYHOACH;
                _dataContext.Update(thongTinBangBaoCap);
                var resultCapNhat = await _dataContext.SaveChangesAsync();
                if (resultCapNhat > 0)
                {
                    return MessageSystem.UPDATE_SUCCESS;
                }
                throw new CustomMessageException(MessageSystem.ADD_FAIL);
            }
            catch (Exception ex)
            {
                throw new CustomMessageException(MessageSystem.UPDATE_SUCCESS, ex.Message);
            }
        }
    }
}
