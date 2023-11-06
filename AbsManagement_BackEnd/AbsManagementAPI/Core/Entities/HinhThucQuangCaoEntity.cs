using System.ComponentModel.DataAnnotations.Schema;

namespace AbsManagementAPI.Core.Entities
{
    public class HinhThucQuangCaoEntity
    {
        public HinhThucQuangCaoEntity()
        {
            DiemDatQuangCaos = new HashSet<DiemDatQuangCaoEntity>();
            ChiTietPhieuChinhSuaDiemDatQuangCaos = new HashSet<ChiTietPhieuChinhSuaDiemDatQuangCaoEntity>();
        }
        public int Id { get; set; }
        public string Ma { get; set; }

        public string Ten { get; set; }

        public virtual ICollection<DiemDatQuangCaoEntity> DiemDatQuangCaos { get; set; }
        public virtual ICollection<ChiTietPhieuChinhSuaDiemDatQuangCaoEntity> ChiTietPhieuChinhSuaDiemDatQuangCaos { get;set; }
    }
}
