using ACG.Core.WinForm.Util;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Components.Command;
using QLMamNon.Components.Command.QLMNDao;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Properties;
using QLMamNon.Reports;
using QLMamNon.Service.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmSoThuTien : CRUDForm<viewbangthutien>
    {
        private class RowInfo
        {
            public RowInfo(viewbangthutien row, int bangThuTienId)
            {
                this.Row = row;
                this.BangThuTienId = bangThuTienId;
            }
            // The row object, not its handle: handles go stale as soon as the grid is re-sorted
            // between building the context menu and clicking its item.
            public viewbangthutien Row;
            public int BangThuTienId;
        }

        #region Properties

        private List<hocsinh> hocSinhDataTable;
        private List<bangthutien_khoanthu> bTTKTDataTable;
        private Dictionary<int, viewbangthutien> prevMonthRowDictionary;
        private List<phieuthu> phieuThuDataTable;
        private BangThuTienGenHistoryCache genHistoryCache;
        private DateTime ngayTinh;
        private int lop;
        private bool isGridLayoutInitialized;

        private static readonly string[] HidePhuPhiColumns = new string[]
        {
            ViewBangThuTienFieldName.TienAnSua,
            ViewBangThuTienFieldName.TienSua,
            ViewBangThuTienFieldName.PhuPhi,
            ViewBangThuTienFieldName.KhoanThuChinh,
            ViewBangThuTienFieldName.SXThangTruoc,
            ViewBangThuTienFieldName.SoTienSXThangTruoc,
            ViewBangThuTienFieldName.AnSangThangTruoc,
            ViewBangThuTienFieldName.SoTienAnSangThangTruoc,
            ViewBangThuTienFieldName.SoTienAnSangThangNay,
            ViewBangThuTienFieldName.SoTienAnSangConLai,
            ViewBangThuTienFieldName.AnToiThangTruoc,
            ViewBangThuTienFieldName.SoTienAnToiThangTruoc,
            ViewBangThuTienFieldName.SoTienAnToiThangNay,
            ViewBangThuTienFieldName.SoTienAnToiConLai,
            ViewBangThuTienFieldName.SoTienDoDung,
            ViewBangThuTienFieldName.SoTienDieuHoa,
            ViewBangThuTienFieldName.SoTienNangKhieu
        };

        private static readonly HashSet<string> FieldsToEvaluate = new HashSet<string>(StringComparer.Ordinal)
        {
            ViewBangThuTienFieldName.SXThangTruoc,
            ViewBangThuTienFieldName.AnSangThangTruoc,
            ViewBangThuTienFieldName.SoTienAnSangThangNay,
            ViewBangThuTienFieldName.AnToiThangTruoc,
            ViewBangThuTienFieldName.SoTienAnToiThangNay,
            ViewBangThuTienFieldName.SoTienNangKhieu,
            ViewBangThuTienFieldName.SoTienTruyThu,
            ViewBangThuTienFieldName.SoTienDieuHoa,
            ViewBangThuTienFieldName.SoTienDoDung,
            ViewBangThuTienFieldName.TienAnSua,
            ViewBangThuTienFieldName.TienSua,
            ViewBangThuTienFieldName.PhuPhi,
            ViewBangThuTienFieldName.BanTru,
            ViewBangThuTienFieldName.HocPhi,
            ViewBangThuTienFieldName.PhucVuBanTru
        };

        #endregion

        #region Events

        public FrmSoThuTien()
            : base()
        {
            InitializeComponent();

            this.GridViewColumnSequenceName = null;
            this.TablePrimaryKey = "BangThuTienId";
            this.DanhMuc = DanhMucConstant.SoThuTien;
            this.FormKey = AppForms.FormSoThuTien;

            this.loadLopData();
            this.loadHocSinhData();

            // The designer never assigns viewBangThuTienRowBindingSource.DataSource, so passing it
            // straight to InitForm left the base DataTable null until the first search - and onSaving
            // iterates DataTable outside the base try/catch.
            BindingList<viewbangthutien> emptyTable = new BindingList<viewbangthutien>();
            this.viewBangThuTienRowBindingSource.DataSource = emptyTable;
            this.InitForm(null, null, null, this.btnLuu, this.btnHuyBo, this.gvMain, emptyTable);
        }

        private void FrmSoThuTien_Load(object sender, EventArgs e)
        {
            this.namHocBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.NamHoc);
            this.cmbNam.Text = DateTime.Now.Year.ToString();
            this.cmbThang.Text = DateTime.Now.Month.ToString();
            this.initGridLayout();
        }

        protected override void onCanceling()
        {
            // Reloading is enough to discard edits because loadViewBangThuTiens fetches the rows
            // with MergeOption.NoTracking. base.onCanceling() must NOT be called: it casts the grid
            // DataSource to BindingSource and calls Entry(row).Reload() on untracked rows.
            btnTimKiem.PerformClick();
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.CanceledCaption);
        }

        private void gcMain_Enter(object sender, EventArgs e)
        {
            this.initGridLayout();
        }

        /// <summary>
        /// One-time column/band/layout setup. Guarded because it used to run on every Enter of the
        /// grid control, which re-hid any column the user had shown again via the column chooser.
        /// </summary>
        private void initGridLayout()
        {
            if (this.isGridLayoutInitialized || Settings.Default.IsMNHongMinh)
            {
                return;
            }

            this.isGridLayoutInitialized = true;

            foreach (string columnName in HidePhuPhiColumns)
            {
                this.hideColumnAndOwnerBand(gvMain.Columns[columnName]);
            }

            // Change column caption PhucVuBanTru to "Học Phí"
            BandedGridColumn phucVuBanTruColumn = gvMain.Columns[ViewBangThuTienFieldName.PhucVuBanTru] as BandedGridColumn;

            if (phucVuBanTruColumn != null && phucVuBanTruColumn.OwnerBand != null)
            {
                phucVuBanTruColumn.OwnerBand.Caption = "Học Phí";
            }

            // The second STT column: it shares FieldName "STT" with colSTT, so it cannot be looked
            // up by name - use the designer field instead of a positional index.
            this.hideColumnAndOwnerBand(this.colSTT2);

            // btnPrint1
            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // btnPrint2
            layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // btnPrintGiayBaoNopTien
            layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // btnPrint
            layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        private void hideColumnAndOwnerBand(GridColumn gridColumn)
        {
            BandedGridColumn column = gridColumn as BandedGridColumn;

            if (column == null)
            {
                return;
            }

            column.Visible = false;

            if (column.OwnerBand == null)
            {
                return;
            }

            column.OwnerBand.Visible = false;

            if (column.OwnerBand.ParentBand != null)
            {
                column.OwnerBand.ParentBand.Visible = false;
            }
        }

        private void gcMain_MouseDown(object sender, MouseEventArgs e)
        {
            // Without this guard a right-click on a band header toggled the sort order while also
            // opening the row context menu.
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            BandedGridHitInfo hit = gvMain.CalcHitInfo(e.Location);

            if (hit.HitTest != BandedGridHitTest.Band)
            {
                return;
            }

            if (hit.Band == gridBand17)
            {
                this.colSTT.SortOrder = DevExpress.Data.ColumnSortOrder.None;
                this.colTen.SortOrder = this.colTen.SortOrder == DevExpress.Data.ColumnSortOrder.Descending
                    ? DevExpress.Data.ColumnSortOrder.Ascending
                    : DevExpress.Data.ColumnSortOrder.Descending;
            }
            else if (hit.Band == gridBand16)
            {
                this.colTen.SortOrder = DevExpress.Data.ColumnSortOrder.None;
                this.colSTT.SortOrder = this.colSTT.SortOrder == DevExpress.Data.ColumnSortOrder.Descending
                    ? DevExpress.Data.ColumnSortOrder.Ascending
                    : DevExpress.Data.ColumnSortOrder.Descending;
            }
        }

        private void gvMain_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.MenuType == GridMenuType.Row)
            {
                int rowHandle = e.HitInfo.RowHandle;
                // Delete existing menu items, if any. 
                e.Menu.Items.Clear();
                // Add the Rows submenu with the 'Delete Row' command 
                DXMenuItem item = CreateSubMenuRows(view, rowHandle);
                item.BeginGroup = true;
                e.Menu.Items.Add(item);
            }
        }

        private void gvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            // e.RowHandle < 0 covers InvalidRowHandle, the new item row, group rows and the
            // auto-filter row - GetRow returns null for all of them.
            if (gv == null || e.Value == null || e.RowHandle < 0)
            {
                return;
            }

            viewbangthutien row = gv.GetRow(e.RowHandle) as viewbangthutien;

            if (row == null)
            {
                return;
            }

            // Recalculate BanTru
            if (ViewBangThuTienFieldName.PhucVuBanTru == e.Column.FieldName)
            {
                row.BanTru = row.PhucVuBanTru - row.HocPhi;
            }

            if (FieldsToEvaluate.Contains(e.Column.FieldName))
            {
                BangThuTienUtil.EvaluateValuesForViewBangThuTienRow(row,
                    prevMonthRowDictionary != null && prevMonthRowDictionary.ContainsKey(row.HocSinhId) ? prevMonthRowDictionary[row.HocSinhId] : null,
                    bTTKTDataTable, phieuThuDataTable, true, false, false, this.genHistoryCache);

                if (ViewBangThuTienFieldName.SXThangTruoc.Equals(e.Column.FieldName))
                {
                    BangThuTienUtil.RecalculateBangThuTienKhoanThuList(Entities, row);
                }

                // viewbangthutien does not raise property-change notifications, so the footer
                // summaries over the recalculated columns would otherwise stay stale.
                gv.UpdateTotalSummary();
            }
        }

        private void gvMain_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "HoTen")
            {
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (this.isValidNgayTinhAndLop())
            {
                this.persistNgayTinhAndLop();
                this.loadViewBangThuTiens(ngayTinh, lop);
            }
        }

        private void persistNgayTinhAndLop()
        {
            int? lopId = this.cmbLop.EditValue as int?;

            if (lopId == null)
            {
                return;
            }

            this.ngayTinh = this.GetNgayTinh();
            this.lop = lopId.Value;
        }

        private DateTime GetNgayTinh()
        {
            if (ControlUtil.IsEditValueNull(this.cmbNam) || ControlUtil.IsEditValueNull(this.cmbThang))
            {
                return DateTime.MinValue;
            }

            // cmbThang is an editable combo, so its EditValue is whatever the user typed.
            int? nam = cmbNam.EditValue as int?;
            int? thang = IntUtil.StringToInt(Convert.ToString(cmbThang.EditValue));

            if (nam == null || thang == null || thang.Value < 1 || thang.Value > 12
                || nam.Value < 1 || nam.Value > 9999)
            {
                // Callers treat DateTime.MinValue as "invalid"; isValidNgayTinh shows the message.
                return DateTime.MinValue;
            }

            int ngay = DateTime.DaysInMonth(nam.Value, thang.Value);
            return new DateTime(nam.Value, thang.Value, ngay);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.cmbNam.EditValue = null;
            this.cmbThang.EditValue = null;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.isValidNgayTinhAndLop() && !this.isDataTableEmpty())
            {
                RptSoThuTienTiengAnh rpt = new RptSoThuTienTiengAnh();
                this.fillRptSoThuTien(rpt);
                FormMainFacade.ShowReport(rpt);
            }
        }

        private void btnPrint1_Click(object sender, EventArgs e)
        {
            if (this.isValidNgayTinhAndLop() && !this.isDataTableEmpty())
            {
                RptSoThuTienTrang1 rpt = new RptSoThuTienTrang1();
                this.fillRptSoThuTienTrang1(rpt);
                FormMainFacade.ShowReport(rpt);
            }
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            if (this.isValidNgayTinhAndLop() && !this.isDataTableEmpty())
            {
                RptSoThuTienTrang2 rpt = new RptSoThuTienTrang2();
                this.fillRptSoThuTienTrang2(rpt);
                FormMainFacade.ShowReport(rpt);
            }
        }

        private void btnPrintGiayBaoNopTien_Click(object sender, EventArgs e)
        {
            if (this.isValidNgayTinhAndLop() && !this.isDataTableEmpty())
            {
                RptGiayBaoNopTien rpt = new RptGiayBaoNopTien();
                this.fillRptGiayBaoNopTien(rpt);
                FormMainFacade.ShowReport(rpt);
            }
        }

        protected override void onSaving()
        {
            if (ListUtil.IsEmpty(this.DataTable))
            {
                return;
            }

            try
            {
                // One transaction over: the bangthutien columns, the bangthutien_khoanthu amounts
                // and the SoTienTruyThu stored procedure calls. The procedure wrappers run through
                // ExecuteFunction on this same context, so they enlist in this transaction.
                using (DbContextTransaction transaction = Entities.Database.BeginTransaction())
                {
                    List<int> bangThuTienIds = new List<int>(this.DataTable.Count);
                    Dictionary<int, viewbangthutien> viewRowsById = new Dictionary<int, viewbangthutien>(this.DataTable.Count);

                    foreach (viewbangthutien viewBangThuTienRow in this.DataTable)
                    {
                        viewBangThuTienRow.SoTienSXThangTruoc = BangThuTienUtil.SXToSoTienSX(
                            viewBangThuTienRow.SXThangTruoc == null ? 0 : viewBangThuTienRow.SXThangTruoc.Value,
                            Settings.Default.TienAnChinh);
                        viewBangThuTienRow.SoTienAnSangThangTruoc = BangThuTienUtil.SXAnSangToSoTienAnSang(
                            viewBangThuTienRow.AnSangThangTruoc == null ? 0 : viewBangThuTienRow.AnSangThangTruoc.Value,
                            Settings.Default.TienAnSang);
                        viewBangThuTienRow.SoTienAnToiThangTruoc = BangThuTienUtil.SXAnToiToSoTienAnToi(
                            viewBangThuTienRow.AnToiThangTruoc, Settings.Default.TienAnToi);

                        bangThuTienIds.Add(viewBangThuTienRow.BangThuTienId);

                        if (!viewRowsById.ContainsKey(viewBangThuTienRow.BangThuTienId))
                        {
                            viewRowsById.Add(viewBangThuTienRow.BangThuTienId, viewBangThuTienRow);
                        }
                    }

                    this.saveBangThuTienRows(bangThuTienIds, viewRowsById);
                    this.saveBangThuTienKhoanThuRows(bangThuTienIds, viewRowsById);

                    Entities.SaveChanges();

                    this.updateSoTienTruyThu();

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                this.onSavingError(ex);
                return;
            }

            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SavedCaption);

            btnTimKiem_Click(null, null);
        }

        /// <summary>
        /// viewbangthutien is a read-only entity set (a DefiningQuery with no UpdateFunction), so
        /// the edited columns have to be written through the bangthutien table entity instead.
        /// </summary>
        private void saveBangThuTienRows(List<int> bangThuTienIds, Dictionary<int, viewbangthutien> viewRowsById)
        {
            List<bangthutien> bangThuTienRows = Entities.bangthutiens
                .Where(b => bangThuTienIds.Contains(b.BangThuTienId)).ToList();

            foreach (bangthutien bangThuTienRow in bangThuTienRows)
            {
                if (viewRowsById.ContainsKey(bangThuTienRow.BangThuTienId))
                {
                    copyToBangThuTien(viewRowsById[bangThuTienRow.BangThuTienId], bangThuTienRow);
                }
            }
        }

        private void saveBangThuTienKhoanThuRows(List<int> bangThuTienIds, Dictionary<int, viewbangthutien> viewRowsById)
        {
            List<bangthutien_khoanthu> rowsToUpdate = Entities.getBangThuTienKhoanThuByBangThuTienIds(StringUtil.Join(bangThuTienIds, ",")).ToList();

            // If KhoanThuId does not exist in rowsToUpdate then adding it. The new rows go into the
            // same list so that the SoTien loop below fills them in - otherwise they were persisted
            // as 0 and the amounts typed by the user were lost.
            foreach (int bangThuTienId in bangThuTienIds)
            {
                List<int> khoanThuIds = new List<int>() { BangThuTienConstant.KhoanThuIdAnSang, BangThuTienConstant.KhoanThuIdAnToi, BangThuTienConstant.KhoanThuIdBanTru, BangThuTienConstant.KhoanThuIdHocPhi, BangThuTienConstant.KhoanThuIdPhuPhi, BangThuTienConstant.KhoanThuIdTienAnSua, BangThuTienConstant.KhoanThuIdTienSua };
                List<bangthutien_khoanthu> bangThuTienKhoanThuRows = rowsToUpdate.FindAll(b => b.BangThuTienId == bangThuTienId);

                foreach (bangthutien_khoanthu bangThuTienKhoanThuRow in bangThuTienKhoanThuRows)
                {
                    khoanThuIds.Remove(bangThuTienKhoanThuRow.KhoanThuId);
                }

                foreach (int khoanThuId in khoanThuIds)
                {
                    bangthutien_khoanthu bangThuTienKhoanThu = new bangthutien_khoanthu()
                    {
                        BangThuTienId = bangThuTienId,
                        KhoanThuId = khoanThuId
                    };
                    Entities.bangthutien_khoanthu.Add(bangThuTienKhoanThu);
                    rowsToUpdate.Add(bangThuTienKhoanThu);
                }
            }

            // Update SoTien
            foreach (bangthutien_khoanthu bangThuTienKhoanThuRow in rowsToUpdate)
            {
                if (!viewRowsById.ContainsKey(bangThuTienKhoanThuRow.BangThuTienId))
                {
                    // Orphan khoản thu row: its bangthutien is not in the grid any more.
                    continue;
                }

                viewbangthutien viewBangThuTienRow = viewRowsById[bangThuTienKhoanThuRow.BangThuTienId];

                switch (bangThuTienKhoanThuRow.KhoanThuId)
                {
                    case BangThuTienConstant.KhoanThuIdTienAnSua:
                        bangThuTienKhoanThuRow.SoTien = viewBangThuTienRow.TienAnSua;
                        break;
                    case BangThuTienConstant.KhoanThuIdPhuPhi:
                        bangThuTienKhoanThuRow.SoTien = viewBangThuTienRow.PhuPhi;
                        break;
                    case BangThuTienConstant.KhoanThuIdBanTru:
                        bangThuTienKhoanThuRow.SoTien = viewBangThuTienRow.BanTru;
                        break;
                    case BangThuTienConstant.KhoanThuIdHocPhi:
                        bangThuTienKhoanThuRow.SoTien = viewBangThuTienRow.HocPhi;
                        break;
                    case BangThuTienConstant.KhoanThuIdAnSang:
                        bangThuTienKhoanThuRow.SoTien = viewBangThuTienRow.SoTienAnSangThangNay;
                        break;
                    case BangThuTienConstant.KhoanThuIdAnToi:
                        bangThuTienKhoanThuRow.SoTien = viewBangThuTienRow.SoTienAnToiThangNay;
                        break;
                    case BangThuTienConstant.KhoanThuIdTienSua:
                        bangThuTienKhoanThuRow.SoTien = viewBangThuTienRow.TienSua;
                        break;
                    default:
                        break;
                }
            }
        }

        private void updateSoTienTruyThu()
        {
            QLMNDaoJobInvoker qlmnDaoJobInvoker = new QLMNDaoJobInvoker();
            qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand = new UpdateSoTienTruyThuOfBangThuTienCommand();
            CommandParameter commandParameter = qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand.CommandParameter;

            foreach (viewbangthutien viewBangThuTienRow in this.DataTable)
            {
                long originalVersionToCompare = viewBangThuTienRow.OriginalThanhTien;
                long currentVersionToCompare = viewBangThuTienRow.ThanhTien;

                if (originalVersionToCompare == currentVersionToCompare)
                {
                    continue;
                }

                // Indexer assignment, not Remove + Add: the command instance is reused across
                // iterations and Execute never clears its parameters.
                commandParameter[UpdateSoTienTruyThuOfBangThuTienCommand.ParameterEntities] = Entities;
                commandParameter[UpdateSoTienTruyThuOfBangThuTienCommand.ParameterCurrentViewBangThuTien] = viewBangThuTienRow;
                commandParameter[UpdateSoTienTruyThuOfBangThuTienCommand.ParameterSoTienAdded] = currentVersionToCompare - originalVersionToCompare;
                qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTien();

                // So that saving twice without an intervening reload cannot apply the delta again.
                viewBangThuTienRow.OriginalThanhTien = currentVersionToCompare;
            }
        }

        protected override void onSavingError(Exception ex)
        {
            LoggerFacade.Error(ex, AppForms.FormSoThuTien);
            LoggerFacade.Error(String.Format("Lưu sổ thu tiền không thành công: ngayTinh={0:MM/yyyy}, lop={1}", this.ngayTinh, this.lop),
                AppForms.FormSoThuTien);
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
            XtraMessageBox.Show(String.Format("Lưu sổ thu tiền không thành công.{0}{0}{1}", Environment.NewLine, ex.Message),
                "Lỗi khi lưu", MessageBoxButtons.OK, MessageBoxIcon.Error);

            base.onSavingError(ex);
        }

        protected override void reloadDataTable()
        {
            btnTimKiem_Click(null, null);
        }

        #endregion

        #region Validation

        #endregion

        #region Helper

        /// <summary>
        /// Columns of bangthutien that the grid can edit, copied back onto the writable
        /// bangthutien entity when saving. viewbangthutien itself is a read-only entity set.
        /// </summary>
        private static void copyToBangThuTien(viewbangthutien source, bangthutien target)
        {
            target.SXThangTruoc = source.SXThangTruoc;
            target.SoTienSXThangTruoc = source.SoTienSXThangTruoc;
            target.AnSangThangTruoc = source.AnSangThangTruoc;
            target.SoTienAnSangThangTruoc = source.SoTienAnSangThangTruoc;
            target.SoTienAnSangThangNay = source.SoTienAnSangThangNay;
            target.AnToiThangTruoc = source.AnToiThangTruoc;
            target.SoTienAnToiThangTruoc = source.SoTienAnToiThangTruoc;
            target.SoTienAnToiThangNay = source.SoTienAnToiThangNay;
            target.SoTienNangKhieu = source.SoTienNangKhieu;
            target.SoTienTruyThu = source.SoTienTruyThu;
            target.SoTienDoDung = source.SoTienDoDung;
            target.SoTienDieuHoa = source.SoTienDieuHoa;
            target.STT = source.STT;
            target.GhiChu = source.GhiChu;
        }

        private DXMenuItem CreateSubMenuRows(GridView view, int rowHandle)
        {
            DXMenuItem menuItemDeleteRow = new DXMenuItem("Xóa dòng này", new EventHandler(onDeleteRowClick), imageCollection1.Images[0]);

            // Resolve the row BEFORE using it: GetRowCellValue returns null for group, footer and
            // new-item rows, and unboxing that null threw.
            viewbangthutien row = view.IsDataRow(rowHandle) ? view.GetRow(rowHandle) as viewbangthutien : null;

            if (row == null)
            {
                menuItemDeleteRow.Enabled = false;
                return menuItemDeleteRow;
            }

            menuItemDeleteRow.Tag = new RowInfo(row, row.BangThuTienId);
            menuItemDeleteRow.Enabled = true;
            return menuItemDeleteRow;
        }

        private void onDeleteRowClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = sender as DXMenuItem;

            if (menuItem == null)
            {
                return;
            }

            RowInfo ri = menuItem.Tag as RowInfo;

            if (ri == null)
            {
                return;
            }

            string message = menuItem.Caption.Replace("&", "");

            if (XtraMessageBox.Show(message + " ?", "Confirm operation", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Delete BangThuTien and relative records from DB first: if either delete fails the
                // grid must keep showing the row instead of silently diverging from the database.
                using (DbContextTransaction transaction = Entities.Database.BeginTransaction())
                {
                    Entities.deleteBangThuTienKhoanThuByBangThuTienId(ri.BangThuTienId);
                    Entities.deleteBangThuTienById(ri.BangThuTienId);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                LoggerFacade.Error(ex, AppForms.FormSoThuTien);
                LoggerFacade.Error(String.Format("Xóa bảng thu tiền không thành công: BangThuTienId={0}", ri.BangThuTienId), AppForms.FormSoThuTien);
                XtraMessageBox.Show(String.Format("Xóa dòng này không thành công.{0}{0}{1}", Environment.NewLine, ex.Message),
                    "Lỗi khi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.DataTable != null)
            {
                this.DataTable.Remove(ri.Row);
            }

            if (this.bTTKTDataTable != null)
            {
                this.bTTKTDataTable.RemoveAll(b => b.BangThuTienId == ri.BangThuTienId);
            }

            if (this.prevMonthRowDictionary != null)
            {
                this.prevMonthRowDictionary.Remove(ri.Row.HocSinhId);
            }

            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.DeletedCaption);
        }

        private bool isDataTableEmpty()
        {
            if (this.DataTable == null || this.DataTable.Count == 0)
            {
                MessageBox.Show("Danh sách sổ thu tiền trống", "Danh sách trống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            return false;
        }

        private void loadLopData()
        {
            // StaticDataFacade hands out the one cached list instance that every other form binds
            // to, so this must not mutate the lop rows. This form only needs LopId and Name.
            List<lop> dataTable = StaticDataFacade.Get(StaticDataKeys.LopHoc) as List<lop>;

            if (dataTable == null)
            {
                return;
            }

            this.lopRowBindingSource.DataSource = dataTable;
        }

        private void loadHocSinhData()
        {
            hocSinhDataTable = Entities.hocsinhs.ToList();
        }

        private void loadViewBangThuTiens(DateTime ngayTinh, int lop)
        {
            if (this.isNeedToGenerateSoThuTiens(ngayTinh, lop, false))
            {
                // Clear first: otherwise the grid, the print buttons and the cell recalculations
                // keep working on the previously loaded class while the combos show another one.
                this.resetPerSearchCaches();
                BindingList<viewbangthutien> emptyTable = new BindingList<viewbangthutien>();
                this.viewBangThuTienRowBindingSource.DataSource = emptyTable;
                this.DataTable = emptyTable;
                this.showFormGenerateSoThuTiens(true);
                return;
            }

            this.ShowGridLoadingPanel();
            this.gvMain.BeginUpdate();

            try
            {
                // NoTracking: viewbangthutien is a read-only entity set and these rows are
                // recalculated in memory below. Leaving them tracked made Huỷ Bỏ a no-op, because
                // a reload with the default MergeOption.AppendOnly returns the same dirty
                // instances, and made SaveChanges try to UPDATE the view.
                BindingList<viewbangthutien> table = new BindingList<viewbangthutien>(
                    Entities.getViewBangThuTienByNgayTinhAndLop(ngayTinh, lop, MergeOption.NoTracking).ToList());
                List<int> bangThuTienIds = new List<int>(table.Count);

                foreach (viewbangthutien row in table)
                {
                    bangThuTienIds.Add(row.BangThuTienId);
                }

                this.resetPerSearchCaches();

                if (!ListUtil.IsEmpty(bangThuTienIds))
                {
                    bTTKTDataTable = Entities.getBangThuTienKhoanThuByBangThuTienIds(String.Join(",", bangThuTienIds), MergeOption.NoTracking).ToList();
                    phieuThuDataTable = Entities.getPhieuThuByHocSinhIdAndNgayTinh(-1, ngayTinh).ToList();

                    SoThuTienService soThuTienService = new SoThuTienService();
                    prevMonthRowDictionary = soThuTienService.EvaluatePrevMonthViewBangThuTienTable(Entities, ngayTinh.AddMonths(-1), lop);

                    foreach (viewbangthutien row in table)
                    {
                        row.Ten = StaticDataUtil.GetHocSinhNameByHocSinhId(hocSinhDataTable, row.HocSinhId);
                        row.HoTen = StaticDataUtil.GetHocSinhFullNameByHocSinhId(hocSinhDataTable, row.HocSinhId);

                        hocsinh hocSinh = StaticDataUtil.GetHocSinhByHocSinhId(hocSinhDataTable, row.HocSinhId);
                        if (hocSinh != null)
                        {
                            row.SoDienThoai = hocSinh.DienThoai;
                            if (string.IsNullOrWhiteSpace(row.SoDienThoai))
                            {
                                row.SoDienThoai = hocSinh.DienThoaiMe;
                            }
                        }

                        BangThuTienUtil.EvaluateValuesForViewBangThuTienRow(row,
                            prevMonthRowDictionary != null && prevMonthRowDictionary.ContainsKey(row.HocSinhId) ? prevMonthRowDictionary[row.HocSinhId] : null,
                            bTTKTDataTable, phieuThuDataTable, false, false, true, this.genHistoryCache);
                        row.PhucVuBanTru = row.HocPhi + row.BanTru;
                        row.OriginalThanhTien = row.ThanhTien;
                    }
                }

                this.viewBangThuTienRowBindingSource.DataSource = table;
                this.DataTable = table;
            }
            finally
            {
                this.gvMain.EndUpdate();
                this.HideGridLoadingPanel();
            }
        }

        /// <summary>
        /// Drops every cache tied to the previous search. The collections are emptied rather than
        /// nulled because BangThuTienUtil indexes into them without a null check.
        /// </summary>
        private void resetPerSearchCaches()
        {
            this.bTTKTDataTable = new List<bangthutien_khoanthu>();
            this.phieuThuDataTable = new List<phieuthu>();
            this.prevMonthRowDictionary = new Dictionary<int, viewbangthutien>();
            this.genHistoryCache = new BangThuTienGenHistoryCache(Entities);
        }

        #endregion
    }
}