using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Windows.Forms;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmLopHoc : CRUDForm<lop>
    {
        // KhoiId đọc được lúc load, theo từng LopId. lop.KhoiId là [NotMapped] nên EF không thấy
        // thay đổi của nó: nếu người dùng chỉ đổi Khối của một Lớp thì entry vẫn là Unchanged, phải
        // so với bản chụp này mới biết cần ghi lại lop_khoi.
        private readonly Dictionary<int, int?> loadedKhoiIdByLopId = new Dictionary<int, int?>();

        // base.onSaving() tự bắt lỗi và báo qua onSavingError, không trả về trạng thái thành công,
        // nên dùng cờ này để biết SaveChanges() của lớp cơ sở có lỗi hay không.
        private bool savingFailed;

        // Trong khi transaction còn mở thì không được hiện MessageBox: InnoDB giữ lock của các dòng
        // lop_khoi vừa xóa cho tới khi người dùng bấm OK, máy khác sửa cùng lớp sẽ bị lock timeout.
        // Vì vậy gom lỗi lại và chỉ báo sau khi transaction đã rollback.
        private bool deferSavingError;

        private Exception deferredSavingError;

        public FrmLopHoc()
        {
            InitializeComponent();

            this.TablePrimaryKey = "LopId";
            this.DanhMuc = DanhMucConstant.LopHoc;
            this.FormKey = AppForms.FormDanhMucTruongHoc;

            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormLopHoc(Entities);
            this.loadLopData();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.lopRowBindingSource.DataSource);
        }

        private void gvMain_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "KhoiId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object khoiIdObj = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "KhoiId");

                if (khoiIdObj != null)
                {
                    int khoiId = (int)khoiIdObj;
                    e.DisplayText = StaticDataUtil.GetKhoiNameByKhoiId(khoiId);
                }
            }
        }

        private void loadLopData()
        {
            Entities.lops.Load();
            BindingList<lop> dataTable = Entities.lops.Local.ToBindingList();

            foreach (lop row in dataTable)
            {
                int? khoiId = StaticDataUtil.GetKhoiIdByLopId(Entities, row.LopId);
                if (khoiId.HasValue)
                {
                    row.KhoiId = khoiId.Value;
                }
            }

            this.lopRowBindingSource.DataSource = dataTable;

            // Chỉ chụp lại KhoiId khi đã load xong: load lỗi giữa đường thì bản chụp không được
            // phép khác với dữ liệu đang hiển thị trên lưới.
            this.refreshKhoiIdSnapshot(dataTable);
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
            List<lop_khoi> addedLinkRows = new List<lop_khoi>();
            bool baseSaveSucceeded = false;
            bool committed = false;

            try
            {
                // Một transaction cho cả lop và lop_khoi: deleteLopKhoiByLopId chạy ngay lập tức qua
                // ExecuteFunction trên cùng context nên có enlist vào transaction này. Nếu không có
                // transaction, SaveChanges() lỗi sẽ để lại lop_khoi đã bị xóa trong khi lop vẫn còn.
                using (DbContextTransaction transaction = Entities.Database.BeginTransaction())
                {
                    List<lop> deletedRow = new List<lop>();
                    List<lop> addedRow = new List<lop>();
                    List<lop> modifiedRow = new List<lop>();

                    foreach (lop row in this.DataTable)
                    {
                        DbEntityEntry entry = Entities.Entry(row);

                        if (entry.State == EntityState.Deleted)
                        {
                            deletedRow.Add(row);
                        }
                        else if (entry.State == EntityState.Added || entry.State == EntityState.Detached)
                        {
                            // Detached: dòng vừa thêm trên lưới, base.onSaving() sẽ chuyển sang Added.
                            addedRow.Add(row);
                        }
                        else if (entry.State == EntityState.Modified || this.isKhoiIdChanged(row))
                        {
                            modifiedRow.Add(row);
                        }
                    }

                    foreach (lop row in deletedRow)
                    {
                        Entities.deleteLopKhoiByLopId(row.LopId);
                    }

                    foreach (lop row in modifiedRow)
                    {
                        int? khoiId = StaticDataUtil.GetKhoiIdByLopId(Entities, row.LopId);

                        if (khoiId != null)
                        {
                            Entities.deleteLopKhoiByLopId(row.LopId);
                        }

                        if (row.KhoiId != null)
                        {
                            lop_khoi lopKhoi = new lop_khoi()
                            {
                                LopId = row.LopId,
                                KhoiId = row.KhoiId.Value
                            };
                            Entities.lop_khoi.Add(lopKhoi);
                            addedLinkRows.Add(lopKhoi);
                        }
                    }

                    base.onSaving();

                    if (this.savingFailed)
                    {
                        // Không Commit để các dòng lop_khoi vừa bị stored procedure xóa được phục
                        // hồi, và bỏ các dòng lop_khoi vừa thêm ra khỏi context để lần Lưu sau không
                        // ghi trùng. Lỗi được báo bên dưới, sau khi transaction đã rollback.
                        this.detachLinkRows(addedLinkRows);
                    }
                    else
                    {
                        baseSaveSucceeded = true;

                        // Dòng mới chỉ có LopId thật sau khi base.onSaving() lưu thành công: ghi
                        // lop_khoi trước đó sẽ tạo liên kết với LopId = 0.
                        foreach (lop row in addedRow)
                        {
                            if (row.KhoiId != null && row.LopId > 0)
                            {
                                lop_khoi lopKhoi = new lop_khoi()
                                {
                                    LopId = row.LopId,
                                    KhoiId = row.KhoiId.Value
                                };
                                Entities.lop_khoi.Add(lopKhoi);
                                addedLinkRows.Add(lopKhoi);
                            }
                            else if (row.KhoiId != null)
                            {
                                LoggerFacade.Warning(String.Format("Bỏ qua lop_khoi của lớp '{0}' vì LopId vẫn là 0 sau khi lưu.", row.Name),
                                    this.FormKey);
                            }
                        }

                        Entities.SaveChanges();
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
                    this.detachLinkRows(addedLinkRows);
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
                    // dòng là đã lưu trong khi DB thì không, nên lần Lưu sau sẽ không ghi lại chúng.
                    LoggerFacade.Error("Transaction bị hủy sau khi lưu lớp học: dữ liệu trên lưới không còn khớp với cơ sở dữ liệu.", this.FormKey);
                    MessageBox.Show(String.Format("Các thay đổi đã bị hủy bỏ. Vui lòng đóng và mở lại danh mục {0} rồi nhập lại.", this.DanhMuc),
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return;
            }

            this.refreshKhoiIdSnapshot(this.DataTable);

            // Reload StaticData for Lop
            StaticDataFacade.Reload(StaticDataKeys.LopHoc);
        }

        protected override void onCanceling()
        {
            base.onCanceling();

            // base.onCanceling() hoàn tác bằng Entry(row).Reload() nên không phục hồi được KhoiId
            // ([NotMapped], và sửa riêng Khối còn không làm entry thành Modified). Nếu không gán lại
            // từ bản chụp thì lưới vẫn giữ Khối người dùng đã hủy và lần Lưu sau sẽ ghi nó xuống
            // lop_khoi.
            if (this.DataTable != null)
            {
                foreach (lop row in this.DataTable)
                {
                    int? loadedKhoiId;

                    if (this.loadedKhoiIdByLopId.TryGetValue(row.LopId, out loadedKhoiId))
                    {
                        row.KhoiId = loadedKhoiId;
                    }
                    else
                    {
                        row.KhoiId = null;
                    }
                }
            }

            // Bỏ khỏi bản chụp các dòng không còn trên lưới (dòng mới thêm rồi hủy).
            this.refreshKhoiIdSnapshot(this.DataTable);

            this.lopRowBindingSource.ResetBindings(false);
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

        private bool isKhoiIdChanged(lop row)
        {
            int? loadedKhoiId;

            if (!this.loadedKhoiIdByLopId.TryGetValue(row.LopId, out loadedKhoiId))
            {
                return row.KhoiId != null;
            }

            return loadedKhoiId != row.KhoiId;
        }

        private void refreshKhoiIdSnapshot(IEnumerable<lop> rows)
        {
            this.loadedKhoiIdByLopId.Clear();

            if (rows == null)
            {
                return;
            }

            foreach (lop row in rows)
            {
                if (row.LopId > 0)
                {
                    this.loadedKhoiIdByLopId[row.LopId] = row.KhoiId;
                }
            }
        }

        private void detachLinkRows(List<lop_khoi> linkRows)
        {
            foreach (lop_khoi linkRow in linkRows)
            {
                DbEntityEntry entry = Entities.Entry(linkRow);

                if (entry.State == EntityState.Added)
                {
                    entry.State = EntityState.Detached;
                }
            }

            linkRows.Clear();
        }
    }
}
