using ACG.Core.WinForm.Data.Static;
using QLMamNon.Dao;
using System.Linq;

namespace QLMamNon.Components.Data.Static
{
    public class TinhThanhPhoData : IStaticData
    {
        private qlmamnonEntities entities;

        public TinhThanhPhoData(qlmamnonEntities entities)
        {
            this.entities = entities;
        }

        #region IStaticData Members

        public object Retrieve()
        {
            return entities.thanhphoes.ToList();
        }

        #endregion
    }
}
