using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace QLMamNon.UserControls
{
    public partial class UCEditFormLopHoc : UCCRUDBase
    {
        private lop row = null;

        public UCEditFormLopHoc(qlmamnonEntities entities) : base()
        {
            InitializeComponent();
            this.Entities = entities;
        }

        private void UCEditFormLopHoc_Load(object sender, EventArgs e)
        {
            this.khoiRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.KhoiHoc);

            row = this.GridView.GetFocusedRow() as lop;
            if (row != null)
            {
                this.cmbKhoi.EditValue = StaticDataUtil.GetKhoiIdByLopId(Entities, row.LopId);
            }
        }

        private void UCEditFormLopHoc_Enter(object sender, EventArgs e)
        {
        }

        private void cmbKhoi_EditValueChanged(object sender, EventArgs e)
        {
            if (row != null)
            {
                DbEntityEntry entry = Entities.Entry(row);

                if (entry.State == EntityState.Unchanged)
                {
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
