using ACG.Core.WinForm.Util;
using QLMamNon.Dao;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmSelectHocSinhsToGenerate : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        public List<hocsinh> HocSinhTable { get; set; }

        public List<int> GeneratedHocSinhIds { get; set; }

        public List<hocsinh> HocSinhRows { get; set; }

        #endregion

        public FrmSelectHocSinhsToGenerate()
        {
            InitializeComponent();
        }

        private void FrmSelectHocSinhsToGenerate_Load(object sender, EventArgs e)
        {
            this.hocSinhRowBindingSource.DataSource = HocSinhTable;
        }

        private void FrmSelectHocSinhsToGenerate_Shown(object sender, EventArgs e)
        {
            this.gvMain.SelectAll();

            foreach (int rowHandler in this.gvMain.GetSelectedRows())
            {
                hocsinh hocSinhRow = (hocsinh)this.gvMain.GetRow(rowHandler);

                if (this.GeneratedHocSinhIds.Contains(hocSinhRow.HocSinhId))
                {
                    this.gvMain.UnselectRow(rowHandler);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            this.HocSinhRows = new List<hocsinh>();
            int[] selectedRowHandlers = this.gvMain.GetSelectedRows();

            for (int i = 0; i < selectedRowHandlers.Length; i++)
            {
                hocsinh rowView = (hocsinh)this.gvMain.GetRow(selectedRowHandlers[i]);
                this.HocSinhRows.Add(rowView);
            }

            if (ListUtil.IsEmpty(this.HocSinhRows))
            {
                MessageBox.Show("Bạn chưa chọn học sinh", "Chọn học sinh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}