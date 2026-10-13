using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.UserControls;

namespace QLMamNon.Forms.HocSinh
{
    public partial class FrmPhanBoTaiSan : CRUDForm<viewbangiaotaisan>
    {
        private Dictionary<int, double> _taiSanIdToSoLuongDictionary;

        public FrmPhanBoTaiSan()
        {
            InitializeComponent();

            this.TablePrimaryKey = "TaiSanLopId";
            this.DanhMuc = DanhMucConstant.PhanBoTaiSan;
            this.FormKey = AppForms.FormPhanBoTaiSan;
            this.gvLop.OptionsEditForm.CustomEditFormLayout = new UCEditFormBanGiaoTaiSan();
            // Read-only form: viewbangiaotaisan is a read-only view entity set (DefiningQuery, no
            // modification functions), so SaveChanges() would throw UpdateException. Writes belong on the
            // taisan_lop table; until that service exists nothing here can be persisted, so Save and the
            // move buttons are hidden and the lop grid is not editable.
            this.InitForm(null, null, null, null, null, this.gvLop, this.viewBanGiaoTaiSanRowBindingSource.DataSource);
            this.btnSave.Visible = false;
            this.btnMoveRight.Visible = false;
            this.btnMoveLeft.Visible = false;
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.gvLop.OptionsBehavior.Editable = false;
            this.loadTaiSanToGridTaiSan();
        }

        private void loadTaiSanToGridTaiSan()
        {
            BindingList<viewtaisan> viewTaiSanRows = new BindingList<viewtaisan>(Entities.getViewTaiSanForBanGiaoTaiSan().ToList());
            this.viewTaiSanRowBindingSource.DataSource = viewTaiSanRows;

            this._taiSanIdToSoLuongDictionary = new Dictionary<int, double>();

            foreach (viewtaisan viewTaiSanRow in viewTaiSanRows)
            {
                if (this._taiSanIdToSoLuongDictionary.ContainsKey(viewTaiSanRow.TaiSanId))
                {
                    this._taiSanIdToSoLuongDictionary[viewTaiSanRow.TaiSanId] += viewTaiSanRow.SoLuong;
                }
                else
                {
                    this._taiSanIdToSoLuongDictionary.Add(viewTaiSanRow.TaiSanId, viewTaiSanRow.SoLuong);
                }
            }

            if (this.DataTable != null)
            {
                foreach (viewbangiaotaisan viewBanGiaoTaiSanRow in this.DataTable)
                {
                    if (viewBanGiaoTaiSanRow.SoLuongBanGiao == null)
                    {
                        continue;
                    }

                    if (this._taiSanIdToSoLuongDictionary.ContainsKey(viewBanGiaoTaiSanRow.TaiSanId))
                    {
                        this._taiSanIdToSoLuongDictionary[viewBanGiaoTaiSanRow.TaiSanId] += viewBanGiaoTaiSanRow.SoLuongBanGiao.Value;
                    }
                    else
                    {
                        this._taiSanIdToSoLuongDictionary.Add(viewBanGiaoTaiSanRow.TaiSanId, viewBanGiaoTaiSanRow.SoLuongBanGiao.Value);
                    }
                }
            }
        }

        private void loadTaiSanToGridLop()
        {
            if (ControlUtil.IsEditValueNull(this.cmbLopHoc))
            {
                return;
            }

            this.viewBanGiaoTaiSanRowBindingSource.DataSource = new BindingList<viewbangiaotaisan>(Entities.getViewBanGiaoTaiSanByLopId((int)this.cmbLopHoc.EditValue).ToList());
            this.viewBanGiaoTaiSanRowBindingSource.Filter = String.Format("[NgayHetHan] IS NULL OR [NgayHetHan]>#{0:yyyy-MM-dd}#", DateTime.Now);
            this.DataTable = this.viewBanGiaoTaiSanRowBindingSource.DataSource as BindingList<viewbangiaotaisan>;
        }

