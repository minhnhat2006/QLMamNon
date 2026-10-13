using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.UserControls;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmKhoiHoc : CRUDForm<khoi>
    {
        // TruongId đọc được lúc load, theo từng KhoiId. khoi.TruongId là [NotMapped] nên EF không
        // thấy thay đổi của nó: nếu người dùng chỉ đổi Trường của một Khối thì entry vẫn là
        // Unchanged, phải so với bản chụp này mới biết cần ghi lại khoi_truong.
        private readonly Dictionary<int, int?> loadedTruongIdByKhoiId = new Dictionary<int, int?>();

        // base.onSaving() tự bắt lỗi và báo qua onSavingError, không trả về trạng thái thành công,
        // nên dùng cờ này để biết SaveChanges() của lớp cơ sở có lỗi hay không.
        private bool savingFailed;

        public FrmKhoiHoc()
        {
            InitializeComponent();

            this.TablePrimaryKey = "KhoiId";
            this.DanhMuc = DanhMucConstant.KhoiHoc;
            this.FormKey = AppForms.FormDanhMucTruongHoc;

            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormKhoiHoc();
            this.loadKhoiData();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.khoiRowBindingSource.DataSource);
        }

        private void gvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "TruongId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                Object truongIdObj = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "TruongId");

                if (truongIdObj != null)
                {
                    int truongId = (int)truongIdObj;
                    e.DisplayText = StaticDataUtil.GetTruongNameByTruongId(truongId);
                }
            }
        }

        private void loadKhoiData()
        {
            Entities.khois.Load();
            BindingList<khoi> dataTable = Entities.khois.Local.ToBindingList();

            foreach (khoi row in dataTable)
            {
                int? truongId = StaticDataUtil.GetTruongIdByKhoiId(Entities, row.KhoiId);
                if (truongId.HasValue)
                {
                    row.TruongId = truongId.Value;
                }
            }

            this.khoiRowBindingSource.DataSource = dataTable;

            // Chỉ chụp lại TruongId khi đã load xong: load lỗi giữa đường thì bản chụp không được
            // phép khác với dữ liệu đang hiển thị trên lưới.
            this.refreshTruongIdSnapshot(dataTable);
        }

        protected override void onSaving()
        {
            if (this.DataTable == null)
            {
                return;
            }

            this.savingFailed = false;
            List<khoi_truong> addedLinkRows = new List<khoi_truong>();
            bool baseSaveSucceeded = false;

            try
            {
                // Một transaction cho cả khoi và khoi_truong: deleteKhoiTruongByKhoiId chạy ngay lập
                // tức qua ExecuteFunction trên cùng context nên có enlist vào transaction này. Nếu
                // không có transaction, SaveChanges() lỗi sẽ để lại khoi_truong đã bị xóa trong khi
                // khoi vẫn còn.
                using (DbContextTransaction transaction = Entities.Database.BeginTransaction())
                {
                    List<khoi> deletedRow = new List<khoi>();
                    List<khoi> addedRow = new List<khoi>();
                    List<khoi> modifiedRow = new List<khoi>();

                    foreach (khoi row in this.DataTable)
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
                        else if (entry.State == EntityState.Modified || this.isTruongIdChanged(row))
                        {
                            modifiedRow.Add(row);
                        }
                    }

                    foreach (khoi row in deletedRow)
                    {
                        Entities.deleteKhoiTruongByKhoiId(row.KhoiId);
                    }

                    foreach (khoi row in modifiedRow)
                    {
                        khoi_truong khoiTruongRow = StaticDataUtil.GetKhoiTruongByKhoiId(Entities, row.KhoiId);

                        if (khoiTruongRow != null)
                        {
                            Entities.deleteKhoiTruongByKhoiId(row.KhoiId);
                        }

                        if (row.TruongId != null)
                        {
                            khoi_truong khoiTruong = new khoi_truong()
                            {
                                KhoiId = row.KhoiId,
                                TruongId = row.TruongId.Value
                            };
                            Entities.khoi_truong.Add(khoiTruong);
                            addedLinkRows.Add(khoiTruong);
                        }
                    }

                    base.onSaving();

                    if (this.savingFailed)
                    {
                        // base.onSaving() đã hiện thông báo lỗi và ghi log. Không Commit để các dòng
                        // khoi_truong vừa bị stored procedure xóa được phục hồi, và bỏ các dòng
                        // khoi_truong vừa thêm ra khỏi context để lần Lưu sau không ghi trùng.
                        this.detachLinkRows(addedLinkRows);
                        return;
                    }

                    baseSaveSucceeded = true;

                    // Dòng mới chỉ có KhoiId thật sau khi base.onSaving() lưu thành công: ghi
                    // khoi_truong trước đó sẽ tạo liên kết với KhoiId = 0.
                    foreach (khoi row in addedRow)
                    {
                        if (row.TruongId != null && row.KhoiId > 0)
                        {
                            khoi_truong khoiTruong = new khoi_truong()
                            {
                                KhoiId = row.KhoiId,
                                TruongId = row.TruongId.Value
                            };
                            Entities.khoi_truong.Add(khoiTruong);
                            addedLinkRows.Add(khoiTruong);
                        }
                        else if (row.TruongId != null)
                        {
                            LoggerFacade.Warning(String.Format("Bỏ qua khoi_truong của khối '{0}' vì KhoiId vẫn là 0 sau khi lưu.", row.Name),
                                this.FormKey);
                        }
                    }

                    Entities.SaveChanges();
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                this.detachLinkRows(addedLinkRows);
                this.onSavingError(ex);

                if (baseSaveSucceeded)
                {
                    // Transaction bị hủy sau khi SaveChanges() của lớp cơ sở đã chạy: context coi các
                    // dòng là đã lưu trong khi DB thì không, nên lần Lưu sau sẽ không ghi lại chúng.
                    LoggerFacade.Error("Transaction bị hủy sau khi lưu khối học: dữ liệu trên lưới không còn khớp với cơ sở dữ liệu.", this.FormKey);
                    MessageBox.Show(String.Format("Các thay đổi đã bị hủy bỏ. Vui lòng đóng và mở lại danh mục {0} rồi nhập lại.", this.DanhMuc),
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return;
            }

            this.refreshTruongIdSnapshot(this.DataTable);

            // Reload StaticData for Khoi
            StaticDataFacade.Reload(StaticDataKeys.KhoiHoc);
        }

        protected override void onSavingError(Exception ex)
        {
            this.savingFailed = true;
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
            base.onSavingError(ex);
        }

        private bool isTruongIdChanged(khoi row)
        {
            int? loadedTruongId;

            if (!this.loadedTruongIdByKhoiId.TryGetValue(row.KhoiId, out loadedTruongId))
            {
                return row.TruongId != null;
            }

            return loadedTruongId != row.TruongId;
        }

        private void refreshTruongIdSnapshot(IEnumerable<khoi> rows)
        {
            this.loadedTruongIdByKhoiId.Clear();

            if (rows == null)
            {
                return;
            }

            foreach (khoi row in rows)
            {
                if (row.KhoiId > 0)
                {
                    this.loadedTruongIdByKhoiId[row.KhoiId] = row.TruongId;
                }
            }
        }

        private void detachLinkRows(List<khoi_truong> linkRows)
        {
            foreach (khoi_truong linkRow in linkRows)
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
