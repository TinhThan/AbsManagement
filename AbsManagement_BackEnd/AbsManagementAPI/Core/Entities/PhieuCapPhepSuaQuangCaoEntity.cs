using AbsManagementAPI.Core.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace AbsManagementAPI.Entities
{
    public class PhieuCapPhepSuaQuangCaoEntity
    {
        public int Id { get; set; }
        public int IdDiemDat { get; set; }
        public int IdBangQuangCao { get; set; }
        public string NoiDung { get; set; }
        public DateTimeOffset? NgayGui { get; set; }
        public string TinhTrang { get; set; }

        public virtual BangQuangCaoEntity BangQuangCao { get; set; }
        public virtual DiemDatQuangCaoEntity DiemDatQuangCao { get; set; }
    }
}
