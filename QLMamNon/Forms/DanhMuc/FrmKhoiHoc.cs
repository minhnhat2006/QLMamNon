using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using DevExpress.XtraGrid.Views.Base;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.UserControls;

namespace QLMamNon.Forms.DanhMuc
{
    public partial class FrmKhoiHoc : CRUDForm<khoi>
    {
        public FrmKhoiHoc()
        {
            InitializeComponent();

            this.TablePrimaryKey = "KhoiId";
            this.DanhMuc = DanhMucConstant.KhoiHoc;
            this.FormKey = AppForms.FormDanhMucTruongHoc;

            this.gvMain.OptionsEditForm.CustomEditFormLayout = new UCEditFormKhoiHoc();
            this.loadKhoiData();
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.khoiRowBindingSource.DataSource);
        }

        private void gvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (e.Column.FieldName == "TruongId" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                Object truongIdObj = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "TruongId");

                if (truongIdObj != null)
                {
                    int truongId = (int)truongIdObj;
                    e.DisplayText = StaticDataUtil.GetTruongNameByTruongId(truongId);
                }
            }
        }

        private void loadKhoiData()
        {
            Entities.khois.Load();
            BindingList<khoi> dataTable = Entities.khois.Local.ToBindingList(); ;

            foreach (khoi row in dataTable)
            {
                int? truongId = StaticDataUtil.GetTruongIdByKhoiId(Entities, row.KhoiId);
                if (truongId.HasValue)
                {
                    row.TruongId = truongId.Value;
                }
            }

            this.khoiRowBindingSource.DataSource = dataTable;
        }

        protected override void onSaving()
        {
            List<khoi> deletedRow = new List<khoi>();
            List<khoi> addedRow = new List<khoi>();
            List<khoi> modifiedRow = new List<khoi>();

            foreach (khoi row in DataTable)
            {
                if (Entities.Entry(row).State == EntityState.Deleted)
                {
                    deletedRow.Add(row);
                }
                else if (Entities.Entry(row).State == EntityState.Added)
                {
                    addedRow.Add(row);
                }
                if (Entities.Entry(row).State == EntityState.Modified)
                {
                    modifiedRow.Add(row);
                }
            }

            foreach (khoi row in deletedRow)
            {
                Entities.deleteKhoiTruongByKhoiId(row.KhoiId);
            }

            foreach (khoi row in modifiedRow)
            {
                khoi_truong khoiTruongRow = StaticDataUtil.GetKhoiTruongByKhoiId(Entities, row.KhoiId);

                if (khoiTruongRow != null)
                {
                    Entities.deleteKhoiTruongByKhoiId(row.KhoiId);
                }

                if (row.TruongId != null)
                {
                    khoi_truong khoiTruong = new khoi_truong()
                    {
                        KhoiId = row.KhoiId,
                        TruongId = row.TruongId.Value
                    };
                    Entities.khoi_truong.Add(khoiTruong);
                }
            }

            foreach (khoi row in addedRow)
            {
                if (row.TruongId != null)
                {
                    khoi_truong khoiTruong = new khoi_truong()
                    {
                        KhoiId = row.KhoiId,
                        TruongId = row.TruongId.Value
                    };
                    Entities.khoi_truong.Add(khoiTruong);
                }
            }

            base.onSaving();

            // Reload StaticData for Khoi
            StaticDataFacade.Reload(StaticDataKeys.KhoiHoc);
        }
    }
}