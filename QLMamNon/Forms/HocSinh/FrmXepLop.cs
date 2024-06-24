using ACG.Core.WinForm.Util;
using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using QLMamNon.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QLMamNon.Forms.HocSinh
{
    public partial class FrmXepLop : CRUDForm<hocsinh>
    {
        public FrmXepLop()
        {
            InitializeComponent();

            this.FormKey = AppForms.FormXepLop;
            this.InitForm(null, null, null, this.btnSave, null, this.gvDen, null);
        }

        private void FrmXepLop_Load(object sender, EventArgs e)
        {

            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(GridViewMain_CustomDrawCell);
            this.gridView1.CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(gvMain_CustomColumnDisplayText);

            this.gvDen.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(GridViewMain_CustomDrawCell);
            this.gvDen.CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(gvMain_CustomColumnDisplayText);
            this.gvDen.OptionsEditForm.CustomEditFormLayout = new UCEditFormXepLop();

            this.Entities.hocsinhs.Load();
            this.hocSinhRowBindingSourceDi.DataSource = this.Entities.hocsinhs.Local.ToBindingList();
            this.namHocBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.NamHoc);
            this.lopRowBindingSourceDi.DataSource = StaticDataFacade.Get(StaticDataKeys.LopHoc);
            this.lopRowBindingSourceDen.DataSource = StaticDataFacade.Get(StaticDataKeys.LopHoc);

            this.cmbNamHocDi.EditValue = DateTime.Now.Year;
        }

        private void gvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "GioiTinh" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                sbyte gioiTinh = (sbyte)view.GetListSourceRowCellValue(e.ListSourceRowIndex, "GioiTinh");
                switch (gioiTinh)
                {
                    case 0: e.DisplayText = "Nữ"; break;
                    case 1: e.DisplayText = "Nam"; break;
                }
            }
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if (ControlUtil.IsEditValueNull(this.cmbLopHocDen))
            {
                MessageBox.Show("Xin vui long chọn Lớp học", "Chọn lớp học", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.hocSinhRowBindingSourceDi.Current == null)
            {
                return;
            }

            hocsinh oldRow = this.hocSinhRowBindingSourceDi.Current as hocsinh;

            if (this.isHocSinhExisted(this.hocSinhRowBindingSourceDen, oldRow.HocSinhId))
            {
                return;
            }

            hocsinh newRow = this.hocSinhRowBindingSourceDen.AddNew() as hocsinh;
            copyHocSinhRow(oldRow, newRow);
            newRow.LopDangHoc = this.cmbLopHocDen.Text;
            newRow.NgayVaoLop = DateTime.Now;

            this.gvDen.ShowEditForm();
        }

        private static void copyHocSinhRow(hocsinh oldRow, hocsinh newRow)
        {
            newRow.HocSinhId = oldRow.HocSinhId;
            newRow.HoDem = oldRow.HoDem;
            newRow.Ten = oldRow.Ten;
            newRow.GioiTinh = oldRow.GioiTinh;
            newRow.ThoiHoc = oldRow.ThoiHoc;
            newRow.NgaySinh = oldRow.NgaySinh;
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            if (this.hocSinhRowBindingSourceDen.Current == null)
            {
                return;
            }

            hocsinh oldRow = this.hocSinhRowBindingSourceDen.Current as hocsinh;

            if (this.isHocSinhExisted(this.hocSinhRowBindingSourceDi, oldRow.HocSinhId))
            {
                List<hocsinh> hocSinhTable = this.hocSinhRowBindingSourceDi.DataSource as List<hocsinh>;
                List<hocsinh> hocSinhRows = hocSinhTable.FindAll(h => h.HocSinhId == oldRow.HocSinhId);
                hocSinhRows[0].LopDangHoc = CommonConstant.EMPTY;
                this.gvDen.DeleteSelectedRows();

                return;
            }

            hocsinh newRow = this.hocSinhRowBindingSourceDi.AddNew() as hocsinh;
            copyHocSinhRow(oldRow, newRow);

            this.gvDen.DeleteSelectedRows();
        }

        private void cmbLopHocDi_EditValueChanged(object sender, EventArgs e)
        {
            this.loadHocSinhToGridDi();
        }

        private void cmbNamHocDi_EditValueChanged(object sender, EventArgs e)
        {
            this.loadHocSinhToGridDi();
        }

        private void cmbLopHocDen_EditValueChanged(object sender, EventArgs e)
        {
            this.loadHocSinhToGridDen();
        }

        protected override void onSaving()
        {
            if (ControlUtil.IsEditValueNull(this.cmbLopHocDen))
            {
                MessageBox.Show("Xin vui long chọn Lớp học", "Chọn lớp học", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<int> hocSinhIds = new List<int>();
            int lop = (int)cmbLopHocDen.EditValue;
            List<hocsinh> hocSinhTable = Entities.getHocSinhByParams(DateTime.Now, null, lop, null).ToList();

            foreach (hocsinh hocSinhRow in hocSinhRowBindingSourceDen)
            {
                int hocSinh = hocSinhRow.HocSinhId;
                hocSinhIds.Add(hocSinh);

                if (Entities.Entry(hocSinhRow).State == EntityState.Modified || Entities.Entry(hocSinhRow).State == EntityState.Added || Entities.Entry(hocSinhRow).State == EntityState.Detached)
                {
                    Entities.insertHocSinhToLop(hocSinh, lop, hocSinhRow.NgayVaoLop);
                }
            }

            List<int> deletingHocSinhIds = new List<int>();

            foreach (hocsinh hsRow in hocSinhTable)
            {
                if (!hocSinhIds.Contains(hsRow.HocSinhId))
                {
                    deletingHocSinhIds.Add(hsRow.HocSinhId);
                }
            }

            if (!ListUtil.IsEmpty(deletingHocSinhIds))
            {
                Entities.deleteHocSinhLopByHocSinhIds(StringUtil.Join(deletingHocSinhIds, ","), DateTime.Now.AddDays(-1));

                SoThuTienService soThuTienService = new SoThuTienService();
                soThuTienService.DeleteBangThuTienByHocSinhIdsAndDate(Entities, deletingHocSinhIds, DateTimeUtil.DateEndOfMonth(DateTime.Now));
            }

            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SavedCaption);
        }

        private void gvDen_RowUpdated(object sender, RowObjectEventArgs e)
        {
            if (this.gvDen.IsNewItemRow(e.RowHandle))
            {
                this.hocSinhRowBindingSourceDi.RemoveCurrent();
            }
        }
    }
}