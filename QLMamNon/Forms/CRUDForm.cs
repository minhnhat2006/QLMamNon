using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Windows.Forms;

namespace QLMamNon.Forms
{
    public class CRUDForm : XtraForm
    {
        protected SimpleButton ButtonAdd { get; set; }

        protected SimpleButton ButtonEdit { get; set; }

        protected SimpleButton ButtonDelete { get; set; }

        protected SimpleButton ButtonSave { get; set; }

        protected SimpleButton ButtonCancel { get; set; }

        protected GridView GridViewMain { get; set; }

        public qlmamnonEntities Entities { get; set; }

        protected string FormKey { get; set; }

        protected string DanhMuc { get; set; }

        protected string TablePrimaryKey { get; set; }

        protected string GridViewColumnSequenceName { get; set; }
    }

    public class CRUDForm<T> : CRUDForm where T : class
    {
        #region Properties

        protected BindingList<T> DataTable { get; set; }

        #endregion

        public CRUDForm()
        {
            this.GridViewColumnSequenceName = "STT";
            bool designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (!designMode)
            {
                this.Entities = StaticDataFacade.GetQLMNEntities();
            }
        }

        #region Construction

        protected void InitForm(SimpleButton buttonAdd, SimpleButton buttonEdit, SimpleButton buttonDelete, SimpleButton buttonSave,
            SimpleButton buttonCancel, GridView gridView, object dataTable)
        {
            this.ButtonAdd = buttonAdd;
            this.ButtonCancel = buttonCancel;
            this.ButtonDelete = buttonDelete;
            this.ButtonEdit = buttonEdit;
            this.ButtonSave = buttonSave;
            this.GridViewMain = gridView;
            this.DataTable = dataTable as BindingList<T>;

            this.InitEvents();
        }

        protected void InitEvents()
        {
            this.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            this.Activated += new EventHandler(Form_Activated);

            if (this.ButtonAdd != null)
            {
                this.ButtonAdd.Click += new System.EventHandler(this.btnAdd_Click);
            }
            if (this.ButtonEdit != null)
            {
                this.ButtonEdit.Click += new System.EventHandler(this.btnEdit_Click);
            }
            if (this.ButtonDelete != null)
            {
                this.ButtonDelete.Click += new System.EventHandler(this.btnDelete_Click);
            }
            if (this.ButtonSave != null)
            {
                this.ButtonSave.Click += new System.EventHandler(this.btnSave_Click);
            }
            if (this.ButtonCancel != null)
            {
                this.ButtonCancel.Click += new System.EventHandler(this.btnCancel_Click);
            }
            if (this.Entities != null)
            {
                //this.Entities.RowUpdated += new MySqlRowUpdatedEventHandler(onRowUpdated);
            }
            if (this.GridViewMain != null)
            {
                this.GridViewMain.ShowingPopupEditForm += new ShowingPopupEditFormEventHandler(GridViewMain_ShowingPopupEditForm);
                this.GridViewMain.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(GridViewMain_CustomDrawCell);
                this.GridViewMain.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(GridViewMain_CellValueChanged);
                this.GridViewMain.RowDeleting += new DevExpress.Data.RowDeletingEventHandler(GridViewMain_RowDeleting);
                this.GridViewMain.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(GridViewMain_RowUpdated);
                ModuleMediatorFacade.Invoke(this, WorkFlowActions.AdjustPopupEditForm, this.GridViewMain);
            }

            ModuleMediatorFacade.Invoke(this, WorkFlowActions.InitFormTracker, this);
        }

        #endregion

        #region Events

        protected void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormMainFacade.CloseForm(FormKey);
        }

        protected void Form_Activated(object sender, EventArgs e)
        {
            FormMainFacade.SetFormCaption(this.FormKey);
        }

        //protected void onRowUpdated(object sender, MySqlRowUpdatedEventArgs e)
        //{
        //    // Conditionally execute this code block on inserts only. 
        //    if (e.StatementType == StatementType.Insert)
        //    {
        //        MySqlCommand cmdNewID = new MySqlCommand("SELECT @@IDENTITY", this.Entities.SelectCommand.Connection);
        //        e.Row[this.TablePrimaryKey] = cmdNewID.ExecuteScalar();
        //        e.Status = UpdateStatus.SkipCurrentRow;
        //    }
        //}

