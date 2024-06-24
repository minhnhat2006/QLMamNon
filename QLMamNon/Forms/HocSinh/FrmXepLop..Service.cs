using ACG.Core.WinForm.Util;
using QLMamNon.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace QLMamNon.Forms.HocSinh
{
    public partial class FrmXepLop
    {
        private bool isHocSinhExisted(BindingSource hocSinhRowBindingSource, int hocSinhId)
        {
            foreach (hocsinh row in hocSinhRowBindingSource)
            {
                if (hocSinhId == row.HocSinhId)
                {
                    return true;
                }
            }

            return false;
        }

        private void loadHocSinhToGridDi()
        {
            int? namHoc = (int?)this.cmbNamHocDi.EditValue;
            int? lopHoc = (int?)this.cmbLopHocDi.EditValue;
            List<hocsinh> hocSinhTable;

            if (lopHoc == null)
            {
                hocSinhTable = Entities.getHocSinhNotAssignedToLop().ToList();
            }
            else
            {
                hocSinhTable = Entities.getHocSinhByParams(new DateTime(namHoc.Value + 1, 1, 1), null, lopHoc, null).ToList();
            }

            ThongTinHocSinhUtil.EvaluateLopInfoForHocSinhTable(Entities, hocSinhTable);

            this.hocSinhRowBindingSourceDi.DataSource = new BindingList<hocsinh>(hocSinhTable);
        }

        private void loadHocSinhToGridDen()
        {
            if (ControlUtil.IsEditValueNull(this.cmbLopHocDen))
            {
                return;
            }

            List<hocsinh> hocSinhTable = Entities.getHocSinhByParams(DateTime.Now, null, (int)this.cmbLopHocDen.EditValue, null).ToList();

            ThongTinHocSinhUtil.EvaluateLopInfoForHocSinhTable(Entities, hocSinhTable);

            this.hocSinhRowBindingSourceDen.DataSource = new BindingList<hocsinh>(hocSinhTable);
        }
    }
}
