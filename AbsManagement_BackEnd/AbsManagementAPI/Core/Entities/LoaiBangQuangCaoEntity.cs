using System.ComponentModel.DataAnnotations.Schema;

namespace AbsManagementAPI.Core.Entities
{
    public class LoaiBangQuangCaoEntity
    {
        public LoaiBangQuangCaoEntity()
        {
            BangQuangCaos = new HashSet<BangQuangCaoEntity>();
        }
        public string Ma { get; set; }

        public string Ten { get; set; }

        public virtual ICollection<BangQuangCaoEntity> BangQuangCaos { get; set; }
    }
}
