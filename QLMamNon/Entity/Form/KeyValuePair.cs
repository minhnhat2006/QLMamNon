using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLMamNon.Entity.Form
{
    public class KeyValuePair
    {
        public KeyValuePair(object key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        public object Key { get; set; }

        public object Value { get; set; }
    }
}
