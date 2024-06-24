using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;
using System;
using System.Data;

namespace QLMamNon.UserControls
{
    public partial class UCEditFormKhoiHoc : UCCRUDBase
    {
        public UCEditFormKhoiHoc()
        {
            InitializeComponent();
        }

        private void UCEditFormKhoiHoc_Load(object sender, EventArgs e)
        {
            this.truongRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.TruongHoc);
        }

        private void UCEditFormKhoiHoc_Enter(object sender, EventArgs e)
        {
            DataRow row = this.GridView.GetFocusedDataRow();

            if (row != null)
            {
                this.cmbTruong.EditValue = StaticDataUtil.GetTruongIdByKhoiId(Entities, (int)row["KhoiId"]);
            }
        }
    }
}
