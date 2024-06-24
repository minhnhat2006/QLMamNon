using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using QLMamNon.UserControls;
using System;
using System.Data.Entity;

namespace QLMamNon.Forms.SystemSetting
{
    public partial class FrmUser : CRUDForm<user>
    {
        public FrmUser()
        {
            InitializeComponent();

            this.TablePrimaryKey = "UserId";
            this.DanhMuc = DanhMucConstant.User;
            this.FormKey = AppForms.FormUser;

            Entities.users.Load();
            this.userBindingSource.DataSource = Entities.users.Local.ToBindingList();

            UCEditFormUser ucEditFormUser = new UCEditFormUser();
            ucEditFormUser.Entities = this.Entities;
            ucEditFormUser.GridView = this.gvMain;
            this.gvMain.OptionsEditForm.CustomEditFormLayout = ucEditFormUser;
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.userBindingSource.DataSource);
        }

        protected override void onSaving()
        {
            try
            {
                AuthenService authenService = new AuthenService();

                foreach (user userRow in DataTable)
                {
                    authenService.UpdateUserPrivileges(Entities, userRow.UserId, userRow.UserPrivileges);
                }

                base.onSaving();
            }
            catch (Exception ex)
            {
                this.onSavingError(ex);
            }

            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SavedCaption);
        }
    }
}