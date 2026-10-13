using QLMamNon.Components.Data.Static;
using QLMamNon.Dao;
using QLMamNon.Facade;

namespace QLMamNon.Components.Command.QLMNDao
{
    public class UpdateSoTienTruyThuOfBangThuTienCommand : QLMNDaoCommand
    {
        public const string ParameterCurrentViewBangThuTien = "CurrentViewBangThuTien";
        public const string ParameterSoTienAdded = "SoTienAdded";

        /// <summary>
        /// Optional. When supplied, the stored procedure runs on the caller's context so that it
        /// enlists in any transaction the caller has opened. When omitted, behaviour is unchanged:
        /// the command creates its own context.
        /// </summary>
        public const string ParameterEntities = "Entities";

        protected override void onExecute()
        {
            qlmamnonEntities entities = CommandParameter.ContainsKey(ParameterEntities)
                ? (qlmamnonEntities)CommandParameter[ParameterEntities]
                : StaticDataFacade.GetQLMNEntities();
            viewbangthutien currentViewBangThuTien = (viewbangthutien)CommandParameter[ParameterCurrentViewBangThuTien];
            long soTienAdded = (long)this.CommandParameter[ParameterSoTienAdded];
            entities.updateSoTienTruyThuByHocSinhAndNgayUpdated(currentViewBangThuTien.HocSinhId, currentViewBangThuTien.NgayTinh, soTienAdded);
        }
    }
}
