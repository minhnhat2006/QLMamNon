using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLMamNon.Entity.Form
{
    public class NamHoc
    {
        public NamHoc(int fromYear, int toYear)
        {
            FromYear = fromYear;
            ToYear = toYear;
        }

        public int FromYear { get; set; }

        public int ToYear { get; set; }

        public string Text
        {
            get
            {
                return String.Format("{0} - {1}", FromYear, ToYear);
            }
        }
    }
}
