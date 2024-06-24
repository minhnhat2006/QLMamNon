using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using ACG.Core.WinForm.Util;
using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Service.SMS;

namespace QLMamNon.Forms.TinNhan
{
    public partial class FrmTinNhanPhuHuynh : DevExpress.XtraEditors.XtraForm
    {
        private const string PhraseHocSinhName = "{Ten_HS}";
        private const string PhraseFatherName = "{Ten_Cha}";
        private const string PhraseMotherName = "{Ten_Me}";

        private qlmamnonEntities entities;

        public FrmTinNhanPhuHuynh()
        {
            InitializeComponent();
        }

        private void FrmTinNhanPhuHuynh_Load(object sender, EventArgs e)
        {
            entities = StaticDataFacade.GetQLMNEntities();
            fillTxtGuide();
            fillCmbPort();
            this.lopBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.LopHoc);
            entities.smstemplates.Load();
            this.smstemplateBindingSource.DataSource = entities.smstemplates.Local.ToBindingList();
            this.cmbLop_EditValueChanged(sender, e);
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            txtNoiDung.Text = (string)gridView1.GetFocusedRowCellValue("Template");
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtNoiDung.Text = (string)gridView1.GetFocusedRowCellValue("Template");
        }

        private void gridControl1_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.EndEdit)
            {
                entities.SaveChanges();
            }
        }

        private void fillCmbPort()
        {
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                cmbPort.Items.Add(port);
            }

            if (ports.Length > 0)
            {
                cmbPort.SelectedIndex = 0;
                cmbPort.Text = ports[0];
            }
        }

        private void fillTxtGuide()
        {
            object[,] textAndColors = new object[8, 2]
            {
                {"Vui lòng sử dụng mẫu tin nhắn với cú pháp như sau: ", Color.Black},
                {"{Tên đơn vị} ", Color.Blue},
                {"{Từ khóa} ", Color.Green},
                {"{Nội dung tin nhắn}\n", Color.Orange},
                {"Ví dụ tin nhẵn mẫu: ", Color.Black},
                {"Trường MN Hồng Minh ", Color.Blue},
                {"Thông báo ", Color.Green},
                {"Thứ 5 ngày 20/11, các bé nghỉ học do trường tổ chức ngày Nhà giáo Việt nam", Color.Orange}
            };

            for (int i = 0; i < textAndColors.GetLength(0); i++)
            {
                string text = (string)textAndColors[i, 0];
                Color color = (Color)textAndColors[i, 1];
                txtGuide.SelectionStart = txtGuide.TextLength;
                txtGuide.SelectionLength = text.Length;
                txtGuide.SelectionColor = color;
                txtGuide.AppendText(text);
            }
        }

        private void cmbLop_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbLop.EditValue == null)
            {
                this.hocsinhBindingSource.DataSource = entities.getHocSinhByLopAndNgay(null, DateTime.Now);
            }
            else
            {
                this.hocsinhBindingSource.DataSource = entities.getHocSinhByLopAndNgay((int)this.cmbLop.EditValue, DateTime.Now);
            }

            this.gvMain.SelectAll();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (StringUtil.IsEmpty(this.txtNoiDung.Text))
            {
                MessageBox.Show("Xin vui lòng nhập nội dung tin nhắn", "Nội dung tin nhắn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Dictionary<string, string> phoneNumberToSmsContentDic = new Dictionary<string, string>();
            int[] selectedRowHandlers = this.gvMain.GetSelectedRows();

            if (ArrayUtil.IsEmpty(selectedRowHandlers))
            {
                MessageBox.Show("Xin vui lòng chọn học sinh", "Chọn học sinh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (int rowHandler in selectedRowHandlers)
            {
                string soDienThoai = "";
                object sdtCha = gvMain.GetRowCellValue(rowHandler, "DienThoai");
                object sdtMe = gvMain.GetRowCellValue(rowHandler, "DienThoaiMe");
                string hocSinhName = (string)gvMain.GetRowCellValue(rowHandler, "HoTen");
                string fatherName = (string)gvMain.GetRowCellValue(rowHandler, "HoTenCha");
                string motherName = (string)gvMain.GetRowCellValue(rowHandler, "HoTenMe");
                string smsContent = txtNoiDung.Text.Replace(PhraseHocSinhName, hocSinhName).Replace(PhraseFatherName, fatherName).Replace(PhraseMotherName, motherName);

                if (sdtMe != null)
                {
                    soDienThoai = (string)sdtMe;
                }

                if (!StringUtil.IsEmpty(soDienThoai))
                {
                    phoneNumberToSmsContentDic.Add($"{soDienThoai}-{rowHandler}", smsContent);
                }
                else
                {
                    if (sdtCha != null)
                    {
                        soDienThoai = (string)sdtCha;

                        if (!StringUtil.IsEmpty(soDienThoai))
                        {
                            phoneNumberToSmsContentDic.Add($"{soDienThoai}-{rowHandler}", smsContent);
                        }
                    }
                }
            }

            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }

                serialPort1.PortName = cmbPort.Text;
                serialPort1.NewLine = Environment.NewLine;

                btnGui.Text = "Đang gửi";
                btnGui.Enabled = false;

                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync(phoneNumberToSmsContentDic);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Xuất hiện lỗi trong quá trình gửi tin nhắn. Chi tiết: {0}", ex.Message), "Gửi tin nhắn không thành công", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSend.Text = "Gửi";
                btnSend.Enabled = true;
            }
        }

        private void txtNoiDung_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.lblKiTu.Text = String.Format("Nội dung tin nhắn tối đa 160 kí tự (số kí tự còn lại: {0})", StringUtil.IsEmpty(this.txtNoiDung.Text) ? 160 : 160 - this.txtNoiDung.Text.Length);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }

                serialPort1.PortName = cmbPort.Text;
                serialPort1.NewLine = Environment.NewLine;
                serialPort1.Open();
                MessageBox.Show("Kết nối thành công!", "Kiểm tra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                string errorMsg = $"{ex.Message}\n{ex.StackTrace}";
                Console.WriteLine(errorMsg);
                MessageBox.Show(errorMsg, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Dictionary<string, string> phoneNumberToSmsContentDic = (Dictionary<string, string>)e.Argument;
                ISMS sms = new USB3GSms(serialPort1, (int)txtInterval.Value);
                Dictionary<string, bool> sentStatuses = sms.SendSMS(phoneNumberToSmsContentDic);
                string pathToLogFile = $"{AppDomain.CurrentDomain.BaseDirectory}sms_log.txt";

                foreach (var item in sentStatuses)
                {
                    string phoneNumberKey = item.Key;
                    string phoneNumber = phoneNumberKey.Substring(0, phoneNumberKey.IndexOf("-"));
                    File.AppendAllText(pathToLogFile, $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}    {phoneNumber}    {phoneNumberToSmsContentDic[item.Key]}    {item.Value} \r\n");
                }

                MessageBox.Show("Tin nhắn đã được gửi", "Gửi tin nhắn thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    String.Format("Xuất hiện lỗi trong quá trình gửi tin nhắn. Chi tiết: {0}", ex.Message),
                    "Gửi tin nhắn không thành công", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnGui.Text = "Gửi";
            btnGui.Enabled = true;
        }
    }
}