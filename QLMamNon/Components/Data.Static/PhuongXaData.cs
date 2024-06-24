using ACG.Core.WinForm.Data.Static;
using QLMamNon.Dao;
using System.Linq;

namespace QLMamNon.Components.Data.Static
{
    public class PhuongXaData : IStaticData
    {
        private qlmamnonEntities entities;

        public PhuongXaData(qlmamnonEntities entities)
        {
            this.entities = entities;
        }

        #region IStaticData Members

        public object Retrieve()
        {
            return this.entities.phuongxas.ToList();
        }

        #endregion
    }
}
