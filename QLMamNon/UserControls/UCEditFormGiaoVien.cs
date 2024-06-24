using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Facade;
using QLMamNon.Components.Data.Static;

namespace QLMamNon.UserControls
{
    public partial class UCEditFormGiaoVien : UCCRUDBase
    {
        public UCEditFormGiaoVien()
        {
            InitializeComponent();
        }

        private void UCEditFormGiaoVien_Load(object sender, EventArgs e)
        {
            this.thanhPhoRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.TinhThanhPho);
        }

        private void cmbTinh_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbTinh.EditValue != DBNull.Value)
            {
                QuanHuyenDataTable table = StaticDataFacade.Get(StaticDataKeys.QuanHuyen) as QuanHuyenDataTable;
                this.quanHuyenRowBindingSource.DataSource = table.Select(String.Format("ThanhPhoId={0}", cmbTinh.EditValue));
            }
        }

        private void cmbQuan_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbQuan.EditValue != DBNull.Value)
            {
                PhuongXaDataTable table = StaticDataFacade.Get(StaticDataKeys.PhuongXa) as PhuongXaDataTable;
                this.phuongXaRowBindingSource.DataSource = table.Select(String.Format("QuanHuyenId={0}", cmbQuan.EditValue));
            }
        }
    }
}
