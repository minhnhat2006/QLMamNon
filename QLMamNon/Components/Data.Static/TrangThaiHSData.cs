using System.Collections.Generic;
using ACG.Core.WinForm.Data.Static;
using QLMamNon.Entity.Form;
using QLMamNon.Properties;

namespace QLMamNon.Components.Data.Static
{
    public class TrangThaiHSData : IStaticData
    {
        #region IStaticData Members

        public object Retrieve()
        {
            List<KeyValuePair> trangThaiList = new List<KeyValuePair>();
            trangThaiList.Add(new KeyValuePair(0, "Đang học"));
            trangThaiList.Add(new KeyValuePair(1, "Thôi học"));

            return trangThaiList;
        }

        #endregion
    }
}
