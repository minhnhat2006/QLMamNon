using ACG.Core.WinForm.Data.Static;
using QLMamNon.Dao;
using System.Linq;

namespace QLMamNon.Components.Data.Static
{
    public class KhoanThuData : IStaticData
    {
        private qlmamnonEntities entities;

        public KhoanThuData(qlmamnonEntities entities)
        {
            this.entities = entities;
        }

        #region IStaticData Members

        public object Retrieve()
        {
            return this.entities.khoanthus.ToList();
        }

        #endregion
    }
}
