using System;
using QLMamNon.Components.Data.Static;
using QLMamNon.Facade;
using ACG.Core.WinForm.Util;
using QLMamNon.Constant;

namespace QLMamNon.UserControls
{
    public partial class UCEditFormXepLop : UCCRUDBase
    {
        public UCEditFormXepLop()
        {
            InitializeComponent();
        }

        private void UCEditFormXepLop_Load(object sender, EventArgs e)
        {
            this.phanLoaiChiRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.PhanLoaiChi);
        }

        private void UCEditFormXepLop_Enter(object sender, EventArgs e)
        {
        }
    }
}
