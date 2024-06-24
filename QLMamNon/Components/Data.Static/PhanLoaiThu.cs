using ACG.Core.WinForm.Data.Static;
using QLMamNon.Dao;
using System.Linq;

namespace QLMamNon.Components.Data.Static
{
    public class PhanLoaiThu : IStaticData
    {
        private qlmamnonEntities entities;

        public PhanLoaiThu(qlmamnonEntities entities)
        {
            this.entities = entities;
        }

        #region IStaticData Members

        public object Retrieve()
        {
            return this.entities.phanloaithus.ToList();
        }

        #endregion
    }
}
