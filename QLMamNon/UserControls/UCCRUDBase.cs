using DevExpress.XtraGrid.Views.Grid;
using QLMamNon.Dao;

namespace QLMamNon.UserControls
{
    public partial class UCCRUDBase : EditFormUserControl
    {
        #region Properties

        public qlmamnonEntities Entities { get; set; }

        public GridView GridView { get; set; }

        #endregion

        public UCCRUDBase() { }
    }
}
