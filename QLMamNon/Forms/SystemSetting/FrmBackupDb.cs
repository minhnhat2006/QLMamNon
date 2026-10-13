using System;
using System.Threading.Tasks;
using QLMamNon.Constant;
using QLMamNon.Facade;
using QLMamNon.Properties;
using System.IO;
using System.Windows.Forms;

namespace QLMamNon.Forms.Authenticate
{
    public partial class FrmBackupDb : DevExpress.XtraEditors.XtraForm
    {
        public FrmBackupDb()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void FrmBackupDb_Load(object sender, EventArgs e)
        {
            this.simpleButton1.Enabled = false;
            this.ControlBox = false;
            this.progressBarControl1.EditValue = 0;
            string timestamp = (DateTime.Now).ToString("yyyyMMddHHmmssffff");
            string sqlFileName = Path.Combine(Path.GetTempPath(), String.Format("qlmn_backup_{0}.sql", timestamp));
            this.lblState.Text = String.Format("Tạo file {0}...", sqlFileName);

            // Each await resumes on the UI thread; a failed stage returns, so later stages never run.
            try
            {
                await Task.Run(() => MySqlFacade.BackupDb(Settings.Default.qlmamnonConnectionString1, sqlFileName));
            }
            catch (Exception ex)
            {
                onBackupFailed("Tạo file sao lưu không thành công", sqlFileName, ex);
                return;
            }

            onCreateMySqlFileCompleted(sqlFileName);

            try
            {
                await Task.Run(() => GDriveFacade.UploadFile("", sqlFileName));
            }
            catch (Exception ex)
            {
                onBackupFailed("Upload file lên GDrive không thành công", sqlFileName, ex);
                return;
            }

            onUploadMySqlFileCompleted(sqlFileName);
        }

        private void onBackupFailed(string stageMessage, string sqlFileName, Exception ex)
        {
            LoggerFacade.Error(ex, AppForms.FormBackupDb);
            this.lblState.Text = "Sao lưu thất bại: " + stageMessage;
            this.simpleButton1.Enabled = true;
            this.ControlBox = true;
            deleteTempFile(sqlFileName);
            MessageBox.Show(String.Format("{0}. Chi tiết: {1}", stageMessage, ex.Message), "Sao lưu dữ liệu không thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void deleteTempFile(string sqlFileName)
        {
            try
            {
                if (File.Exists(sqlFileName))
                {
                    File.Delete(sqlFileName);
                }
            }
            catch (Exception ex)
            {
                LoggerFacade.Error(ex, AppForms.FormBackupDb);
            }
        }

        private string onCreateMySqlFileCompleted(string sqlFileName)
        {
            this.progressBarControl1.EditValue = 50;
            this.lblState.Text = String.Format("Upload file lên GDrive {0}...", sqlFileName);
            return sqlFileName;
        }

        private string onUploadMySqlFileCompleted(string sqlFileName)
        {
            this.progressBarControl1.EditValue = 100;
            this.lblState.Text = "Hoàn thành";
            this.simpleButton1.Enabled = true;
            this.ControlBox = true;
            deleteTempFile(sqlFileName);
            return sqlFileName;
        }
    }
}