using ACG.Core.WinForm.Data.Static;
using QLMamNon.Dao;
using System.Linq;

namespace QLMamNon.Components.Data.Static
{
    public class BangTinhPhi : IStaticData
    {
        private qlmamnonEntities entities;

        public BangTinhPhi(qlmamnonEntities entities)
        {
            this.entities = entities;
        }

        #region IStaticData Members

        public object Retrieve()
        {
            return entities.bangtinhphis.ToList();
        }

        #endregion
    }
}
