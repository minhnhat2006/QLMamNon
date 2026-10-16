using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using QLMamNon.UserControls;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace QLMamNon.Forms.SystemSetting
{
    public partial class FrmUser : CRUDForm<user>
    {
        // base.onSaving() tự bắt lỗi và báo qua onSavingError, không trả về trạng thái thành công,
        // nên dùng cờ này để biết SaveChanges() của lớp cơ sở có lỗi hay không.
        private bool savingFailed;

        // Trong khi transaction còn mở thì không được hiện MessageBox: lock của các dòng user,
        // user_privilege bị giữ cho tới khi người dùng bấm OK. Vì vậy gom lỗi lại và chỉ báo sau
        // khi transaction đã rollback.
        private bool deferSavingError;

        private Exception deferredSavingError;

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
            this.deferredSavingError = null;
            this.deferSavingError = true;
            bool baseSaveSucceeded = false;
            bool committed = false;

            try
            {
                // Một transaction cho cả user và user_privilege: UpdateUserPrivileges chỉ gọi stored
                // procedure qua ExecuteFunction trên cùng context nên có enlist. Nếu không có
                // transaction, lỗi giữa vòng lặp sẽ để lại user đã lưu nhưng quyền của người dùng
                // đang xử lý vừa bị deleteUserPrivileges xóa mà chưa ghi lại được.
                using (DbContextTransaction transaction = Entities.Database.BeginTransaction())
                {
                    // Lưu user trước: dòng mới chỉ có UserId thật sau khi SaveChanges() chạy, ghi
                    // quyền trước đó sẽ tạo user_privilege với UserId = 0.
                    base.onSaving();

                    if (!this.savingFailed)
                    {
                        baseSaveSucceeded = true;
                        AuthenService authenService = new AuthenService();

                        foreach (user userRow in this.DataTable)
                        {
                            if (userRow.UserId > 0)
                            {
                                // UpdateUserPrivileges chỉ gọi stored procedure (deleteUserPrivileges,
                                // insertUserPrivilegeIfNotExists) qua ExecuteFunction nên ghi thẳng
                                // xuống DB, không có thay đổi nào chờ SaveChanges() nữa.
                                authenService.UpdateUserPrivileges(Entities, userRow.UserId, userRow.UserPrivileges);
                            }
                            else if (userRow.UserPrivileges != null)
                            {
                                LoggerFacade.Warning(String.Format("Bỏ qua quyền của người dùng '{0}' vì UserId vẫn là 0 sau khi lưu.", userRow.UserName),
                                    this.FormKey);
                            }
                        }

                        transaction.Commit();
                        committed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (committed)
                {
                    // Commit() đã xong, lỗi chỉ xảy ra khi Dispose() transaction: dữ liệu đã được lưu
                    // nên chỉ ghi log, không được báo với người dùng là chưa lưu.
                    LoggerFacade.Error(ex, this.FormKey);
                }
                else
                {
                    this.savingFailed = true;
                    this.deferredSavingError = ex;
                }
            }
            finally
            {
                this.deferSavingError = false;
            }

            if (this.savingFailed)
            {
                this.reportDeferredSavingError();

                if (baseSaveSucceeded)
                {
                    // Transaction bị hủy sau khi SaveChanges() của lớp cơ sở đã chạy: context coi các
                    // dòng là đã lưu trong khi DB thì không, và danh sách quyền chỉ nằm trong bộ nhớ,
                    // nên lần Lưu sau sẽ không ghi lại chúng.
                    LoggerFacade.Error("Transaction bị hủy sau khi lưu người dùng: dữ liệu trên lưới không còn khớp với cơ sở dữ liệu.", this.FormKey);
                    MessageBox.Show(String.Format("Các thay đổi đã bị hủy bỏ. Vui lòng đóng và mở lại danh mục {0} rồi nhập lại.", this.DanhMuc),
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        protected override void onSavingError(Exception ex)
        {
            this.savingFailed = true;

            if (this.deferSavingError)
            {
                this.deferredSavingError = ex;
                return;
            }

            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
            base.onSavingError(ex);
        }

        private void reportDeferredSavingError()
        {
            Exception ex = this.deferredSavingError;
            this.deferredSavingError = null;

            if (ex == null)
            {
                FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
                return;
            }

            // deferSavingError đã được tắt trong finally nên lần này lỗi được báo thật.
            this.onSavingError(ex);
        }
    }
}