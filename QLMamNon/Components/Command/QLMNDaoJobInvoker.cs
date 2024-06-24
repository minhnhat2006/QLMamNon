using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QLMamNon.Components.Command.QLMNDao;

namespace QLMamNon.Components.Command
{
    public class QLMNDaoJobInvoker
    {
        public QLMNDaoCommand UpdateSoTienTruyThuOfBangThuTienCommand { get; set; }

        public void UpdateSoTienTruyThuOfBangThuTien()
        {
            this.UpdateSoTienTruyThuOfBangThuTienCommand.Execute();
        }
    }
}
