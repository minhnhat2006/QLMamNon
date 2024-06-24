using ACG.Core.WinForm.Util;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Properties;
using QLMamNon.Reports;
using QLMamNon.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace QLMamNon.Forms.HocSinh
{
    public partial class FrmThongTinHocSinh : CRUDForm<hocsinh>
    {
        #region Properties

        private static readonly int[] HidePhuPhiColumns = new int[] { 3 };

        #endregion

        #region Events

        public FrmThongTinHocSinh()
            : base()
        {
            InitializeComponent();

            this.TablePrimaryKey = "HocSinhId";
            this.DanhMuc = DanhMucConstant.ThongTinHocSinh;
            this.FormKey = AppForms.FormThongTinHocSinh;

            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormThongTinHocSinh();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.hocSinhRowBindingSource.DataSource);
        }

        private void FrmThongTinHocSinh_Load(object sender, EventArgs e)
        {
            this.quanHuyenRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.QuanHuyen);
            this.lopRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.LopHoc);
            this.keyValuePairBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.TrangThaiHS);
            this.cmbThoiHoc.EditValue = 0;
            this.initCmbNamSinh();
            this.loadThongTinHocSinh(Entities, null, null, null, null, null, null, false);
        }

        private void initCmbNamSinh()
        {
            List<int> namSinhs = new List<int>();

            for (int i = Settings.Default.NamSinhStart; i < DateTime.Now.Year; i++)
            {
                namSinhs.Add(i);
            }

            this.cmbNamSinh.Properties.Items.AddRange(namSinhs);
        }

        private void gcMain_Load(object sender, EventArgs e)
        {
            if (!Settings.Default.IsMNHongMinh)
            {
                foreach (int columnIndex in HidePhuPhiColumns)
                {
                    gvMain.Columns[columnIndex].Visible = false;

                    if (gvMain.Columns[columnIndex].OwnerBand != null)
                    {
                        gvMain.Columns[columnIndex].OwnerBand.Visible = false;

                        if (gvMain.Columns[columnIndex].OwnerBand.ParentBand != null)
                        {
                            gvMain.Columns[columnIndex].OwnerBand.ParentBand.Visible = false;
                        }
                    }
                }

                // Họ Tên Cha
                gvMain.Columns["HoTenCha"].OwnerBand.Visible = false;
                // Điện Thoại Cha
                gvMain.Columns["DienThoai"].OwnerBand.Caption = "Điện thoại cha";
                // Họ Tên Mẹ
                gvMain.Columns["HoTenMe"].OwnerBand.Visible = false;
                // Điện Thoại Mẹ
                gvMain.Columns["DienThoaiMe"].OwnerBand.Caption = "Điện thoại mẹ";
            }
        }

        private void gvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "GioiTinh" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object objGioiTinh = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "GioiTinh");
                if (objGioiTinh != null)
                {
                    sbyte gioiTinh = (sbyte)objGioiTinh;
                    switch (gioiTinh)
                    {
                        case 0: e.DisplayText = "Nữ"; break;
                        case 1: e.DisplayText = "Nam"; break;
                    }
                }
            }
            else if (e.Column.FieldName == "ThoiHoc" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                Object thoiHoc = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "ThoiHoc");
                e.DisplayText = (thoiHoc != null && thoiHoc.Equals(true)) ? "Thôi học" : "Đang học";
            }
            else if (e.Column.FieldName == "TinhThanhPhoId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object tinhId = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "TinhThanhPhoId");
                if (tinhId != null)
                {
                    e.DisplayText = StaticDataUtil.GetThanhPhoById((List<thanhpho>)StaticDataFacade.Get(StaticDataKeys.TinhThanhPho), (int)tinhId);
                }
            }
            else if (e.Column.FieldName == "QuanHuyenId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object quanHuyenId = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "QuanHuyenId");
                if (quanHuyenId != null)
                {
                    e.DisplayText = StaticDataUtil.GetQuanHuyenById((List<quanhuyen>)StaticDataFacade.Get(StaticDataKeys.QuanHuyen), (int)quanHuyenId);
                }
            }
            else if (e.Column.FieldName == "PhuongXaId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object phuongXaId = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "PhuongXaId");
                if (phuongXaId != null)
                {
                    e.DisplayText = StaticDataUtil.GetPhuongXaById((List<phuongxa>)StaticDataFacade.Get(StaticDataKeys.PhuongXa), (int)phuongXaId);
                }
            }
            else if (e.Column.Caption == "NgaySinh" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object ngaySinhObj = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "NgaySinh");
                if (ngaySinhObj != null)
                {
                    DateTime ngaySinh = (DateTime)ngaySinhObj;
                    e.DisplayText = ngaySinh.Day.ToString();
                }
            }
            else if (e.Column.Caption == "ThangSinh" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object ngaySinhObj = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "NgaySinh");
                if (ngaySinhObj != null)
                {
                    DateTime ngaySinh = (DateTime)ngaySinhObj;
                    e.DisplayText = ngaySinh.Month.ToString();
                }
            }
            else if (e.Column.Caption == "NamSinh" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object ngaySinhObj = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "NgaySinh");
                if (ngaySinhObj != null)
                {
                    DateTime ngaySinh = (DateTime)ngaySinhObj;
                    e.DisplayText = ngaySinh.Year.ToString();
                }
            }
        }

        private void cmbQuanHuyen_EditValueChanged(object sender, EventArgs e)
        {
            List<phuongxa> table = (List<phuongxa>)StaticDataFacade.Get(StaticDataKeys.PhuongXa);
            if (cmbQuanHuyen.EditValue != null && cmbQuanHuyen.EditValue != null)
            {
                this.phuongXaRowBindingSource.DataSource = table.FindAll(p => p.QuanHuyenId == (int)cmbQuanHuyen.EditValue);
            }
            else
            {
                this.phuongXaRowBindingSource.DataSource = null;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.loadThongTinHocSinh(Entities, (int?)cmbQuanHuyen.EditValue, (int?)cmbPhuongXa.EditValue, (int?)cmbLop.EditValue, IntUtil.StringToInt(cmbThang.Text),
                IntUtil.StringToInt(cmbNamSinh.Text), (DateTime?)cmbNgaySinh.EditValue, (int?)cmbThoiHoc.EditValue == 1);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.cmbQuanHuyen.EditValue = null;
            this.cmbPhuongXa.EditValue = null;
            this.cmbLop.EditValue = null;
            this.cmbThang.EditValue = null;
            this.cmbNamSinh.EditValue = null;
            this.cmbThoiHoc.EditValue = null;
            this.cmbNgaySinh.EditValue = null;
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            dialog.FileName = "ThongTinHocSinh.xlsx";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.gvMain.ExportToXlsx(dialog.FileName, new XlsxExportOptions(TextExportMode.Text));
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            RptThongTinHocSinh rpt = new RptThongTinHocSinh();
            this.fillReportHocSinh(rpt);
            FormMainFacade.ShowReport(rpt);
        }

        #endregion

        #region Validation

        #endregion

        #region Helper

        private void loadThongTinHocSinh(qlmamnonEntities entities, int? quan, int? phuong, int? lop, int? thangSinh, int? namSinh, DateTime? ngaySinh, bool? thoiHoc)
        {
            List<hocsinh> hocSinhTable = entities.getHocSinhForThongTinHocSinh(quan, phuong, ngaySinh, thangSinh, lop, thoiHoc, namSinh).ToList();
            ThongTinHocSinhUtil.EvaluateLopInfoForHocSinhTable(entities, hocSinhTable);
            this.DataTable = new BindingList<hocsinh>(hocSinhTable);
            this.hocSinhRowBindingSource.DataSource = this.DataTable;
        }

        private void fillReportHocSinh(RptThongTinHocSinh rpt)
        {
            List<object> rows = new List<object>(this.GridViewMain.RowCount);

            for (int i = 0; i < this.GridViewMain.DataRowCount; i++)
            {
                object dataRow = this.GridViewMain.GetRow(this.GridViewMain.GetVisibleRowHandle(i));
                rows.Add(dataRow);

                hocsinh hocSinhRow = dataRow as hocsinh;
                hocSinhRow.STT = i + 1;

                if (hocSinhRow.PhuongXaId != null)
                {
                    hocSinhRow.PhuongXa = StaticDataUtil.GetPhuongXaById((List<phuongxa>)StaticDataFacade.Get(StaticDataKeys.PhuongXa), hocSinhRow.PhuongXaId.Value);
                }

                if (hocSinhRow.QuanHuyenId != null)
                {
                    hocSinhRow.QuanHuyen = StaticDataUtil.GetQuanHuyenById((List<quanhuyen>)StaticDataFacade.Get(StaticDataKeys.QuanHuyen), hocSinhRow.QuanHuyenId.Value);
                }
            }

            rpt.hocSinhBindingSource.DataSource = rows;
        }

        #endregion
    }
}