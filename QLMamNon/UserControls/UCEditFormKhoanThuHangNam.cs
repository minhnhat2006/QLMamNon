using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Facade;
using QLMamNon.Components.Data.Static;

namespace QLMamNon.UserControls
{
    public partial class UCEditFormKhoanThuHangNam : UCCRUDBase
    {
        public UCEditFormKhoanThuHangNam()
        {
            InitializeComponent();
        }

        private void UCEditFormKhoanThuHangNam_Load(object sender, EventArgs e)
        {
            this.khoanThuRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.KhoanThu);
            this.khoiRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.KhoiHoc);
        }
    }
}
