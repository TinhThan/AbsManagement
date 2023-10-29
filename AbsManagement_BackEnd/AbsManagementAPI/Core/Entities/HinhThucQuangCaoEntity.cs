using System.ComponentModel.DataAnnotations.Schema;

namespace AbsManagementAPI.Core.Entities
{
    public class HinhThucQuangCaoEntity
    {
        public HinhThucQuangCaoEntity()
        {
            BangQuangCaos = new HashSet<BangQuangCaoEntity>();
        }
        public string Ma { get; set; }

        public string Ten { get; set; }

        public virtual ICollection<BangQuangCaoEntity> BangQuangCaos { get; set; }
    }
}
