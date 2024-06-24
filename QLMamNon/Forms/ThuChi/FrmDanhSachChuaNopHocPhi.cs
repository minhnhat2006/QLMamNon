using ACG.Core.WinForm.Util;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Reports;
using QLMamNon.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmDanhSachChuaNopHocPhi : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmDanhSachChuaNopHocPhi()
        {
            FormKey = AppForms.FormDanhSachChuaNopHocPhi;
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbNam.Text) || string.IsNullOrWhiteSpace(cmbThang.Text))
            {
                MessageBox.Show("Xin vui lòng chọn Năm/Tháng", "Chọn Năm/Tháng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int year = (int)cmbNam.EditValue;
            int month = int.Parse(cmbThang.Text);
            DateTime ngaytinh = new DateTime(year, month, 1);
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            List<viewbangthutien> table = new List<viewbangthutien>(entities.getViewBangThuTienByNgayTinhAndLop(ngaytinh, null).ToList());
            List<int> bangThuTienIds = new List<int>(table.Count);

            foreach (viewbangthutien row in table)
            {
                bangThuTienIds.Add(row.BangThuTienId);
            }

            if (!ListUtil.IsEmpty(bangThuTienIds))
            {
                List<bangthutien_khoanthu> bTTKTDataTable = entities.getBangThuTienKhoanThuByBangThuTienIds(string.Join(",", bangThuTienIds)).ToList();
                List<phieuthu> phieuThuDataTable = entities.getPhieuThuByHocSinhIdAndNgayTinh(-1, ngaytinh).ToList();
                Dictionary<int, lop> hocSinhIdsToLopNames = StaticDataUtil.GetLopsByHocSinhIds(entities, table.Select(row => row.HocSinhId).ToList(), ngaytinh);

                SoThuTienService soThuTienService = new SoThuTienService();
                Dictionary<int, viewbangthutien> prevMonthRowDictionary = soThuTienService.EvaluatePrevMonthViewBangThuTienTable(entities, ngaytinh.AddMonths(-1), null);

                foreach (viewbangthutien row in table)
                {
                    if (hocSinhIdsToLopNames.ContainsKey(row.HocSinhId))
                    {
                        row.Lop = hocSinhIdsToLopNames[row.HocSinhId].Name;
                    }

                    row.Ten = StaticDataUtil.GetHocSinhNameByHocSinhId(entities.hocsinhs.ToList(), row.HocSinhId);
                    row.HoTen = StaticDataUtil.GetHocSinhFullNameByHocSinhId(entities.hocsinhs.ToList(), row.HocSinhId);
                    BangThuTienUtil.EvaluateValuesForViewBangThuTienRow(row,
                        prevMonthRowDictionary != null && prevMonthRowDictionary.ContainsKey(row.HocSinhId) ? prevMonthRowDictionary[row.HocSinhId] : null,
                        bTTKTDataTable, phieuThuDataTable, false, false, true);
                    row.PhucVuBanTru = row.HocPhi + row.BanTru;
                    row.OriginalThanhTien = row.ThanhTien;
                }

                table.RemoveAll(row => string.IsNullOrWhiteSpace(row.Lop) || (row.ThanhTien - row.SoTienNopLan1 - row.SoTienNopLan2) <= 0);

                for (int idx = 0; idx < table.Count; idx++)
                {
                    table[idx].STT = idx + 1;
                }
            }

            RptDanhSachChuaNopHocPhi rpt = new RptDanhSachChuaNopHocPhi();
            rpt.Month.Value = month;
            rpt.Year.Value = year;
            rpt.viewBangThuTienBindingSource.DataSource = table;
            FormMainFacade.ShowReport(rpt);
        }

        private void FrmDanhSachChuaNopHocPhi_Load(object sender, EventArgs e)
        {
            namHocBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.NamHoc);
            cmbNam.EditValue = DateTime.Now.Year;
            cmbThang.Text = (DateTime.Now.Month).ToString();
        }
    }
}