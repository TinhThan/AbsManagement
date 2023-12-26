namespace AbsManagementAPI.Core.Entities
{
    public class BangQuangCaoEntity
    {

        public BangQuangCaoEntity()
        {
            BaoCaoViPhams = new HashSet<BaoCaoViPhamEntity>();
        }

        public int Id { get; set; }
        public int IdDiemDatQuangCao { get; set; }
        public int IdLoaiBangQuangCao { get; set; }
        public string KichThuoc { get; set; }
        public string DanhSachHinhAnh { get; set; }
        public DateTimeOffset NgayHetHan { get; set; }
        public string IdTinhTrang { get; set; }
        public DateTimeOffset NgayCapNhat { get; set; }

        public virtual DiemDatQuangCaoEntity DiemDatQuangCao { get; set; }
        public virtual LoaiBangQuangCaoEntity LoaiBangQuangCao { get; set; }
        public virtual ICollection<BaoCaoViPhamEntity> BaoCaoViPhams { get; set; }
    }
}
