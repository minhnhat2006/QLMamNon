using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Reports;
using QLMamNon.Service.Data;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmBaoCaoTinhHinhThuChi : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        public GridView GridView { get; set; }

        public bool IsEditing { get; set; }

        public phieuthu PhieuThuRow { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmBaoCaoTinhHinhThuChi()
        {
            FormKey = AppForms.FormBaoCaoTinhHinhThuChi;
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (this.dateTuNgay.DateTime == null || this.dateDenNgay.DateTime == null)
            {
                MessageBox.Show("Xin vui lòng chọn ngày", "Chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<int> phanLoaiThuIds = new List<int>();
            int[] selectedThuRowHandlers = this.gvThu.GetSelectedRows();

            if (ArrayUtil.IsEmpty(selectedThuRowHandlers))
            {
                MessageBox.Show("Xin vui lòng chọn Phân loại thu", "Chọn Phân loại thu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (int rowHandler in selectedThuRowHandlers)
            {
                int phanLoaiThuId = (int)this.gvThu.GetRowCellValue(rowHandler, "PhanLoaiThuId");
                phanLoaiThuIds.Add(phanLoaiThuId);
            }

            List<int> phanLoaiChiIds = new List<int>();
            int[] selectedRowHandlers = this.gvMain.GetSelectedRows();

            if (ArrayUtil.IsEmpty(selectedRowHandlers))
            {
                MessageBox.Show("Xin vui lòng chọn Mã loại chi", "Chọn Mã loại chi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (chkTon.Checked && StringUtil.IsEmpty(txtTon.Text))
            {
                MessageBox.Show("Xin vui lòng nhập số tiền tồn tháng trước", "Nhập số tiền tồn tháng trước", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (int rowHandler in selectedRowHandlers)
            {
                int phanLoaiChiId = (int)this.gvMain.GetRowCellValue(rowHandler, "PhanLoaiChiId");
                phanLoaiChiIds.Add(phanLoaiChiId);
            }

            DateTime fromDate = DateTimeUtil.StartOfDate(this.dateTuNgay.DateTime);
            DateTime toDate = DateTimeUtil.EndOfDate(this.dateDenNgay.DateTime);

            RptTinhHinhThuChi rpt = new RptTinhHinhThuChi();
            rpt.TuNgay.Value = fromDate;
            rpt.DenNgay.Value = toDate;
            rpt.TongThu.Value = entities.getSumSoTienThuByDateRange(fromDate, toDate, StringUtil.JoinWithCommas(phanLoaiThuIds)).FirstOrDefault();
            rpt.TongChi.Value = entities.getSumSoTienChiByDateRange(fromDate, toDate, StringUtil.JoinWithCommas(phanLoaiChiIds)).FirstOrDefault();

            rpt.Ton.Value = findSoTienTonDauKy(toDate);
            rpt.ChenhLech.Value = (long)rpt.Ton.Value + (long)rpt.TongThu.Value - (long)rpt.TongChi.Value;
            rpt.PhaiThu.Value = findSoTienPhaiThu(fromDate, toDate);
            rpt.TruyThu.Value = (long)rpt.PhaiThu.Value - (long)rpt.TongThu.Value;

            PhieuThuService phieuThuService = new PhieuThuService();
            rpt.thuDataSource.DataSource = phieuThuService.LoadPhieuThuByDateRangeWithGroupPhanLoaiThu(fromDate, toDate, phanLoaiThuIds);

            PhieuChiService phieuChiService = new PhieuChiService();
            rpt.chiDataSource.DataSource = phieuChiService.LoadPhieuChiByDateRangeWithGroupPhanLoaiChi(entities, fromDate, toDate, phanLoaiChiIds);

            FormMainFacade.ShowReport(rpt);
        }

        private long findSoTienTonDauKy(DateTime toDate)
        {
            long soTienTonDauKy = (long)txtTon.Value;

            if (!chkTon.Checked)
            {
                SoThuTienService soThuTienService = new SoThuTienService();
                soTienTonDauKy = (long)soThuTienService.GetSoTienTonDauKy(entities, toDate);
            }
            return soTienTonDauKy;
        }

        private long findSoTienPhaiThu(DateTime fromDate, DateTime toDate)
        {
            long phaiThu = 0;
            int? lop = null;

            List<viewbangthutien> viewBangThuTienDataTable = entities.getViewBangThuTienByNgayTinhAndLop(toDate, lop).ToList();
            List<int> bangThuTienIds = new List<int>(viewBangThuTienDataTable.Count);

            foreach (viewbangthutien row in viewBangThuTienDataTable)
            {
                bangThuTienIds.Add(row.BangThuTienId);
            }

            if (!ListUtil.IsEmpty(bangThuTienIds))
            {
                List<bangthutien_khoanthu> bTTKTDataTable = entities.getBangThuTienKhoanThuByBangThuTienIds(String.Join(",", bangThuTienIds)).ToList();
                List<phieuthu> phieuThuDataTable = entities.getPhieuThuByHocSinhIdAndNgayTinh(-1, toDate).ToList();
                List<hocsinh> hocSinhDataTable = entities.hocsinhs.ToList();
                SoThuTienService soThuTienService = new SoThuTienService();
                Dictionary<int, viewbangthutien> prevMonthRowDictionary = soThuTienService.EvaluatePrevMonthViewBangThuTienTable(entities, toDate.AddMonths(-1), lop);

                foreach (viewbangthutien row in viewBangThuTienDataTable)
                {
                    row.HoTen = StaticDataUtil.GetHocSinhFullNameByHocSinhId(hocSinhDataTable, row.HocSinhId);
                    BangThuTienUtil.EvaluateValuesForViewBangThuTienRow(row,
                        prevMonthRowDictionary != null && prevMonthRowDictionary.ContainsKey(row.HocSinhId) ? prevMonthRowDictionary[row.HocSinhId] : null,
                        bTTKTDataTable, phieuThuDataTable, false, false, true);
                    row.PhucVuBanTru = row.HocPhi + row.BanTru;
                }
            }

            foreach (viewbangthutien viewBangThuTienRow in viewBangThuTienDataTable)
            {
                phaiThu += Convert.ToInt64(viewBangThuTienRow.ThanhTien);
            }

            return phaiThu;
        }

        private void FrmBaoCaoTinhHinhThuChi_Load(object sender, EventArgs e)
        {
            this.phanLoaiChiRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.PhanLoaiChi);
            this.phanLoaiThuRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.PhanLoaiThu);
        }

        private void FrmBaoCaoTinhHinhThuChi_Shown(object sender, EventArgs e)
        {
            this.gvThu.SelectAll();
            this.gvMain.SelectAll();
        }

        private void chkTon_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chk = (CheckEdit)sender;
            txtTon.Enabled = chk.Checked;
        }
    }
}