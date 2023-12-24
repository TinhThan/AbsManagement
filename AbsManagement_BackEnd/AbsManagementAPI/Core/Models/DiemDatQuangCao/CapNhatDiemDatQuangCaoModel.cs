using AbsManagementAPI.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AbsManagementAPI.Core.Models.DiemDatQuangCao
{
    public class CapNhatDiemDatQuangCaoModel : ThemDiemDatQuangCaoModel
    {
        public DateTimeOffset NgayCapNhat { get; set; }
    }
}
