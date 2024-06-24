using System.Collections.Generic;
using ACG.Core.WinForm.Data.Static;
using QLMamNon.Entity.Form;
using QLMamNon.Properties;

namespace QLMamNon.Components.Data.Static
{
    public class NamHocData : IStaticData
    {
        #region IStaticData Members

        public object Retrieve()
        {
            List<NamHoc> namHocList = new List<NamHoc>();

            for (int i = Settings.Default.StartYearForDropDown; i < 2049; i++)
            {
                namHocList.Add(new NamHoc(i, i + 1));
            }

            return namHocList;
        }

        #endregion
    }
}
