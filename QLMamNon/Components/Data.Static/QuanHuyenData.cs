using System.Linq;
using ACG.Core.WinForm.Data.Static;
using QLMamNon.Dao;

namespace QLMamNon.Components.Data.Static
{
    public class QuanHuyenData : IStaticData
    {
        private qlmamnonEntities entities;

        public QuanHuyenData(qlmamnonEntities entities)
        {
            this.entities = entities;
        }

        #region IStaticData Members

        public object Retrieve()
        {
            return this.entities.quanhuyens.ToList();
        }

        #endregion
    }
}
