using System.ComponentModel.DataAnnotations.Schema;

namespace AbsManagementAPI.Core.Entities
{
    public class HinhThucBaoCaoEntity
    {
        public HinhThucBaoCaoEntity()
        {
            BaoCaoViPhams = new HashSet<BaoCaoViPhamEntity>();
        }
        public string Ma { get; set; }

        public string Ten { get; set; }

        public virtual ICollection<BaoCaoViPhamEntity> BaoCaoViPhams { get; set; }
    }
}
