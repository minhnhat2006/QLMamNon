using DevExpress.XtraEditors;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Facade;
using QLMamNon.Forms;
using QLMamNon.Forms.HocSinh;
using QLMamNon.UserControls;
using System.Collections.Generic;
using System.Windows.Forms;


namespace QLMamNon.Workflow.Handler
{
    public class AdjustPopupEditFormAction : ACG.Core.WinForm.Mediator.Action
    {
        #region Properties

        protected Dictionary<string, string> EDIT_FORM_BUTTON_CAPTION_MAP = new Dictionary<string, string>
            {
                {"Update", "Lưu"},
                {"Cancel", "Hủy"}
            };

        protected Dictionary<string, string> EDIT_FORM_BUTTON_CAPTION_TO_IMAGE_MAP = new Dictionary<string, string>
            {
                {"Update", "btnLuu.Image"},
                {"Cancel", "btnHuyBo.Image"}
            };

        #endregion

        public void gvMain_ShowingPopupEditForm(object sender, ShowingPopupEditFormEventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThongTinHocSinh));

            e.EditForm.MaximizeBox = false;
            e.EditForm.MinimizeBox = false;

            foreach (Control control in e.EditForm.Controls)
            {
                if (control is EditFormContainer)
                {
                    foreach (Control nestedControl in control.Controls)
                    {
                        if (nestedControl is PanelControl)
                        {
                            foreach (Control button in nestedControl.Controls)
                                if (button is SimpleButton)
                                {
                                    SimpleButton simpleButton = button as SimpleButton;

                                    if (!EDIT_FORM_BUTTON_CAPTION_TO_IMAGE_MAP.ContainsKey(simpleButton.Text))
                                    {
                                        return;
                                    }

                                    simpleButton.Image = ((System.Drawing.Image)(resources.GetObject(EDIT_FORM_BUTTON_CAPTION_TO_IMAGE_MAP[simpleButton.Text])));
                                    simpleButton.Text = EDIT_FORM_BUTTON_CAPTION_MAP[simpleButton.Text];

                                    ActivityTrackerFacade.InitActivityTracker(e.EditForm);
                                }
                        }
                    }
                }
            }

            // Perform cancelling on EditForm closed
            GridView gridView = sender as GridView;
            UCCRUDBase uc = gridView.OptionsEditForm.CustomEditFormLayout as UCCRUDBase;
            uc.GridView = gridView;
            e.EditForm.FormClosed += new FormClosedEventHandler(EditForm_FormClosed);
        }

        private void EditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            XtraForm form = sender as XtraForm;

            foreach (Control control in form.Controls)
            {
                if (control is EditFormContainer)
                {
                    UCCRUDBase uc = (control as EditFormContainer).ActiveControl as UCCRUDBase;

                    if (uc != null)
                    {
                        uc.GridView.CancelUpdateCurrentRow();
                    }
                }
            }
        }

        public override void Receive(object from, object message)
        {
            GridView gv = message as GridView;
            gv.ShowingPopupEditForm += new ShowingPopupEditFormEventHandler(this.gvMain_ShowingPopupEditForm);
        }
    }
}
