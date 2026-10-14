using ACG.Core.WinForm.Util;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Reports;
using QLMamNon.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static QLMamNon.Constant.PhanLoaiThuConstant;

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
            FormKey = AppForms.FormBangKeThuTrongNgay;
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(FrmBangKeThuTrongNgay_FormClosed);
        }

        private void FrmBangKeThuTrongNgay_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.entities != null)
            {
                this.entities.Dispose();
                this.entities = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (this.dateTuNgay.DateTime == DateTime.MinValue)
            {
                MessageBox.Show("Xin vui lòng chọn ngày", "Chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SoThuTienService soThuTienService = new SoThuTienService();
            List<BangKeThuTienItem> allBangKeThuTienItems = soThuTienService.GetBangKeTongHopThuTienForTaoPhieuThu(entities, this.dateTuNgay.DateTime, null);
            Dictionary<int, BangKeThuTienItem> hocSinhIdToBangKeThuTienItems = new Dictionary<int, BangKeThuTienItem>();
            foreach (var item in allBangKeThuTienItems)
            {
                if (!hocSinhIdToBangKeThuTienItems.ContainsKey(item.HocSinhId))
                {
                    hocSinhIdToBangKeThuTienItems.Add(item.HocSinhId, item);
                }
            }

            RptBangKeThuTrongNgay rpt = new RptBangKeThuTrongNgay();
            DateTime fromDate = DateTimeUtil.StartOfDate(dateTuNgay.DateTime);
            DateTime endDate = DateTimeUtil.EndOfDate(dateTuNgay.DateTime);
            // Filter in the query — avoids loading the entire phieuthus table (6.3)
            List<phieuthu> phieuThuDataTable = entities.phieuthus.Where(pt =>
                pt.Ngay >= fromDate && pt.Ngay <= endDate).ToList();

            rpt.FromDate.Value = fromDate;

            List<int> hocSinhIds = new List<int>();
            foreach (phieuthu item in phieuThuDataTable)
            {
                if (item.HocSinhId != null)
                {
                    hocSinhIds.Add(item.HocSinhId.Value);
                }
            }

            // Load only the students that appear in today's receipts (6.3)
            List<hocsinh> hocSinhTable = ListUtil.IsEmpty(hocSinhIds)
                ? new List<hocsinh>()
                : entities.hocsinhs.Where(hs => hocSinhIds.Contains(hs.HocSinhId)).ToList();
            Dictionary<int, lop> hocSinhIdToLops = StaticDataUtil.GetLopsByHocSinhIds(entities, hocSinhIds, fromDate);
            List<BangKeThuTrongNgayItem> displayedBangKeThuTienItems = new List<BangKeThuTrongNgayItem>();

            foreach (phieuthu item in phieuThuDataTable)
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
                        SoTienNop = item.PaymentTypeEnum != PaymentType.TRANSFER ? item.SoTien : 0,
                        SoTienChuyenKhoan = item.PaymentTypeEnum == PaymentType.TRANSFER ? item.SoTien : 0,
                        ConLai = conLai,
                        SoBienLai = item.MaPhieu
                    };
                displayedBangKeThuTienItems.Add(bangKeThuTrongNgayItem);
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