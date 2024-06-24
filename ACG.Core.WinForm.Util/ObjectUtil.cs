
namespace ACG.Core.WinForm.Util
{
    public class ObjectUtil
    {
        public static bool equals(object obj1, object obj2)
        {
            return (obj1 == null && obj2 == null) || (obj1 != null && obj1.Equals(obj2));
        }
    }
}