        protected void GridViewMain_ShowingPopupEditForm(object sender, DevExpress.XtraGrid.Views.Grid.ShowingPopupEditFormEventArgs e)
        {
            e.EditForm.ControlBox = false;
        }

        protected void GridViewMain_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            if (e.Column.Caption == this.GridViewColumnSequenceName)
            {
                if (e.RowHandle >= 0)
                {
                    e.DisplayText = (e.RowHandle + 1).ToString();
                }
                else
                {
                    e.DisplayText = "";
                }
            }
        }

        protected void GridViewMain_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.ModifiedCaption);
        }

        protected void GridViewMain_RowDeleting(object sender, DevExpress.Data.RowDeletingEventArgs e)
        {
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.ModifiedCaption);
        }

        protected void GridViewMain_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.ModifiedCaption);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.onAdding();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            this.onEditing();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.onDeleting();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.onSaving();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.onCanceling();
        }

        #endregion

        #region CRUD

        protected virtual void onAdding()
        {
            BindingSource bindingSource = this.GridViewMain.GridControl.DataSource as BindingSource;
            bindingSource.AddNew();
            this.GridViewMain.GridControl.DataSource = GridControl.NewItemRowHandle;
            this.GridViewMain.ShowEditForm();

            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.AddedCaption);
        }

        protected virtual void onEditing()
        {
            this.GridViewMain.ShowPopupEditForm();
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.ModifiedCaption);
        }

        protected virtual void onSaving()
        {
            try
            {
                if (this.DataTable != null)
                {
                    foreach (T row in this.DataTable)
                    {
                        if (Entities.Entry<T>(row).State == EntityState.Detached)
                        {
                            Entities.Entry<T>(row).State = EntityState.Added;
                        }
                    }

                    this.Entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                this.onSavingError(ex);
            }

            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SavedCaption);
        }

        protected virtual void onSavingError(Exception ex)
        {
            if (ex is DBConcurrencyException)
            {
                this.reloadDataTable();
            }
        }

        protected virtual void reloadDataTable() { }

        protected virtual void onDeleting()
        {
            if (this.GridViewMain.FocusedRowHandle < 0)
            {
                return;
            }

            var confirmResult = MessageBox.Show(String.Format("Bạn có chắc muốn xóa {0} được chọn không?", this.DanhMuc), String.Format("Xóa {0}", this.DanhMuc),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                T row = GridViewMain.GetRow(GridViewMain.FocusedRowHandle) as T;
                this.GridViewMain.DeleteSelectedRows();

                if (Entities.Entry<T>(row).State != EntityState.Deleted)
                {
                    Entities.Entry<T>(row).State = EntityState.Deleted;
                }

                FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.DeletedCaption);
            }
        }

        protected virtual void onCanceling()
        {
            BindingSource bindingSource = this.GridViewMain.GridControl.DataSource as BindingSource;
            List<T> rowsToRemove = new List<T>();
            List<T> rowsToAdd = new List<T>();

            if (this.DataTable != null)
            {
                foreach (T row in this.DataTable)
                {
                    EntityState rowState = Entities.Entry<T>(row).State;

                    if (rowState == EntityState.Detached || rowState == EntityState.Added)
                    {
                        rowsToRemove.Add(row);
                    }
                    else if (rowState == EntityState.Deleted)
                    {
                        Entities.Entry<T>(row).Reload();
                        rowsToAdd.Add(row);
                    }
                    else if (rowState == EntityState.Modified)
                    {
                        Entities.Entry<T>(row).Reload();
                    }
                }

                foreach (T row in rowsToAdd)
                {
                    bindingSource.Add(row);
                }

                foreach (T row in rowsToRemove)
                {
                    bindingSource.Remove(row);
                }

                bindingSource.ResetBindings(false);
            }

            fillRelativeMainDataTable();

            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.CanceledCaption);
        }

        #endregion

        #region Additional utils

        protected virtual void fillRelativeMainDataTable()
        {
        }

        protected void ShowGridLoadingPanel()
        {
            this.GridViewMain.ShowLoadingPanel();
        }

        protected void HideGridLoadingPanel()
        {
            this.GridViewMain.HideLoadingPanel();
        }

        #endregion
    }
}
