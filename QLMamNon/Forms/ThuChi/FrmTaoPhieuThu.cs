using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Properties;
using QLMamNon.Reports;
using QLMamNon.Service.Data;
using LayoutVisibility = DevExpress.XtraLayout.Utils.LayoutVisibility;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmTaoPhieuThu : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        public GridView GridView { get; set; }

        public bool IsEditing { get; set; }

        public phieuthu PhieuThuRow { get; set; }

        private Dictionary<int, BangKeThuTienItem> hocSinhIdToBangKeThuTienItems;

        private qlmamnonEntities entities;

        #endregion

        public FrmTaoPhieuThu()
        {
            this.FormKey = AppForms.FormTaoPhieuThu;
            InitializeComponent();
        }

        private void FrmTaoPhieuThu_Load(object sender, EventArgs e)
        {
            entities = StaticDataFacade.GetQLMNEntities();
            List<hocsinh> hocSinhTable = entities.getHocSinhForThongTinHocSinh(null, null, null, null, null, false, null).ToList();
            ThongTinHocSinhUtil.EvaluateLopInfoForHocSinhTable(entities, hocSinhTable);
            this.hocSinhRowBindingSource.DataSource = hocSinhTable;
            this.hocSinhRowBindingSource.Filter = "LopDangHoc IS NOT NULL";
            this.phanLoaiThuRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.PhanLoaiThu);

            SoThuTienService soThuTienService = new SoThuTienService();
            DateTime endOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            List<BangKeThuTienItem> allBangKeThuTienItems = soThuTienService.GetBangKeTongHopThuTienForTaoPhieuThu(entities, endOfMonth, null);

            hocSinhIdToBangKeThuTienItems = new Dictionary<int, BangKeThuTienItem>();
            foreach (var item in allBangKeThuTienItems)
            {
                hocSinhIdToBangKeThuTienItems.Add(item.HocSinhId, item);
            }

            if (this.IsEditing)
            {
                loadPhieuThu();
            }
            else
            {
                resetForm();
            }
        }

        private void FrmTaoPhieuThu_Enter(object sender, EventArgs e)
        {
            int hocSinhId = (int)this.GridView.GetFocusedRowCellValue("HocSinhId");
            int phanLoaiThuId = (int)this.GridView.GetFocusedRowCellValue("PhanLoaiThuId");
            this.cmbHocSinh.EditValue = hocSinhId;
            this.cmbPhanLoaiThu.EditValue = phanLoaiThuId;
        }

        protected void FrmTaoPhieuThu_Activated(object sender, EventArgs e)
        {
            FormMainFacade.SetFormCaption(this.FormKey);
        }

        private void FrmTaoPhieuThu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.IsEditing = false;
        }

        private void btnLuuTao_Click(object sender, EventArgs e)
        {
            if (this.onSavePhieuThu())
            {
                this.btnIn_Click(sender, e);
                this.resetForm();
                FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedAndAddingPhieuThuCaption);
                this.IsEditing = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (this.onSavePhieuThu())
            {
                this.Close();
                FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedPhieuThuCaption);
            }
        }

        private bool onSavePhieuThu()
        {
            if (!this.dxValidationProvider.Validate())
            {
                return false;
            }

            this.luuPhieuThu();

            if (isSelectedPhanLoaiThuByHocSinh())
            {
                this.updateSoTienTruyThuForBangThuTienNextMonths(this.dateNgay.DateTime, (int)this.cmbHocSinh.EditValue);
            }

            return true;
        }

        private void loadPhieuThu()
        {
            this.dateNgay.DateTime = this.PhieuThuRow.Ngay;
            this.txtSoTien.Value = this.PhieuThuRow.SoTien;
            this.txtMaPhieu.Text = this.PhieuThuRow.MaPhieu;
            this.txtGhiChu.Text = this.PhieuThuRow.GhiChu;
            this.cmbHocSinh.EditValue = this.PhieuThuRow.HocSinhId;
            this.cmbPhanLoaiThu.EditValue = this.PhieuThuRow.PhanLoaiThuId;
        }

        private void luuPhieuThu()
        {
            if (this.IsEditing)
            {
                this.updatePhieuThu();
            }
            else
            {
                this.insertPhieuThu();
            }

            if (this.GridView != null)
            {
                BindingSource phieuThuBindingSource = this.GridView.GridControl.DataSource as BindingSource;
                PhieuThuService phieuThuService = new PhieuThuService();
                entities.hocsinhs.Load();
                phieuThuBindingSource.DataSource = phieuThuService.LoadPhieuThu(entities.hocsinhs.Local.ToBindingList());
            }
        }

        private void insertPhieuThu()
        {
            DateTime ngay = this.dateNgay.DateTime;
            long soTien = (long)this.txtSoTien.Value;
            string maPhieu = this.txtMaPhieu.Text;
            string ghiChu = this.txtGhiChu.Text;
            int? hocSinhId = (int?)this.cmbHocSinh.EditValue;
            int? phanLoaiThuId = (int?)this.cmbPhanLoaiThu.EditValue;
            PhieuThuService phieuThuService = new PhieuThuService();
            phieuThuService.InsertPhieuThu(ngay, soTien, maPhieu, ghiChu, hocSinhId, phanLoaiThuId);

            Settings.Default["LastMaPhieuThu"] = Settings.Default.LastMaPhieuThu + 1;
            Settings.Default.Save();
        }

        private void updatePhieuThu()
        {
            DateTime ngay = this.dateNgay.DateTime;
            long soTien = (long)this.txtSoTien.Value;
            string maPhieu = this.txtMaPhieu.Text;
            string ghiChu = this.txtGhiChu.Text;
            int? hocSinhId = (int?)this.cmbHocSinh.EditValue;
            int? phanLoaiThuId = (int?)this.cmbPhanLoaiThu.EditValue;
            PhieuThuService phieuThuService = new PhieuThuService();
            phieuThuService.UpdatePhieuThu(this.PhieuThuRow, ngay, soTien, maPhieu, ghiChu, hocSinhId, phanLoaiThuId);
        }

        private void resetForm()
        {
            this.txtSoTien.Value = 0;
            this.txtMaPhieu.Text = $"{Settings.Default.LastMaPhieuThu + 1}";
            this.txtGhiChu.Text = "";
            this.txtConLai.Text = "";
            this.cmbHocSinh.EditValue = null;
        }

        private void cmbPhanLoaiThu_EditValueChanged(object sender, EventArgs e)
        {
            LayoutVisibility enableCmbHocSinhId = isSelectedPhanLoaiThuByHocSinh() ? LayoutVisibility.Always : LayoutVisibility.Never;
            lciCmbHocSinh.Visibility = enableCmbHocSinhId;
            lciTxtConLai.Visibility = enableCmbHocSinhId;
        }

        private bool isSelectedPhanLoaiThuByHocSinh()
        {
            string selectedPhanLoaiThuId = string.Format("{0}", cmbPhanLoaiThu.EditValue);
            string[] phanLoaiThuByHocSinhs = StringUtil.Split(Settings.Default.PhanLoaiThuByHocSinh, ",");

            return ArrayUtil.Contains(phanLoaiThuByHocSinhs, selectedPhanLoaiThuId);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            RptPhieuThuTien rptPhieuThuTien = new RptPhieuThuTien();
            rptPhieuThuTien.NgayNop.Value = dateNgay.DateTime;
            rptPhieuThuTien.HoTenHS.Value = cmbHocSinh.Text;
            rptPhieuThuTien.SoTien.Value = txtSoTien.EditValue;
            rptPhieuThuTien.ConLai.Value = txtConLai.EditValue;
            Dictionary<int, lop> hocSinhIdToLops = StaticDataUtil.GetLopsByHocSinhIds(entities, new List<int>() { (int)cmbHocSinh.EditValue }, dateNgay.DateTime);
            if (hocSinhIdToLops.ContainsKey((int)cmbHocSinh.EditValue))
            {
                rptPhieuThuTien.Lop.Value = hocSinhIdToLops[(int)cmbHocSinh.EditValue].Name;
            }

            AuthenticatedEntity authenticatedEntity = StaticDataFacade.Get(StaticDataKeys.AuthenticatedData) as AuthenticatedEntity;
            rptPhieuThuTien.NguoiThu.Value = authenticatedEntity.User.UserName;

            ReportPrintTool printTool = new ReportPrintTool(rptPhieuThuTien);
            printTool.Print((string)Settings.Default["POSPrinterName"]);
        }

        private void btnChonMayIn_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                string printerName = printDialog1.PrinterSettings.PrinterName;
                Settings.Default["POSPrinterName"] = printerName;
                Settings.Default.Save();
            }
        }

        private void cmbHocSinh_EditValueChanged(object sender, EventArgs e)
        {
            if (IsEditing)
            {
                return;
            }

            if (cmbHocSinh.EditValue != null && hocSinhIdToBangKeThuTienItems.ContainsKey((int)cmbHocSinh.EditValue))
            {
                BangKeThuTienItem bangKeThuTienItem = hocSinhIdToBangKeThuTienItems[(int)cmbHocSinh.EditValue];
                txtSoTien.Value = (decimal)(bangKeThuTienItem.PhaiThu - bangKeThuTienItem.DaThu);
            }
            else
            {
                txtSoTien.Value = 0;
            }
        }

        private void txtSoTien_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbHocSinh.EditValue != null && !string.IsNullOrWhiteSpace(cmbHocSinh.EditValue.ToString()) && hocSinhIdToBangKeThuTienItems.ContainsKey((int)cmbHocSinh.EditValue))
            {
                BangKeThuTienItem bangKeThuTienItem = hocSinhIdToBangKeThuTienItems[(int)cmbHocSinh.EditValue];
                double soTienConLai = 0;
                if (IsEditing)
                {
                    soTienConLai = bangKeThuTienItem.PhaiThu - bangKeThuTienItem.DaThu + PhieuThuRow.SoTien - Convert.ToDouble(txtSoTien.EditValue);
                }
                else
                {
                    soTienConLai = bangKeThuTienItem.PhaiThu - bangKeThuTienItem.DaThu - Convert.ToDouble(txtSoTien.EditValue);
                }

                txtConLai.EditValue = string.Format("{0:n0}", soTienConLai);
            }
            else
            {
                txtConLai.EditValue = 0;
            }
        }
    }
}