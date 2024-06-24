using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Reports;
using QLMamNon.Service.Data;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmBangKeCacLoaiChi : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmBangKeCacLoaiChi()
        {
            FormKey = AppForms.FormBangKeCacLoaiChi;
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.cmbNam.Text) || string.IsNullOrWhiteSpace(this.cmbThang.Text))
            {
                MessageBox.Show("Xin vui lòng chọn Năm/Tháng", "Chọn Năm/Tháng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime fromDate = DateTimeUtil.StartOfDate(new DateTime((int)cmbNam.EditValue, int.Parse(cmbThang.Text), 1));
            DateTime toDate = DateTimeUtil.EndOfDate(fromDate.AddMonths(1).AddDays(-1));

            List<phanloaichi> phanLoaiChiDataTable = StaticDataFacade.Get(StaticDataKeys.PhanLoaiChi) as List<phanloaichi>;
            List<int> phanLoaiChiIds = new List<int>();
            foreach (var item in phanLoaiChiDataTable)
            {
                phanLoaiChiIds.Add(item.PhanLoaiChiId);
            }

            PhieuChiService phieuChiService = new PhieuChiService();
            List<phieuchi> phieuChiDataTable = phieuChiService.LoadPhieuChiByDateRange(entities, fromDate, toDate, phanLoaiChiIds);

            int totalDays = (toDate - fromDate).Days + 1;
            Dictionary<int, Dictionary<string, long>> dayToSoTienPhanLoaiChi = new Dictionary<int, Dictionary<string, long>>(totalDays);

            foreach (phieuchi item in phieuChiDataTable)
            {
                if (!dayToSoTienPhanLoaiChi.ContainsKey(item.Ngay.Day))
                {
                    dayToSoTienPhanLoaiChi.Add(item.Ngay.Day, new Dictionary<string, long>());
                }

                Dictionary<string, long> phanLoaiChiToSoTien = dayToSoTienPhanLoaiChi[item.Ngay.Day];
                if (!phanLoaiChiToSoTien.ContainsKey(item.PhanLoaiChi))
                {
                    phanLoaiChiToSoTien.Add(item.PhanLoaiChi, item.SoTien);
                }
                else
                {
                    long currentSoTien = phanLoaiChiToSoTien[item.PhanLoaiChi];
                    phanLoaiChiToSoTien.Remove(item.PhanLoaiChi);
                    phanLoaiChiToSoTien.Add(item.PhanLoaiChi, item.SoTien + currentSoTien);
                }
            }

            Dictionary<string, BangKeCacLoaiChiItem> displayedBangKeThuTienItems = new Dictionary<string, BangKeCacLoaiChiItem>();
            int stt = 1;
            int week = 0;
            int startOfWeek = 0;
            List<string> dateRanges = new List<string>();
            for (int i = 0; i < totalDays; i++)
            {
                DateTime date = fromDate.AddDays(i);

                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                if (i == 0 || date.DayOfWeek == DayOfWeek.Monday)
                {
                    week++;
                    startOfWeek = date.Day;
                }

                if (i == (totalDays - 1) || date.DayOfWeek == DayOfWeek.Saturday)
                {
                    dateRanges.Add($"{startOfWeek}/{date.Month}-{date.ToString("d/M")}");
                }

                if (!dayToSoTienPhanLoaiChi.ContainsKey(date.Day))
                {
                    continue;
                }

                Dictionary<string, long> phanLoaiChiToSoTien = dayToSoTienPhanLoaiChi[date.Day];
                foreach (var item in phanLoaiChiToSoTien)
                {
                    if (!displayedBangKeThuTienItems.ContainsKey(item.Key))
                    {
                        displayedBangKeThuTienItems.Add(item.Key, new BangKeCacLoaiChiItem()
                        {
                            PhanLoaiChi = item.Key,
                            STT = stt++,
                            Week1 = 0,
                            Week2 = 0,
                            Week3 = 0,
                            Week4 = 0,
                            Week5 = 0,
                            Week6 = 0
                        });
                    }

                    BangKeCacLoaiChiItem bangKeCacLoaiChiItem = displayedBangKeThuTienItems[item.Key];

                    switch (week)
                    {
                        case 1:
                            bangKeCacLoaiChiItem.Week1 += item.Value;
                            break;
                        case 2:
                            bangKeCacLoaiChiItem.Week2 += item.Value;
                            break;
                        case 3:
                            bangKeCacLoaiChiItem.Week3 += item.Value;
                            break;
                        case 4:
                            bangKeCacLoaiChiItem.Week4 += item.Value;
                            break;
                        case 5:
                            bangKeCacLoaiChiItem.Week5 += item.Value;
                            break;
                        case 6:
                            bangKeCacLoaiChiItem.Week6 += item.Value;
                            break;
                        default:
                            break;
                    }
                }
            }
            RptBangKeCacLoaiChi rpt = new RptBangKeCacLoaiChi();
            rpt.FromDate.Value = fromDate;
            rpt.ToDate.Value = toDate;
            rpt.Week1.Value = dateRanges[0];
            rpt.Week2.Value = dateRanges[1];
            rpt.Week3.Value = dateRanges[2];
            rpt.Week4.Value = dateRanges[3];
            rpt.Week5.Value = dateRanges.Count > 4 ? dateRanges[4] : string.Empty;
            rpt.Week6.Value = dateRanges.Count > 5 ? dateRanges[5] : string.Empty;
            rpt.bangKeCacLoaiChiBindingSource.DataSource = displayedBangKeThuTienItems.Values;
            FormMainFacade.ShowReport(rpt);
        }

        private void FrmBaoCaoHoatDongTaiChinh_Load(object sender, EventArgs e)
        {
            this.namHocBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.NamHoc);
            cmbNam.EditValue = DateTime.Now.Year;
            cmbThang.Text = (DateTime.Now.Month - 1).ToString();
        }
    }
}