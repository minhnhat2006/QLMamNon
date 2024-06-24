using ACG.Core.WinForm.Data.Static;
using QLMamNon.Dao;
using System.Linq;

namespace QLMamNon.Components.Data.Static
{
    public class KhoiData : IStaticData
    {
        private qlmamnonEntities entities;

        public KhoiData(qlmamnonEntities entities)
        {
            this.entities = entities;
        }

        #region IStaticData Members

        public object Retrieve()
        {
            return this.entities.khois.ToList();
        }

        #endregion
    }
}
