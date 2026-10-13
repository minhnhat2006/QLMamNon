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
        // base.onSaving() tự bắt lỗi và báo qua onSavingError, không trả về trạng thái thành công,
        // nên dùng cờ này để biết SaveChanges() của lớp cơ sở có lỗi hay không.
        private bool savingFailed;

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
            if (this.DataTable == null)
            {
                return;
            }

            this.savingFailed = false;

            // Lưu user trước: dòng mới chỉ có UserId thật sau khi SaveChanges() chạy, ghi quyền
            // trước đó sẽ tạo user_privilege với UserId = 0. base.onSaving() đặt trạng thái
            // "Đã lưu" khi thành công; nếu bước ghi quyền bên dưới lỗi thì đổi lại thành
            // "Lưu không thành công".
            base.onSaving();

            if (this.savingFailed)
            {
                return;
            }

            try
            {
                AuthenService authenService = new AuthenService();

                foreach (user userRow in this.DataTable)
                {
                    if (userRow.UserId > 0)
                    {
                        // UpdateUserPrivileges chỉ gọi stored procedure (deleteUserPrivileges,
                        // insertUserPrivilegeIfNotExists) qua ExecuteFunction nên ghi thẳng xuống DB,
                        // không có thay đổi nào chờ SaveChanges() nữa.
                        authenService.UpdateUserPrivileges(Entities, userRow.UserId, userRow.UserPrivileges);
                    }
                    else if (userRow.UserPrivileges != null)
                    {
                        LoggerFacade.Warning(String.Format("Bỏ qua quyền của người dùng '{0}' vì UserId vẫn là 0 sau khi lưu.", userRow.UserName),
                            this.FormKey);
                    }
                }
            }
            catch (Exception ex)
            {
                this.onSavingError(ex);
            }
        }

        protected override void onSavingError(Exception ex)
        {
            this.savingFailed = true;
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
            base.onSavingError(ex);
        }
    }
}