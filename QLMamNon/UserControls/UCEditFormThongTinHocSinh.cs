using System;
using System.Collections.Generic;
using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;

namespace QLMamNon.UserControls
{
    public partial class UCEditFormThongTinHocSinh : UCCRUDBase
    {
        public UCEditFormThongTinHocSinh()
        {
            InitializeComponent();
        }

        private void UCEditFormThongTinHocSinh_Load(object sender, EventArgs e)
        {
            this.thanhPhoRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.TinhThanhPho);
        }

        private void cmbTinh_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbTinh.EditValue != null)
            {
                List<quanhuyen> table = StaticDataFacade.Get(StaticDataKeys.QuanHuyen) as List<quanhuyen>;
                int thanhPhoId = string.IsNullOrEmpty(cmbTinh.Text) ? -1 : (int)cmbTinh.EditValue;
                this.quanHuyenRowBindingSource.DataSource = table.FindAll(q => q.ThanhPhoId == thanhPhoId);
            }
        }

        private void cmbQuan_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbQuan.EditValue != null)
            {
                List<phuongxa> table = StaticDataFacade.Get(StaticDataKeys.PhuongXa) as List<phuongxa>;
                int cmbQuanId = string.IsNullOrEmpty(cmbQuan.Text) ? -1 : (int)cmbQuan.EditValue;
                this.phuongXaRowBindingSource.DataSource = table.FindAll(p => p.QuanHuyenId == cmbQuanId);
            }
        }
    }
}
