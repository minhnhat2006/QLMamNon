
namespace ACG.Core.WinForm.Util
{
    public class IntUtil
    {
        public static int? StringToInt(string value)
        {
            int intValue = 0;

            if (!int.TryParse(value, out intValue))
            {
                return null;
            }

            return intValue;
        }
    }
}
