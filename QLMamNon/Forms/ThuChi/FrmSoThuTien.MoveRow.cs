using QLMamNon.Dao;
using System;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmSoThuTien
    {
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (this.viewBangThuTienRowBindingSource.Current == null)
            {
                return;
            }

            this.setCurrentViewBangThuTienRowSTT(this.viewBangThuTienRowBindingSource.Current, 0);
            this.viewBangThuTienRowBindingSource.MoveNext();

            for (int i = 1; i < this.GridViewMain.RowCount; i++)
            {
                this.setCurrentViewBangThuTienRowSTT(this.viewBangThuTienRowBindingSource[i], i + 1);
            }

            this.viewBangThuTienRowBindingSource.MoveFirst();
            this.setCurrentViewBangThuTienRowSTT(this.viewBangThuTienRowBindingSource.Current, 1);
        }

        private void setCurrentViewBangThuTienRowSTT(object current, int stt)
        {
            viewbangthutien bangThuTienRow = current as viewbangthutien;

            if (bangThuTienRow.STT != stt)
            {
                bangThuTienRow.STT = stt;
            }

            this.GridViewMain_CellValueChanged(null, null);
        }

        private int getCurrentViewBangThuTienRowSTT(object current)
        {
            viewbangthutien bangThuTienRow = current as viewbangthutien;
            return bangThuTienRow.STT;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this.viewBangThuTienRowBindingSource.Current == null)
            {
                return;
            }

            int stt = this.getCurrentViewBangThuTienRowSTT(this.viewBangThuTienRowBindingSource.Current);

            if (stt == 1)
            {
                return;
            }

            object row = this.GridViewMain.GetRow(this.GridViewMain.FocusedRowHandle);
            object prevRow = this.GridViewMain.GetRow(this.GridViewMain.FocusedRowHandle - 1);
            this.setCurrentViewBangThuTienRowSTT(prevRow, stt);
            this.setCurrentViewBangThuTienRowSTT(row, stt - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.viewBangThuTienRowBindingSource.Current == null)
            {
                return;
            }

            int stt = this.getCurrentViewBangThuTienRowSTT(this.viewBangThuTienRowBindingSource.Current);

            if (stt == this.viewBangThuTienRowBindingSource.Count)
            {
                return;
            }

            object row = this.GridViewMain.GetRow(this.GridViewMain.FocusedRowHandle);
            object nextRow = this.GridViewMain.GetRow(this.GridViewMain.FocusedRowHandle + 1);
            this.setCurrentViewBangThuTienRowSTT(nextRow, stt);
            this.setCurrentViewBangThuTienRowSTT(row, stt + 1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this.viewBangThuTienRowBindingSource.Current == null)
            {
                return;
            }

            this.setCurrentViewBangThuTienRowSTT(this.viewBangThuTienRowBindingSource.Current, this.viewBangThuTienRowBindingSource.Count + 1);
            this.viewBangThuTienRowBindingSource.MoveFirst();

            for (int i = 0; i < this.GridViewMain.RowCount; i++)
            {
                this.setCurrentViewBangThuTienRowSTT(this.viewBangThuTienRowBindingSource.Current, i + 1);
                this.viewBangThuTienRowBindingSource.MoveNext();
            }
        }
    }
}
