using System;
using QLMamNon.Components.Data.Static;
using QLMamNon.Facade;
using ACG.Core.WinForm.Util;
using QLMamNon.Constant;

namespace QLMamNon.UserControls
{
    public partial class UCEditFormBanGiaoTaiSan : UCCRUDBase
    {
        public UCEditFormBanGiaoTaiSan()
        {
            InitializeComponent();
        }

        private void UCEditFormBanGiaoTaiSan_Load(object sender, EventArgs e)
        {
            this.phanLoaiChiRowBindingSource.DataSource = StaticDataFacade.Get(StaticDataKeys.PhanLoaiChi);
        }

        private void UCEditFormBanGiaoTaiSan_Enter(object sender, EventArgs e)
        {
        }
    }
}
