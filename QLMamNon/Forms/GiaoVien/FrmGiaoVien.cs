using System;
using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Facade;
using QLMamNon.Service.Data;
using QLMamNon.UserControls;

namespace QLMamNon.Forms.GiaoVien
{
    public partial class FrmGiaoVien : CRUDForm
    {
        #region Properties

        #endregion

        #region Events

        public FrmGiaoVien()
            : base()
        {
            InitializeComponent();

            this.TablePrimaryKey = "GiaoVienId";
            this.DanhMuc = DanhMucConstant.ThongTinGiaoVien;
            this.FormKey = AppForms.FormGiaoVien;

            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormGiaoVien();
            //this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, 
            //    this.giaoVienTableAdapter.Adapter, this.giaoVienRowBindingSource.DataSource as GiaoVienDataTable);
        }

        private void FrmGiaoVien_Load(object sender, EventArgs e)
        {
            this.quanHuyenRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.QuanHuyen);
            this.loadGiaoVien(null, null, null, null);
        }

        private void gvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "GioiTinh" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object objGioiTinh = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "GioiTinh");
                if (objGioiTinh != DBNull.Value)
                {
                    sbyte gioiTinh = (sbyte)objGioiTinh;
                    switch (gioiTinh)
                    {
                        case 0: e.DisplayText = "Nữ"; break;
                        case 1: e.DisplayText = "Nam"; break;
                    }
                }
            }
            else if (e.Column.FieldName == "TinhTPId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object tinhId = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "TinhTPId");
                if (tinhId != DBNull.Value)
                {
                    //e.DisplayText = StaticDataUtil.GetThanhPhoById(StaticDataFacade.Get(StaticDataKeys.TinhThanhPho) as ThanhPhoDataTable, (int)tinhId);
                }
            }
            else if (e.Column.FieldName == "QuanHuyenId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object quanHuyenId = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "QuanHuyenId");
                if (quanHuyenId != DBNull.Value)
                {
                    //e.DisplayText = StaticDataUtil.GetQuanHuyenById(StaticDataFacade.Get(StaticDataKeys.QuanHuyen) as QuanHuyenDataTable, (int)quanHuyenId);
                }
            }
            else if (e.Column.FieldName == "PhuongXaId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object phuongXaId = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "PhuongXaId");
                if (phuongXaId != DBNull.Value)
                {
                    //e.DisplayText = StaticDataUtil.GetPhuongXaById(StaticDataFacade.Get(StaticDataKeys.PhuongXa) as PhuongXaDataTable, (int)phuongXaId);
                }
            }
        }

        private void cmbQuanHuyen_EditValueChanged(object sender, EventArgs e)
        {
            /*
            PhuongXaDataTable table = StaticDataFacade.Get(StaticDataKeys.PhuongXa) as PhuongXaDataTable;
            if (cmbQuanHuyen.EditValue != DBNull.Value && cmbQuanHuyen.EditValue != null)
            {
                this.phuongXaRowBindingSource.DataSource = table.Select(String.Format("QuanHuyenId={0}", cmbQuanHuyen.EditValue));
            }
            else
            {
                this.phuongXaRowBindingSource.DataSource = null;
            }
            */
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.loadGiaoVien(null, (int?)cmbQuanHuyen.EditValue, (int?)cmbPhuongXa.EditValue, null);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.cmbQuanHuyen.EditValue = null;
            this.cmbPhuongXa.EditValue = null;
        }

        #endregion

        #region Validation

        #endregion

        #region Helper

        private void loadGiaoVien(int? tinhTPId, int? quan, int? phuong, DateTime? ngaySinh)
        {
            /*
            GiaoVienService giaVienService = new GiaoVienService();
            GiaoVienDataTable giaoVienTable = giaVienService.LoadGiaoVien(giaoVienTableAdapter, tinhTPId, quan, phuong, ngaySinh);
            giaoVienTable.CreatedDateColumn.DefaultValue = DateTime.Now;
            this.DataTable = giaoVienTable;
            this.giaoVienRowBindingSource.DataSource = this.DataTable;
            */
        }

        #endregion
    }
}