        private void FrmPhanBoTaiSan_Load(object sender, EventArgs e)
        {
            this.gvTaiSan.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(GridViewMain_CustomDrawCell);
            this.gvLop.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(GridViewMain_CustomDrawCell);
            this.gvTaiSan.CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(gvMain_CustomColumnDisplayText);
            this.gvLop.CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(gvMain_CustomColumnDisplayText);
            this.lopRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.LopHoc);
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
            if (ControlUtil.IsEditValueNull(cmbLopHoc))
            {
                MessageBox.Show("Xin vui lòng chọn lớp", "Chọn lớp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.viewTaiSanRowBindingSource.Current == null)
            {
                return;
            }

            viewtaisan oldRow = this.viewTaiSanRowBindingSource.Current as viewtaisan;

            int existedIndex = this.viewBanGiaoTaiSanRowBindingSource.Find("TaiSanId", oldRow.TaiSanId);

            if (existedIndex >= 0)
            {
                this.viewBanGiaoTaiSanRowBindingSource.Position = existedIndex;
            }
            else
            {
                viewbangiaotaisan newRow = this.viewBanGiaoTaiSanRowBindingSource.AddNew() as viewbangiaotaisan;
                newRow.Ten = oldRow.Ten;
                newRow.TaiSanId = oldRow.TaiSanId;
                newRow.SoChungTu = oldRow.SoChungTu;
                newRow.NgayChungTu = oldRow.NgayChungTu;
                newRow.DonViTinh = oldRow.DonViTinh;
                newRow.DonGia = oldRow.DonGia;
                newRow.SoLuong = oldRow.SoLuong;
                newRow.NgayNhap = oldRow.NgayNhap;
                newRow.LopId = (int)cmbLopHoc.EditValue;
                newRow.LopName = cmbLopHoc.Text;
                newRow.SoLuongBanGiao = oldRow.SoLuong;
                newRow.NgayBanGiao = DateTime.Now;
            }

            gvLop.ShowEditForm();
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            if (this.viewBanGiaoTaiSanRowBindingSource.Current == null)
            {
                return;
            }

            viewbangiaotaisan oldRow = this.viewBanGiaoTaiSanRowBindingSource.Current as viewbangiaotaisan;

            int existedIndex = this.viewTaiSanRowBindingSource.Find("TaiSanId", oldRow.TaiSanId);

            if (existedIndex >= 0)
            {
                this.viewTaiSanRowBindingSource.Position = existedIndex;
                viewtaisan newRow = this.viewTaiSanRowBindingSource.Current as viewtaisan;
                newRow.SoLuong += (oldRow.SoLuongBanGiao == null ? 0 : oldRow.SoLuongBanGiao.Value);
            }
            else
            {
                viewtaisan newRow = this.viewTaiSanRowBindingSource.AddNew() as viewtaisan;
                newRow.Ten = oldRow.Ten;
                newRow.TaiSanId = oldRow.TaiSanId;
                newRow.SoChungTu = oldRow.SoChungTu;
                newRow.NgayChungTu = oldRow.NgayChungTu;
                newRow.DonViTinh = oldRow.DonViTinh;
                newRow.DonGia = oldRow.DonGia;
                newRow.SoLuong = oldRow.SoLuong;
                newRow.NgayNhap = oldRow.NgayNhap;
                this.viewTaiSanRowBindingSource.EndEdit();
            }

            this.viewBanGiaoTaiSanRowBindingSource.RemoveCurrent();
        }

        private void cmbLopHoc_EditValueChanged(object sender, EventArgs e)
        {
            // No "save changes" prompt: the form is read-only, there is nothing to save.
            this.loadTaiSanToGridLop();
            this.loadTaiSanToGridTaiSan();
        }

        private void gvLop_RowUpdated(object sender, RowObjectEventArgs e)
        {
            viewbangiaotaisan viewBanGiaoTaiSanRow = (viewbangiaotaisan)e.Row;
            int taiSanRowIndex = this.viewTaiSanRowBindingSource.Find("TaiSanId", viewBanGiaoTaiSanRow.TaiSanId);

            if (taiSanRowIndex >= 0)
            {
                this.viewTaiSanRowBindingSource.Position = taiSanRowIndex;
                viewtaisan taiSanRow = this.viewTaiSanRowBindingSource.Current as viewtaisan;
                double tongSoLuong;
                this._taiSanIdToSoLuongDictionary.TryGetValue(viewBanGiaoTaiSanRow.TaiSanId, out tongSoLuong);
                double soLuongBanGiao = viewBanGiaoTaiSanRow.SoLuongBanGiao == null ? 0 : viewBanGiaoTaiSanRow.SoLuongBanGiao.Value;
                taiSanRow.SoLuong = tongSoLuong - soLuongBanGiao;

                if (taiSanRow.SoLuong <= 0)
                {
                    this.viewTaiSanRowBindingSource.RemoveCurrent();
                }
            }
            else
            {
                if (Entities.Entry(viewBanGiaoTaiSanRow).State == EntityState.Modified)
                {
                    double tongSoLuong;
                    this._taiSanIdToSoLuongDictionary.TryGetValue(viewBanGiaoTaiSanRow.TaiSanId, out tongSoLuong);
                    double soLuongBanGiao = viewBanGiaoTaiSanRow.SoLuongBanGiao == null ? 0 : viewBanGiaoTaiSanRow.SoLuongBanGiao.Value;
                    double remainSoLuong = tongSoLuong - soLuongBanGiao;

                    if (remainSoLuong > 0)
                    {
                        viewtaisan newRow = this.viewTaiSanRowBindingSource.AddNew() as viewtaisan;
                        newRow.Ten = viewBanGiaoTaiSanRow.Ten;
                        newRow.TaiSanId = viewBanGiaoTaiSanRow.TaiSanId;
                        newRow.SoChungTu = viewBanGiaoTaiSanRow.SoChungTu;
                        newRow.NgayChungTu = viewBanGiaoTaiSanRow.NgayChungTu;
                        newRow.DonViTinh = viewBanGiaoTaiSanRow.DonViTinh;
                        newRow.DonGia = viewBanGiaoTaiSanRow.DonGia;
                        newRow.SoLuong = remainSoLuong;
                        newRow.NgayNhap = viewBanGiaoTaiSanRow.NgayNhap;
                        this.viewTaiSanRowBindingSource.EndEdit();
                    }
                }
            }
        }
    }
}