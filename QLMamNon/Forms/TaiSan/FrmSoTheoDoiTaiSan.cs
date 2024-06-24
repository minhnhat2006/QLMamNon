using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Entity.Form;
using QLMamNon.Facade;
using QLMamNon.Reports;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmSoTheoDoiTaiSan : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        protected string FormKey { get; set; }

        public GridView GridView { get; set; }

        public bool IsEditing { get; set; }

        public phieuthu PhieuThuRow { get; set; }

        private qlmamnonEntities entities;

        #endregion

        public FrmSoTheoDoiTaiSan()
        {
            this.FormKey = AppForms.FormBaoCaoChiTietHoatDongTaiChinh;
            entities = StaticDataFacade.GetQLMNEntities();

            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (ControlUtil.IsEditValueNull(this.cmbYear))
            {
                MessageBox.Show("Xin vui lòng chọn năm", "Chọn năm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RptSoTheoDoiTaiSan rpt = new RptSoTheoDoiTaiSan();
            List<viewbangiaotaisan> table = entities.getViewBanGiaoTaiSanByYear((int)cmbYear.EditValue).ToList();
            List<SoTheoDoiTaiSanItem> soTheoDoiTaiSan = new List<SoTheoDoiTaiSanItem>();
            SoTheoDoiTaiSanItem prevSoTheoDoiTaiSanItem = null;
            SoTheoDoiTaiSanItem soTheoDoiTaiSanItem = null;

            foreach (viewbangiaotaisan row in table)
            {
                row.LopName = StaticDataUtil.GetLopNameByLopId(row.LopId.Value);

                if (prevSoTheoDoiTaiSanItem != null && prevSoTheoDoiTaiSanItem.TaiSanId == row.TaiSanId)
                {
                    soTheoDoiTaiSanItem = new SoTheoDoiTaiSanItem()
                    {
                        TaiSanId = row.TaiSanId,
                        GhiChu = CommonConstant.EMPTY,
                        LyDoGiam = "Bàn giao",
                        NgayChungTuGiam = row.NgayChungTu,
                        SoChungTuGiam = row.SoChungTu,
                        SoLuongBanGiao = row.SoLuongBanGiao.Value,
                        SoTienBanGiao = row.DonGia * row.SoLuongBanGiao.Value,
                    };
                }
                else
                {
                    soTheoDoiTaiSanItem = new SoTheoDoiTaiSanItem()
                    {
                        TaiSanId = row.TaiSanId,
                        Ten = row.Ten,
                        DonGia = row.DonGia,
                        DonViTinh = row.DonViTinh,
                        GhiChu = CommonConstant.EMPTY,
                        LyDoGiam = "Bàn giao",
                        NgayChungTu = row.NgayChungTu,
                        NgayChungTuGiam = row.NgayChungTu,
                        SoChungTu = row.SoChungTu,
                        SoChungTuGiam = row.SoChungTu,
                        SoLuong = row.SoLuong,
                        SoLuongBanGiao = row.SoLuongBanGiao.Value,
                        SoTienBanGiao = row.DonGia * row.SoLuongBanGiao.Value,
                        ThanhTien = row.SoLuong * row.DonGia
                    };
                }

                soTheoDoiTaiSan.Add(soTheoDoiTaiSanItem);
                prevSoTheoDoiTaiSanItem = soTheoDoiTaiSanItem;
            }

            rpt.bindingSource.DataSource = soTheoDoiTaiSan;
            rpt.Year.Value = this.cmbYear.EditValue;
            FormMainFacade.ShowReport(rpt);
        }

        private void FrmBaoCaoHoatDongTaiChinh_Load(object sender, EventArgs e)
        {
            this.namHocBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.NamHoc);
        }
    }
}