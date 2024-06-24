using ACG.Core.WinForm.Data.Static;
using QLMamNon.Dao;
using System.Linq;

namespace QLMamNon.Components.Data.Static
{
    public class PhanLoaiChi : IStaticData
    {
        private qlmamnonEntities entities;

        public PhanLoaiChi(qlmamnonEntities entities)
        {
            this.entities = entities;
        }

        #region IStaticData Members

        public object Retrieve()
        {
            return this.entities.phanloaichis.ToList();
        }

        #endregion
    }
}
