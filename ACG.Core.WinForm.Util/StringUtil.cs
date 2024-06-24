using System;
using System.Collections;
using System.Collections.Generic;

namespace ACG.Core.WinForm.Util
{
    public class StringUtil
    {
        public static bool IsEmpty(string value)
        {
            return value == null || value == "";
        }

        public static String Join(IList list, string separator)
        {
            List<string> listStr = new List<string>();

            if (ListUtil.IsEmpty(list))
            {
                return "";
            }

            foreach (object item in list)
            {
                listStr.Add(String.Format("{0}", item));
            }

            return String.Join(separator, listStr);
        }

        public static String Join(object[] objs, string separator)
        {
            List<object> list = new List<object>(objs);

            return Join(list, separator);
        }

        public static String JoinWithCommas(IList list)
        {
            return Join(list, ",");
        }

        public static string[] Split(string str, string separator)
        {
            return str.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
