using AbsManagementAPI.Core.Entities;

namespace AbsManagementAPI.Core.Models.LoaiBangQuangCao
{
    public class LoaiBangQuangCaoModel
    {
        public int Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }

        public virtual ICollection<BangQuangCaoEntity> BangQuangCaos { get; set; }
    }
}
