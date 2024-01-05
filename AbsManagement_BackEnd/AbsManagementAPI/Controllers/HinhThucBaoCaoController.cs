using AbsManagementAPI.Core.Authentication;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions.Common;
using AbsManagementAPI.Core.Models.CanBo;
using AbsManagementAPI.Core.Models.HinhThucBaoCao;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbsManagementAPI.Controllers
{
    [ApiController]
    [Route("api/hinhthucbaocao")]
    public class HinhThucBaoCaoController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public HinhThucBaoCaoController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }


        /// <summary>
        /// Danh sách hình thức quảng cáo
        /// </summary>
        [HttpGet()]
        public async Task<List<HinhThucBaoCaoModel>> List()
        => await _dataContext.HinhThucBaoCaos.ProjectTo<HinhThucBaoCaoModel>(_mapper.ConfigurationProvider).ToListAsync(CancellationToken.None);

        /// <summary>
        /// Cập nhật hình thức quảng cáo
        /// </summary>
        ///  <param name="id"></param>
        ///  <param name="capNhatHinhThucBaoCaoModel"></param>
        [HttpPost("{id}")]
        [Authorize]
        public async Task<string> Update(int id, CapNhatHinhThucBaoCaoModel capNhatHinhThucBaoCaoModel)
        {
            try
            {
                var detail = await _dataContext.HinhThucBaoCaos.FirstOrDefaultAsync(t => t.Id == id, CancellationToken.None);
                detail.Ten = capNhatHinhThucBaoCaoModel.Ten;
                detail.Ma = capNhatHinhThucBaoCaoModel.Ma;
                _dataContext.HinhThucBaoCaos.Update(detail);
                await _dataContext.SaveChangesAsync(CancellationToken.None);
                return "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                throw new CustomMessageException("Cập nhật thất bại");
            }
        }
        /// <summary>
        /// Tạo hình thức quảng cáo
        /// </summary>
        ///  <param name="themHinhThucBaoCaoModel"></param>
        [HttpPost()]
        [Authorize]
        public async Task<string> Create(ThemHinhThucBaoCaoModel themHinhThucBaoCaoModel)
        {
            try
            {
                var detail = new HinhThucBaoCaoEntity()
                {
                    Ten = themHinhThucBaoCaoModel.Ten,
                    Ma = themHinhThucBaoCaoModel.Ma
                };
                _dataContext.HinhThucBaoCaos.Add(detail);
                await _dataContext.SaveChangesAsync(CancellationToken.None);
                return "Thêm mới thành công";
            }
            catch (Exception ex)
            {
                throw new CustomMessageException("Thêm mới thất bại");
            }
        }
    }
}
