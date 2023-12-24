using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.HinhThucQuangCao
{
    public class HinhThucQuangCaoModel
    {
        public int Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }

        public virtual ICollection<DiemDatQuangCaoEntity> DiemDatQuangCaos { get; set; }
    }
}
