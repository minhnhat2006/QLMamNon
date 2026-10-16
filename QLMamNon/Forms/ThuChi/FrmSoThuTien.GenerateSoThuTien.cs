using ACG.Core.WinForm.Util;
using DevExpress.XtraEditors;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Properties;
using QLMamNon.Service.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace QLMamNon.Forms.ThuChi
{
    public partial class FrmSoThuTien
    {
        private void btnTaoSoThuTien_Click(object sender, EventArgs e)
        {
            if (this.isValidNgayTinhAndLop())
            {
                this.showFormGenerateSoThuTiens(false);
            }
        }

        private bool isValidNgayTinh()
        {
            DateTime ngayTinh = this.GetNgayTinh();

            if (!DateTimeUtil.IsDateBetweenRange(DateTimeUtil.DateStartOfMonth(Settings.Default.AppLannchDate), DateTimeUtil.DateEndOfMonth(DateTime.Now), ngayTinh))
            {
                MessageBox.Show(String.Format("Xin vui lòng chọn năm tháng sau {0:MM/yyyy} và trước {1:MM/yyyy}", Settings.Default.AppLannchDate.AddMonths(-1), DateTime.Now.AddMonths(1)),
                    "Chọn năm tháng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }

            return true;
        }

        private bool isValidNgayTinhAndLop()
        {
            bool isValidNgayTinh = this.isValidNgayTinh();

            if (!isValidNgayTinh)
            {
                return false;
            }

            if (ControlUtil.IsEditValueNull(this.cmbLop))
            {
                MessageBox.Show("Xin vui lòng chọn Lớp", "Chọn lớp", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }

            return true;
        }

        private bool isNeedToGenerateSoThuTiens(DateTime ngayTinh, int lop, bool showMessageBox)
        {
            long genHistoryCount = Entities.countBangThuTienGenHistoryByLopAndNgayTinh(lop, ngayTinh).First().Value;

            if (genHistoryCount == 0)
            {
                if (!DateTimeUtil.IsSameMonthYear(ngayTinh, Settings.Default.AppLannchDate))
                {
                    // NgayTinh luôn là ngày cuối tháng (GetNgayTinh) và thủ tục so sánh ngày chính xác.
                    DateTime preMonthDate = DateTimeUtil.DateEndOfMonth(ngayTinh.AddMonths(-1)).Date;
                    long prevMonthGenHistoryCount = Entities.countBangThuTienGenHistoryByLopAndNgayTinh(lop, preMonthDate).First().Value;

                    if (prevMonthGenHistoryCount == 0 && showMessageBox)
                    {
                        MessageBox.Show(String.Format("Xin vui lòng tạo sổ thu tiền cho lớp {0} tháng {1:MM/yyyy} trước", cmbLop.Text, ngayTinh.AddMonths(-1)),
                        "Tạo số thu tiền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }

                return true;
            }

            return false;
        }

        private void showFormGenerateSoThuTiens(bool needToCheckGenerated)
        {
            if (this.isValidNgayTinhAndLop())
            {
                DateTime ngayTinh = this.GetNgayTinh();
                int lop = (int)this.cmbLop.EditValue;

                if (needToCheckGenerated && !this.isNeedToGenerateSoThuTiens(ngayTinh, lop, true))
                {
                    return;
                }

                using (FrmSelectHocSinhsToGenerate frmSelectHocSinhsToGenerate = new FrmSelectHocSinhsToGenerate())
                {
                    List<int> selectedHocSinhIds = new List<int>();
                    List<viewbangthutien> viewBangThuTienTable = Entities.getViewBangThuTienByNgayTinhAndLop(ngayTinh, lop).ToList();

                    foreach (viewbangthutien viewBangThuTienRow in viewBangThuTienTable)
                    {
                        selectedHocSinhIds.Add(viewBangThuTienRow.HocSinhId);
                    }

                    frmSelectHocSinhsToGenerate.GeneratedHocSinhIds = selectedHocSinhIds;

                    List<hocsinh> hocSinhTable = Entities.getHocSinhByLopAndNgay(lop, ngayTinh).ToList();
                    frmSelectHocSinhsToGenerate.HocSinhTable = hocSinhTable;
                    DialogResult result = frmSelectHocSinhsToGenerate.ShowDialog(this);

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        this.ShowGridLoadingPanel();

                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork += new DoWorkEventHandler(generateSoThuTiens);
                        bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(generatedSoThuTiens);
                        bw.RunWorkerAsync(frmSelectHocSinhsToGenerate.HocSinhRows);
                    }
                }
            }
        }

        private void generateSoThuTiens(object sender, DoWorkEventArgs e)
        {
            // Chạy trên worker thread: không được hiện MessageBox ở đây. Lỗi ném ra sẽ được
            // BackgroundWorker chuyển sang generatedSoThuTiens qua e.Error (chạy trên UI thread).
            this.persistNgayTinhAndLop();
            List<hocsinh> hocSinhRows = e.Argument as List<hocsinh>;
            SoThuTienService soThuTienService = new SoThuTienService();
            int generatedRowsCount = soThuTienService.GenerateSoThuTienByHocSinhRows(ngayTinh, hocSinhRows);

            bangthutiengenhistory bangThuTienGenHistory = new bangthutiengenhistory()
            {
                NgayTinh = ngayTinh,
                LopId = lop,
                DateCreated = DateTime.Now,
                SoTienAnSang = Settings.Default.TienAnSang,
                SoTienAnToi = Settings.Default.TienAnToi,
                SoTienAnChinh = Settings.Default.TienAnChinh
            };

            using (qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities())
            {
                entities.bangthutiengenhistories.Add(bangThuTienGenHistory);
                entities.SaveChanges();
            }
        }

        private void generatedSoThuTiens(object sender, RunWorkerCompletedEventArgs e)
        {
            this.HideGridLoadingPanel();

            if (e.Error != null)
            {
                LoggerFacade.Error(e.Error, AppForms.FormSoThuTien);
                XtraMessageBox.Show(String.Format("Tạo sổ thu tiền không thành công.\n\n{0}", e.Error.Message),
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.loadViewBangThuTiens(this.ngayTinh, this.lop);
        }
    }
}
