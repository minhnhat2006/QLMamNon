using ACG.Core.WinForm.Data.Static;
using QLMamNon.Dao;
using System.Linq;

namespace QLMamNon.Components.Data.Static
{
    public class TruongData : IStaticData
    {
        private qlmamnonEntities entities;

        public TruongData(qlmamnonEntities entities)
        {
            this.entities = entities;
        }

        #region IStaticData Members

        public object Retrieve()
        {
            return this.entities.truongs.ToList();
        }

        #endregion
    }
}
