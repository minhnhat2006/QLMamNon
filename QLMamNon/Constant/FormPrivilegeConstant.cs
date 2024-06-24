using System.Collections.Generic;

namespace QLMamNon.Constant
{
    public static class FormPrivilegeConstant
    {
        public const int XemDanhSachPhieuChi = 1;
        public const int XemDanhSachPhieuThu = 2;
        public const int XemSoQuyTienMat = 3;
        public const int XemBaoCaoThuChi = 4;
        public const int ManageUser = 5;
        public const int AddEditPhieuThu = 6;

        public static Dictionary<string, int> FormKeyToPrivilegeId = new Dictionary<string, int>()
        {
            {AppForms.FormSoQuyTienMat, XemSoQuyTienMat},
            {AppForms.FormBaoCaoTinhHinhThuChi, XemBaoCaoThuChi},
            {AppForms.FormTaoPhieuThu, AddEditPhieuThu},
            {AppForms.FormUser, ManageUser }
        };
    }
}
