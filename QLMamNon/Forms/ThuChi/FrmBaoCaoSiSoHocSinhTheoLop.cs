using ACG.Core.WinForm.Util;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmBaoCaoSiSoHocSinhTheoLop : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmBaoCaoSiSoHocSinhTheoLop()
        {
            FormKey = AppForms.FormBangKeThuHocPhi;
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (this.dateEditThang.DateTime == null)
            {
                MessageBox.Show("Xin vui lòng chọn Tháng", "Chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime selectedDate = dateEditThang.DateTime;
            DateTime fromDate = DateTimeUtil.DateStartOfMonth(selectedDate);
            DateTime toDate = DateTimeUtil.DateEndOfMonth(selectedDate);
            List<lop> lops = StaticDataFacade.Get(StaticDataKeys.LopHoc) as List<lop>;
            List<BaoCaoSiSoHocSinhTheoLopItem> baoCaoSiSoHocSinhTheoLopItems = new List<BaoCaoSiSoHocSinhTheoLopItem>();

            foreach (var lop in lops)
            {
                Dictionary<int, BaoCaoSoLuongHocSinhTheoLopItem> baoCaoSoLuongHocSinhTheoLopItemsMap = new Dictionary<int, BaoCaoSoLuongHocSinhTheoLopItem>();
                List<hocsinh> hocsinhDauThangList = entities.getHocSinhByLopAndNgay(lop.LopId, fromDate).ToList();
                List<hocsinh> hocsinhCuoiThangList = entities.getHocSinhByLopAndNgay(lop.LopId, toDate).ToList();
                int hocsinhMoiCount = hocsinhCuoiThangList.Count(hs => !hocsinhDauThangList.Any(item => item.HocSinhId == hs.HocSinhId));
                int hocsinhThoihocCount = hocsinhDauThangList.Count(hs => !hocsinhCuoiThangList.Any(item => item.HocSinhId == hs.HocSinhId));

                BaoCaoSiSoHocSinhTheoLopItem baoCaoSiSoHocSinhTheoLopItem = new BaoCaoSiSoHocSinhTheoLopItem()
                {
                    Lop = lop.Name,
                    HSDauThang = hocsinhDauThangList.Count,
                    HSCuoiThang = hocsinhCuoiThangList.Count,
                    HSMoi = hocsinhMoiCount,
                    HSThoiHoc = hocsinhThoihocCount
                };

                baoCaoSiSoHocSinhTheoLopItems.Add(baoCaoSiSoHocSinhTheoLopItem);
            }

            RptBaoCaoSiSoHocSinhTheoLop rpt = new RptBaoCaoSiSoHocSinhTheoLop();
            rpt.Month.Value = fromDate.Month;
            rpt.Year.Value = fromDate.Year;
            rpt.baoCaoSiSoHocSinhTheoLopDataSource.DataSource = baoCaoSiSoHocSinhTheoLopItems.OrderBy(item => item.Lop).ToList();
            FormMainFacade.ShowReport(rpt);
        }

        private void FrmBaoCaoHoatDongTaiChinh_Load(object sender, EventArgs e)
        {
            dateEditThang.DateTime = DateTime.Now;
        }
    }
}