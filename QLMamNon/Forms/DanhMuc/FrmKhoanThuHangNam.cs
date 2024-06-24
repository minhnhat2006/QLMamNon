using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.UserControls;
using System;
using System.Data.Entity;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmKhoanThuHangNam : CRUDForm<khoanthuhangnam>
    {
        public FrmKhoanThuHangNam()
        {
            InitializeComponent();

            this.TablePrimaryKey = "KhoanThuHangNamId";
            this.DanhMuc = DanhMucConstant.KhoanThuHangNam;
            this.FormKey = AppForms.FormDanhMucKhoanThuHangNam;
            Entities.khoanthuhangnams.Load();
            this.khoanThuHangNamRowBindingSource.DataSource = Entities.khoanthuhangnams.Local.ToBindingList();
            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormKhoanThuHangNam();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.khoanThuHangNamRowBindingSource.DataSource);
        }

        private void FrmKhoanThuHangNam_Load(object sender, EventArgs e)
        {

        }

        private void gvMain_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "KhoiId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                object khoiIdObj = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "KhoiId");

                if (khoiIdObj != null)
                {
                    int khoiId = (int)khoiIdObj;

                    if (khoiId > 0)
                    {
                        e.DisplayText = StaticDataUtil.GetKhoiNameByKhoiId(khoiId);
                    }
                }
            }
            else if (e.Column.FieldName == "KhoanThuId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                Object khoanThuIdObj = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "KhoanThuId");

                if (khoanThuIdObj != null)
                {
                    int khoanThuId = (int)khoanThuIdObj;
                    e.DisplayText = StaticDataUtil.GetKhoanThuNameByKhoanThuId(khoanThuId);
                }
            }
        }
    }
}