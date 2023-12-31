﻿using AbsManagementAPI.Entities;

namespace AbsManagementAPI.Core.Entities
{
    public class DiemDatQuangCaoEntity
    {
        public DiemDatQuangCaoEntity() {

            BangQuangCaos = new HashSet<BangQuangCaoEntity>();
            BaoCaoViPhams = new HashSet<BaoCaoViPhamEntity>();
        }

        public int Id { get;set; }
        public string DiaChi { get; set; }
        public string Phuong { get; set; }
        public string Quan { get; set; }
        public string ViTri { get; set; }
        public int IdLoaiViTri { get; set; }
        public int IdHinhThucQuangCao { get; set; }
        public string DanhSachHinhAnh { get; set; }
        public string IdTinhTrang { get; set; }

        public virtual LoaiViTriEntity LoaiViTri { get; set; }
        public virtual HinhThucQuangCaoEntity HinhThucQuangCao { get; set; }
        public virtual ICollection<BangQuangCaoEntity> BangQuangCaos { get; set; }
        public virtual ICollection<BaoCaoViPhamEntity> BaoCaoViPhams { get; set; }
        public virtual ICollection<PhieuCapPhepSuaQuangCaoEntity> PhieuCapPhepSuaQuangCaos { get; set; }

    }
}
