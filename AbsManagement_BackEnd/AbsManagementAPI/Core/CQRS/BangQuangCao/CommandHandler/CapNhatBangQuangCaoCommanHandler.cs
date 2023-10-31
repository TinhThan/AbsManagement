using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.CQRS.BangQuangCao.Command;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AutoMapper;
using MediatR;

namespace AbsManagementAPI.Core.CQRS.BangQuangCao.CommandHandler
{
    public class CapNhatBangQuangCaoCommanHandler : BaseHandler, IRequestHandler<CapNhatBangQuangCaoCommand, string>
    {
        public CapNhatBangQuangCaoCommanHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatBangQuangCaoCommand request, CancellationToken cancellationToken)
        {
            var capNhat = _mapper.Map<BangQuangCaoEntity>(request.CapNhatBangQuangCaoModel);
            try
            {
                var thongTinBangBaoCap = await _dataContext.BangQuangCaos.FindAsync(capNhat.Id);
                if (!string.IsNullOrEmpty(thongTinBangBaoCap?.Id.ToString()))
                {
                    return MessageSystem.ADD_FAIL;
                }
                thongTinBangBaoCap.DiaChi = capNhat.DiaChi;
                thongTinBangBaoCap.Phuong = capNhat.Phuong;
                thongTinBangBaoCap.Quan = capNhat.Quan; 
                thongTinBangBaoCap.ViTri = capNhat.ViTri;
                thongTinBangBaoCap.DanhSachHinhAnh = capNhat.DanhSachHinhAnh;
                thongTinBangBaoCap.MaLoaiViTri = capNhat.MaLoaiViTri;
                thongTinBangBaoCap.MaHinhThucQuangCao = capNhat.MaHinhThucQuangCao;
                thongTinBangBaoCap.MaLoaiBangQuangCao = capNhat.MaLoaiBangQuangCao;
                thongTinBangBaoCap.KichThuoc = capNhat.KichThuoc;
                thongTinBangBaoCap.NgayHetHan = (DateTimeOffset)(capNhat.NgayHetHan);
                thongTinBangBaoCap.NgayCapNhat = DateTime.Now;
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
