using System.ComponentModel.DataAnnotations.Schema;

namespace AbsManagementAPI.Core.Entities
{
    public class LoaiViTriEntity
    {
        public LoaiViTriEntity()
        {
            DiemDatQuangCaos = new HashSet<DiemDatQuangCaoEntity>();
        }
        public int Id { get; set; }

        public string Ma { get; set; }

        public string Ten { get; set; }

        public virtual ICollection<DiemDatQuangCaoEntity>  DiemDatQuangCaos { get; set; }
    }
}
