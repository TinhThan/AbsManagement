using AbsManagementAPI.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AbsManagementAPI.Core.Models.BangQuangCao
{
    public class CapNhatBangQuangCaoModel : ThemBangQuangCaoModel
    {
        public DateTimeOffset NgayCapNhat { get; set; }
        public string IdTinhTrang { get; set; }
    }
}
