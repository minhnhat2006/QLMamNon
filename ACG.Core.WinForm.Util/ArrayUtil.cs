using System;

namespace ACG.Core.WinForm.Util
{
    public class ArrayUtil
    {
        public static bool IsEmpty(Object[] arr)
        {
            return arr == null || arr.Length == 0;
        }

        public static bool IsEmpty(int[] arr)
        {
            return arr == null || arr.Length == 0;
        }

        public static bool Contains(object[] arr, object value)
        {
            return Array.LastIndexOf(arr, value) > -1;
        }
    }
}
