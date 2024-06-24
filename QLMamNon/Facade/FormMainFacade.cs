using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Entity;
using QLMamNon.Forms;
using QLMamNon.Forms.Resource;
using QLMamNon.Service.Data;
using QLMamNon.Dao;

namespace QLMamNon.Facade
{
    public static class FormMainFacade
    {
        public enum FormShowType { Normal, Dialog };

        private static IDictionary<string, string> formKeysToCaptions;

        private static FrmMain frmMain;

        private static FormFactory formFactory = new FormFactory();

        public static void InitFormMain(FrmMain formMain)
        {
            frmMain = formMain;
            formKeysToCaptions = new Dictionary<string, string>();
        }

        public static Form GetForm(string form)
        {
            Form frm = formFactory.GetForm(form);
            return frm;
        }

        public static void ShowForm(string form)
        {
            saveCurrentForm(form, FormShowType.Normal);

            if (!checkPrivilegeToAccessForm(form))
            {
                return;
            }

            Form frm = formFactory.GetForm(form);
            frm.MdiParent = frmMain;
            frm.Show();
            frm.Activate();

            if (formKeysToCaptions.ContainsKey(form))
            {
                string caption = formKeysToCaptions[form];
                SetStatusCaption(form, caption);
            }
        }

        private static void saveCurrentForm(string form, FormShowType showType)
        {
            if (AppForms.FormLogin != form)
            {
                StaticDataFacade.Save(StaticDataKeys.CurrentForm, form);
                StaticDataFacade.Save(StaticDataKeys.CurrentFormShowType, showType);
            }
        }

        private static bool checkPrivilegeToAccessForm(string form)
        {
            AuthenService authenService = new AuthenService();
            List<user_privilege> upTable = null;

            if (authenService.IsAuthenticated())
            {
                AuthenticatedEntity authenData = (AuthenticatedEntity)StaticDataFacade.Get(StaticDataKeys.AuthenticatedData);
                upTable = authenData.UserPrivilegeTable;
            }

            if (!authenService.CanAccess(form, upTable))
            {
                if (!authenService.IsAuthenticated())
                {
                    ShowDialog(AppForms.FormLogin);
                }
                else
                {
                    MessageBox.Show("Bạn không thể truy cập vào phần này.", "Thiếu quyền truy cập",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                return false;
            }

            return true;
        }

        public static void ShowDialog(string form)
        {
            saveCurrentForm(form, FormShowType.Dialog);

            if (!checkPrivilegeToAccessForm(form))
            {
                return;
            }

            Form frm = formFactory.GetForm(form);
            frm.ShowDialog();
            frm.Activate();
        }

        public static void ShowReport(XtraReport rpt)
        {
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }

        public static void CloseForm(string form)
        {
            formFactory.RemoveForm(form);
        }

        public static void SetFormCaption(string formKey)
        {
            string caption = formFactory.GetFormCaption(formKey);
            frmMain.SetManHinhCaption(caption);
        }

        public static void SetStatusCaption(string formKey, string caption)
        {
            formKeysToCaptions.Remove(formKey);
            formKeysToCaptions.Add(formKey, caption);
            frmMain.SetTrangThaiCaption(caption);
        }
    }
}
