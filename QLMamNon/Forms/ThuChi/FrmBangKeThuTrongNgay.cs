using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Reports;
using QLMamNon.Service.Data;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmBangKeThuTrongNgay : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmBangKeThuTrongNgay()
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
            if (this.dateTuNgay.DateTime == null)
            {
                MessageBox.Show("Xin vui lòng chọn ngày", "Chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SoThuTienService soThuTienService = new SoThuTienService();
            List<BangKeThuTienItem> allBangKeThuTienItems = soThuTienService.GetBangKeTongHopThuTienForTaoPhieuThu(entities, this.dateTuNgay.DateTime, null);
            Dictionary<int, BangKeThuTienItem> hocSinhIdToBangKeThuTienItems = new Dictionary<int, BangKeThuTienItem>();
            foreach (var item in allBangKeThuTienItems)
            {
                hocSinhIdToBangKeThuTienItems.Add(item.HocSinhId, item);
            }

            RptBangKeThuTrongNgay rpt = new RptBangKeThuTrongNgay();
            DateTime fromDate = DateTimeUtil.StartOfDate(dateTuNgay.DateTime);
            List<phieuthu> phieuThuDataTable = entities.phieuthus.ToList();

            rpt.FromDate.Value = fromDate;

            List<int> hocSinhIds = new List<int>();
            foreach (phieuthu item in phieuThuDataTable)
            {
                if (item.Ngay.ToShortDateString() == fromDate.ToShortDateString() && item.HocSinhId != null)
                {
                    hocSinhIds.Add(item.HocSinhId.Value);
                }
            }

            List<hocsinh> hocSinhTable = entities.hocsinhs.ToList();
            Dictionary<int, lop> hocSinhIdToLops = StaticDataUtil.GetLopsByHocSinhIds(entities, hocSinhIds, fromDate);
            List<BangKeThuTrongNgayItem> displayedBangKeThuTienItems = new List<BangKeThuTrongNgayItem>();

            foreach (phieuthu item in phieuThuDataTable)
            {
                if (item.Ngay.ToShortDateString() == fromDate.ToShortDateString())
                {
                    int lopId = 0;
                    string lop = string.Empty;
                    if (item.HocSinhId != null && hocSinhIdToLops.ContainsKey(item.HocSinhId.Value))
                    {
                        lopId = hocSinhIdToLops[item.HocSinhId.Value].LopId;
                        lop = hocSinhIdToLops[item.HocSinhId.Value].Name;
                    }

                    long conLai = 0;
                    if (item.HocSinhId != null && hocSinhIdToBangKeThuTienItems.ContainsKey(item.HocSinhId.Value))
                    {
                        BangKeThuTienItem bangKeThuTienItem = hocSinhIdToBangKeThuTienItems[item.HocSinhId.Value];
                        conLai = (long)bangKeThuTienItem.PhaiThu - (long)bangKeThuTienItem.DaThu;
                    }

                    BangKeThuTrongNgayItem bangKeThuTrongNgayItem = new BangKeThuTrongNgayItem()
                    {
                        HoTenHS = item.HocSinhId == null ? string.Empty : StaticDataUtil.GetHocSinhFullNameByHocSinhId(hocSinhTable, item.HocSinhId.Value),
                        LopId = lopId,
                        Lop = lop,
                        SoTienNop = item.SoTien,
                        ConLai = conLai,
                        SoBienLai = item.MaPhieu
                    };
                    displayedBangKeThuTienItems.Add(bangKeThuTrongNgayItem);
                }
            }

            List<BangKeThuTrongNgayItem> sortedDisplayBangKeThuTienItems = displayedBangKeThuTienItems.OrderBy(o => o.LopId).ToList();
            for (int i = 0; i < sortedDisplayBangKeThuTienItems.Count; i++)
            {
                sortedDisplayBangKeThuTienItems[i].STT = i + 1;
            }

            rpt.bangKeThuTrongNgayBindingSource.DataSource = sortedDisplayBangKeThuTienItems;
            FormMainFacade.ShowReport(rpt);
        }

        private void FrmBaoCaoHoatDongTaiChinh_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = DateTime.Now;
        }
    }
}