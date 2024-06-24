using System;
using System.Threading.Tasks;
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

        private void FrmBackupDb_Load(object sender, EventArgs e)
        {
            var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.simpleButton1.Enabled = false;
            this.progressBarControl1.EditValue = 0;
            string timestamp = (DateTime.Now).ToString("yyyyMMddHHmmssffff");
            string sqlFileName = Path.GetTempPath() + String.Format("\\qlmn_backup_{0}.sql", timestamp);
            this.lblState.Text = String.Format("Tạo file {0}...", sqlFileName);
            var result = Task.Factory.StartNew(() =>
                {
                    MySqlFacade.BackupDb(Settings.Default.qlmamnonConnectionString1, sqlFileName);
                    return sqlFileName;
                }).
                ContinueWith((x) =>
                {
                    onCreateMySqlFileCompleted(sqlFileName);
                    return sqlFileName;
                }, uiScheduler).
                ContinueWith((x) =>
                {
                    try
                    {
                        GDriveFacade.UploadFile("", sqlFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    return sqlFileName;
                }).
                ContinueWith((x) =>
                {
                    return onUploadMySqlFileCompleted(sqlFileName);
                }, uiScheduler);
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
            File.Delete(sqlFileName);
            return sqlFileName;
        }
    }
}