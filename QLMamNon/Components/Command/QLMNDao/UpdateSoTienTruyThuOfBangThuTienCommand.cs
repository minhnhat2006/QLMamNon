using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;

namespace QLMamNon.Components.Command.QLMNDao
{
    public class UpdateSoTienTruyThuOfBangThuTienCommand : QLMNDaoCommand
    {
        public const string ParameterCurrentViewBangThuTien = "CurrentViewBangThuTien";
        public const string ParameterSoTienAdded = "SoTienAdded";

        protected override void onExecute()
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            viewbangthutien currentViewBangThuTien = (viewbangthutien)CommandParameter[ParameterCurrentViewBangThuTien];
            long soTienAdded = (long)this.CommandParameter[ParameterSoTienAdded];
            entities.updateSoTienTruyThuByHocSinhAndNgayUpdated(currentViewBangThuTien.HocSinhId, currentViewBangThuTien.NgayTinh, soTienAdded);
        }
    }
}
