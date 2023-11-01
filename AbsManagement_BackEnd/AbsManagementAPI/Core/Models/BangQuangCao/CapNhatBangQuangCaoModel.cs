using AbsManagementAPI.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AbsManagementAPI.Core.Models.BangQuangCao
{
    public class CapNhatBangQuangCaoModel : ThemBangQuangCaoModel
    {
        public TrangThaiBangQuangCao TrangThai { get; set; }
        public DateTimeOffset? NgayCapNhat { get; set; }
    }
}
