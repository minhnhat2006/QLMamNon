using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ACG.Core.WinForm.Util
{
    public class ControlUtil
    {
        public static string GetFullNameOfControl(Control control)
        {
            string name = "";

            while (control.Parent != null && !(control is Form))
            {
                name = string.Format("${0}", control.Name) + name;
                control = control.Parent;
            }

            if ((control is Form))
            {
                name = string.Format("${0}", control.Name) + name;
            }

            return name;
        }

        public static Form GetForm(Control control)
        {
            while (control != null && !(control is Form))
            {
                control = control.Parent;
            }

            return control as Form;
        }

        public static bool IsEditValueNull(BaseEdit control)
        {
            return control.EditValue == DBNull.Value || control.EditValue == null;
        }
    }
}
