using ACG.Core.WinForm.Util;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmSoThuTien : CRUDForm<viewbangthutien>
    {
        private class RowInfo
        {
            public RowInfo(GridView view, int rowHandle, int bangThuTienId)
            {
                this.RowHandle = rowHandle;
                this.View = view;
                this.BangThuTienId = bangThuTienId;
            }
            public GridView View;
            public int RowHandle;
            public int BangThuTienId;
        }

        #region Properties

        private List<hocsinh> hocSinhDataTable;
        private List<bangthutien_khoanthu> bTTKTDataTable;
        private Dictionary<int, viewbangthutien> prevMonthRowDictionary;
        private List<phieuthu> phieuThuDataTable;
        private DateTime ngayTinh;
        private int lop;

        private static readonly string[] HidePhuPhiColumns = new string[]
        {
            "TienAnSua",
            "TienSua",
            "PhuPhi",
            "KhoanThuChinh",
            "SXThangTruoc",
            "SoTienSXThangTruoc",
            "AnSangThangTruoc",
            "SoTienAnSangThangTruoc",
            "SoTienAnSangThangNay",
            "SoTienAnSangConLai",
            "AnToiThangTruoc",
            "SoTienAnToiThangTruoc",
            "SoTienAnToiThangNay",
            "SoTienAnToiConLai",
            "SoTienDoDung",
            "SoTienDieuHoa",
            "SoTienNangKhieu"
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
            this.InitForm(null, null, null, this.btnLuu, this.btnHuyBo, this.gvMain, this.viewBangThuTienRowBindingSource.DataSource);
        }

        private void FrmSoThuTien_Load(object sender, EventArgs e)
        {
            this.lopRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.LopHoc);
            this.namHocBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.NamHoc);
            this.cmbNam.Text = DateTime.Now.Year.ToString();
            this.cmbThang.Text = DateTime.Now.Month.ToString();
        }

        protected override void onCanceling()
        {
            btnTimKiem.PerformClick();
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.CanceledCaption);
        }

        private void gcMain_Enter(object sender, EventArgs e)
        {
            if (!Settings.Default.IsMNHongMinh)
            {
                foreach (string columnName in HidePhuPhiColumns)
                {
                    gvMain.Columns[columnName].Visible = false;
                    gvMain.Columns[columnName].OwnerBand.Visible = false;

                    if (gvMain.Columns[columnName].OwnerBand.ParentBand != null)
                    {
                        gvMain.Columns[columnName].OwnerBand.ParentBand.Visible = false;
                    }
                }

                // Change column captiom PhucVuBanTru to "Học Phí"
                gvMain.Columns["PhucVuBanTru"].OwnerBand.Caption = "Học Phí";

                gvMain.Columns[19].Visible = false;
                gvMain.Columns[19].OwnerBand.Visible = false;
                // btnPrint1
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // btnPrint2
                layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // btnPrintGiayBaoNopTien
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // btnPrint
                layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void gcMain_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            BandedGridHitInfo hit = gvMain.CalcHitInfo(e.Location);

            GridBand gridBandHoTen = gridBand17;
            GridBand gridBandSTT = gridBand16;

            if (hit != null && hit.Band == gridBandHoTen)
            {
                if (hit.HitTest == BandedGridHitTest.Band)
                {
                    gridBandSTT.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.None;
                    if (gridBandHoTen.Columns[1].SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
                        gridBandHoTen.Columns[1].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    else
                        gridBandHoTen.Columns[1].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                }
            }
            else if (hit != null && hit.Band == gridBandSTT)
            {
                if (hit.HitTest == BandedGridHitTest.Band)
                {
                    gridBandHoTen.Columns[1].SortOrder = DevExpress.Data.ColumnSortOrder.None;
                    if (gridBandSTT.Columns[0].SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
                        gridBandSTT.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    else
                        gridBandSTT.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                }
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
            List<string> fieldsToEvaluate = new List<string>() {
                ViewBangThuTienFieldName.SXThangTruoc,
                ViewBangThuTienFieldName.AnSangThangTruoc,
                ViewBangThuTienFieldName.SoTienAnSangThangNay,
                ViewBangThuTienFieldName.AnToiThangTruoc,
                ViewBangThuTienFieldName.SoTienAnToiThangNay,
                ViewBangThuTienFieldName.SoTienNangKhieu,
                ViewBangThuTienFieldName.SoTienTruyThu,
                ViewBangThuTienFieldName.SoTienDieuHoa,
                ViewBangThuTienFieldName.SoTienDoDung,
                ViewBangThuTienFieldName.TienAnVaSua,
                ViewBangThuTienFieldName.TienSua,
                ViewBangThuTienFieldName.PhuPhi,
                ViewBangThuTienFieldName.BanTru,
                ViewBangThuTienFieldName.HocPhi,
                ViewBangThuTienFieldName.PhucVuBanTru
            };

            // Recalculate BanTru
            if (ViewBangThuTienFieldName.PhucVuBanTru == e.Column.FieldName && e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                if (e.Value != null)
                {
                    viewbangthutien row = gv.GetRow(e.RowHandle) as viewbangthutien;
                    row.BanTru = row.PhucVuBanTru - row.HocPhi;
                }
            }

            if (fieldsToEvaluate.Contains(e.Column.FieldName) && e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                if (e.Value != null)
                {
                    viewbangthutien row = gv.GetRow(e.RowHandle) as viewbangthutien;
                    BangThuTienUtil.EvaluateValuesForViewBangThuTienRow(row,
                        prevMonthRowDictionary != null && prevMonthRowDictionary.ContainsKey(row.HocSinhId) ? prevMonthRowDictionary[row.HocSinhId] : null,
                        bTTKTDataTable, phieuThuDataTable, true, false, false);

                    if (ViewBangThuTienFieldName.SXThangTruoc.Equals(e.Column.FieldName))
                    {
                        BangThuTienUtil.RecalculateBangThuTienKhoanThuList(Entities, row);
                    }
                }
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
            this.ngayTinh = this.GetNgayTinh();
            this.lop = (int)this.cmbLop.EditValue;
        }

        private DateTime GetNgayTinh()
        {
            if (!ControlUtil.IsEditValueNull(this.cmbNam) && !ControlUtil.IsEditValueNull(this.cmbThang))
            {
                Int32 nam = (Int32)cmbNam.EditValue;
                int thang = IntUtil.StringToInt((string)cmbThang.EditValue).Value;
                int ngay = DateTime.DaysInMonth(nam, thang);
                DateTime ngayTinh = new DateTime(nam, thang, ngay);
                return ngayTinh;
            }

            return DateTime.MinValue;
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
                RptSoThuTien rpt = new RptSoThuTien();
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
            List<int> bangThuTienIds = new List<int>();

            foreach (viewbangthutien viewBangThuTienRow in DataTable)
            {
                viewBangThuTienRow.SoTienSXThangTruoc = BangThuTienUtil.SXToSoTienSX(viewBangThuTienRow.SXThangTruoc.Value, Settings.Default.TienAnChinh);
                viewBangThuTienRow.SoTienAnSangThangTruoc = BangThuTienUtil.SXAnSangToSoTienAnSang(viewBangThuTienRow.AnSangThangTruoc.Value, Settings.Default.TienAnSang);
                viewBangThuTienRow.SoTienAnToiThangTruoc = BangThuTienUtil.SXAnToiToSoTienAnToi(viewBangThuTienRow.AnToiThangTruoc, Settings.Default.TienAnToi);

                if (this.isNeedToUpdateBangThuTienKhoanThu(viewBangThuTienRow))
                {
                    bangThuTienIds.Add(viewBangThuTienRow.BangThuTienId);
                }
            }

            if (!ListUtil.IsEmpty(bangThuTienIds))
            {
                List<bangthutien_khoanthu> bangThuTienKhoanThuDataTable = Entities.getBangThuTienKhoanThuByBangThuTienIds(StringUtil.Join(bangThuTienIds, ",")).ToList();

                // If KhoanThuId does not exist in bangThuTienKhoanThuDataTable then adding it
                foreach (int bangThuTienId in bangThuTienIds)
                {
                    List<int> khoanThuIds = new List<int>() { BangThuTienConstant.KhoanThuIdAnSang, BangThuTienConstant.KhoanThuIdAnToi, BangThuTienConstant.KhoanThuIdBanTru, BangThuTienConstant.KhoanThuIdHocPhi, BangThuTienConstant.KhoanThuIdPhuPhi, BangThuTienConstant.KhoanThuIdTienAnSua, BangThuTienConstant.KhoanThuIdTienSua };
                    List<bangthutien_khoanthu> bangThuTienKhoanThuRows = bangThuTienKhoanThuDataTable.FindAll(b => b.BangThuTienId == bangThuTienId);
                    foreach (bangthutien_khoanthu bangThuTienKhoanThuRow in bangThuTienKhoanThuRows)
                    {
                        khoanThuIds.Remove(bangThuTienKhoanThuRow.KhoanThuId);
                    }

                    foreach (int khoanThuId in khoanThuIds)
                    {
                        bangthutien_khoanthu bangThuTienKhoanThu = new bangthutien_khoanthu()
                        {
                            BangThuTienId = bangThuTienId,
                            KhoanThuId = khoanThuId,
                            SoTien = 0
                        };
                        Entities.bangthutien_khoanthu.Add(bangThuTienKhoanThu);
                    }
                }

                // Update SoTien
                foreach (bangthutien_khoanthu bangThuTienKhoanThuRow in bangThuTienKhoanThuDataTable)
                {
                    List<viewbangthutien> viewBangThuTienRows = new List<viewbangthutien>(DataTable.Where(v => v.BangThuTienId == bangThuTienKhoanThuRow.BangThuTienId));
                    viewbangthutien viewBangThuTienRow = viewBangThuTienRows[0];
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

                Entities.SaveChanges();
            }

            // Start Updating SoTienTruyThu for each BangThuTienRow
            QLMNDaoJobInvoker qlmnDaoJobInvoker = new QLMNDaoJobInvoker();
            qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand = new UpdateSoTienTruyThuOfBangThuTienCommand();

            foreach (viewbangthutien viewBangThuTienRow in DataTable)
            {
                long originalVersionToCompare = viewBangThuTienRow.OriginalThanhTien;
                long currentVersionToCompare = viewBangThuTienRow.ThanhTien;

                if (originalVersionToCompare != currentVersionToCompare)
                {
                    qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand.CommandParameter.Remove(UpdateSoTienTruyThuOfBangThuTienCommand.ParameterCurrentViewBangThuTien);
                    qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand.CommandParameter.Remove(UpdateSoTienTruyThuOfBangThuTienCommand.ParameterSoTienAdded);
                    qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand.CommandParameter.Add(UpdateSoTienTruyThuOfBangThuTienCommand.ParameterCurrentViewBangThuTien, viewBangThuTienRow);
                    qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTienCommand.CommandParameter.Add(UpdateSoTienTruyThuOfBangThuTienCommand.ParameterSoTienAdded, currentVersionToCompare - originalVersionToCompare);
                    qlmnDaoJobInvoker.UpdateSoTienTruyThuOfBangThuTien();
                }
            }
            // Finished Updating SoTienTruyThu for each BangThuTien

            base.onSaving();

            btnTimKiem_Click(null, null);
        }

        #endregion

        #region Validation

        #endregion

        #region Helper

        private DXMenuItem CreateSubMenuRows(GridView view, int rowHandle)
        {
            DXMenuItem menuItemDeleteRow = new DXMenuItem("Xóa dòng này", new EventHandler(onDeleteRowClick), imageCollection1.Images[0]);
            int bangThuTienId = (int)view.GetRowCellValue(rowHandle, "BangThuTienId");
            menuItemDeleteRow.Tag = new RowInfo(view, rowHandle, bangThuTienId);
            menuItemDeleteRow.Enabled = view.IsDataRow(rowHandle);
            return menuItemDeleteRow;
        }

        private void onDeleteRowClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = sender as DXMenuItem;
            RowInfo ri = menuItem.Tag as RowInfo;
            if (ri != null)
            {
                string message = menuItem.Caption.Replace("&", "");
                if (XtraMessageBox.Show(message + " ?", "Confirm operation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                ri.View.DeleteRow(ri.RowHandle);
                // Delete BangThuTien and relative records from DB
                Entities.deleteBangThuTienKhoanThuByBangThuTienId(ri.BangThuTienId);
                Entities.deleteBangThuTienById(ri.BangThuTienId);
            }
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

        private bool isNeedToUpdateBangThuTienKhoanThu(viewbangthutien viewBangThuTienRow)
        {
            /*List<string> fieldsToCheck = new List<string>() { ViewBangThuTienFieldName.SXThangTruoc,
                ViewBangThuTienFieldName.TienAnVaSua,
                ViewBangThuTienFieldName.TienSua,
                ViewBangThuTienFieldName.PhuPhi,
                ViewBangThuTienFieldName.BanTru,
                ViewBangThuTienFieldName.HocPhi,
                ViewBangThuTienFieldName.SoTienAnSangThangNay,
                ViewBangThuTienFieldName.SoTienAnToiThangNay
            };

            foreach (String field in fieldsToCheck)
            {
                object original = Entities.Entry(viewBangThuTienRow).OriginalValues[field];
                object current = Entities.Entry(viewBangThuTienRow).CurrentValues[field];
                bool isChanged = !ObjectUtil.equals(original, current);

                if (isChanged)
                {
                    return true;
                }
            }

            return false;*/
            return true;
        }

        private void loadLopData()
        {
            List<lop> dataTable = StaticDataFacade.Get(StaticDataKeys.LopHoc) as List<lop>;

            foreach (lop row in dataTable)
            {
                int? khoiId = StaticDataUtil.GetKhoiIdByLopId(Entities, row.LopId);
                if (khoiId.HasValue)
                {
                    row.KhoiId = khoiId.Value;
                }
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
                this.showFormGenerateSoThuTiens(true);
                return;
            }

            BindingList<viewbangthutien> table = new BindingList<viewbangthutien>(Entities.getViewBangThuTienByNgayTinhAndLop(ngayTinh, lop).ToList());
            List<int> bangThuTienIds = new List<int>(table.Count);

            foreach (viewbangthutien row in table)
            {
                bangThuTienIds.Add(row.BangThuTienId);
            }

            if (!ListUtil.IsEmpty(bangThuTienIds))
            {
                bTTKTDataTable = Entities.getBangThuTienKhoanThuByBangThuTienIds(String.Join(",", bangThuTienIds)).ToList();
                phieuThuDataTable = Entities.getPhieuThuByHocSinhIdAndNgayTinh(-1, ngayTinh).ToList();

                SoThuTienService soThuTienService = new SoThuTienService();
                prevMonthRowDictionary = soThuTienService.EvaluatePrevMonthViewBangThuTienTable(Entities, ngayTinh.AddMonths(-1), lop);

                foreach (viewbangthutien row in table)
                {
                    row.Ten = StaticDataUtil.GetHocSinhNameByHocSinhId(hocSinhDataTable, row.HocSinhId);
                    row.HoTen = StaticDataUtil.GetHocSinhFullNameByHocSinhId(hocSinhDataTable, row.HocSinhId);
                    BangThuTienUtil.EvaluateValuesForViewBangThuTienRow(row,
                        prevMonthRowDictionary != null && prevMonthRowDictionary.ContainsKey(row.HocSinhId) ? prevMonthRowDictionary[row.HocSinhId] : null,
                        bTTKTDataTable, phieuThuDataTable, false, false, true);
                    row.PhucVuBanTru = row.HocPhi + row.BanTru;
                    row.OriginalThanhTien = row.ThanhTien;
                }
            }

            this.viewBangThuTienRowBindingSource.DataSource = table;
            this.DataTable = table;
        }

        #endregion
    }
}