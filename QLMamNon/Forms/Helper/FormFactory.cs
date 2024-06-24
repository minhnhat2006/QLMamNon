using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QLMamNon.Constant;
using QLMamNon.Forms.Authenticate;
using QLMamNon.Forms.DanhMuc;
using QLMamNon.Forms.HocSinh;
using QLMamNon.Forms.SystemSetting;
using QLMamNon.Forms.ThuChi;
using QLMamNon.Forms.TinNhan;

namespace QLMamNon.Forms.Resource
{
    public class FormFactory
    {
        private Dictionary<string, string> formToTypes = new Dictionary<string, string>();

        private Dictionary<string, Form> forms = new Dictionary<string, Form>();

        public FormFactory()
        {
            formToTypes.Add(AppForms.FormThongTinHocSinh, typeof(FrmThongTinHocSinh).FullName);
            formToTypes.Add(AppForms.FormThongTinHocTap, typeof(FrmThongTinHocTap).FullName);
            formToTypes.Add(AppForms.FormXepLop, typeof(FrmXepLop).FullName);
            formToTypes.Add(AppForms.FormDanhMucTruongHoc, typeof(FrmTruongHoc).FullName);
            formToTypes.Add(AppForms.FormDanhMucKhoiHoc, typeof(FrmKhoiHoc).FullName);
            formToTypes.Add(AppForms.FormDanhMucLopHoc, typeof(FrmLopHoc).FullName);
            formToTypes.Add(AppForms.FormDanhMucTinhThanhPho, typeof(FrmTinhThanhPho).FullName);
            formToTypes.Add(AppForms.FormDanhMucQuanHuyen, typeof(FrmQuanHuyen).FullName);
            formToTypes.Add(AppForms.FormDanhMucPhuongXa, typeof(FrmPhuongXa).FullName);
            formToTypes.Add(AppForms.FormDanhMucPhanLoaiChi, typeof(FrmPhanLoaiChi).FullName);
            formToTypes.Add(AppForms.FormDanhMucPhanLoaiThu, typeof(FrmPhanLoaiThu).FullName);
            formToTypes.Add(AppForms.FormDanhMucKhoanThu, typeof(FrmKhoanThu).FullName);
            formToTypes.Add(AppForms.FormDanhMucKhoanThuHangNam, typeof(FrmKhoanThuHangNam).FullName);
            formToTypes.Add(AppForms.FormBangTinhPhi, typeof(FrmBangTinhPhi).FullName);
            formToTypes.Add(AppForms.FormPhieuThu, typeof(FrmPhieuThu).FullName);
            formToTypes.Add(AppForms.FormTaoPhieuChi, typeof(FrmTaoPhieuChi).FullName);
            formToTypes.Add(AppForms.FormPhieuChi, typeof(FrmPhieuChi).FullName);
            formToTypes.Add(AppForms.FormTaoPhieuThu, typeof(FrmTaoPhieuThu).FullName);
            formToTypes.Add(AppForms.FormSoThuTien, typeof(FrmSoThuTien).FullName);
            formToTypes.Add(AppForms.FormBaoCaoHoatDongTaiChinh, typeof(FrmBaoCaoHoatDongTaiChinh).FullName);
            formToTypes.Add(AppForms.FormBaoCaoChiTietHoatDongTaiChinh, typeof(FrmBaoCaoChiTietHoatDongTaiChinh).FullName);
            formToTypes.Add(AppForms.FormSoQuyTienMat, typeof(FrmSoQuyTienMat).FullName);
            formToTypes.Add(AppForms.FormBangKeThuHocPhi, typeof(FrmBangKeThuHocPhi).FullName);
            formToTypes.Add(AppForms.FormBaoCaoTinhHinhThuChi, typeof(FrmBaoCaoTinhHinhThuChi).FullName);
            formToTypes.Add(AppForms.FormTaiSan, typeof(FrmTaiSan).FullName);
            formToTypes.Add(AppForms.FormPhanBoTaiSan, typeof(FrmPhanBoTaiSan).FullName);
            formToTypes.Add(AppForms.FormSoTheoDoiTaiSan, typeof(FrmSoTheoDoiTaiSan).FullName);
            formToTypes.Add(AppForms.FormTinNhanPhuHuynh, typeof(FrmTinNhanPhuHuynh).FullName);
            formToTypes.Add(AppForms.FormLogin, typeof(FrmLogin).FullName);
            formToTypes.Add(AppForms.FormPreference, typeof(FrmPreference).FullName);
            formToTypes.Add(AppForms.FormUser, typeof(FrmUser).FullName);
            formToTypes.Add(AppForms.FormBackupDb, typeof(FrmBackupDb).FullName);
            formToTypes.Add(AppForms.FormBangKeCacLoaiChi, typeof(FrmBangKeCacLoaiChi).FullName);
            formToTypes.Add(AppForms.FormBangKeThuTrongNgay, typeof(FrmBangKeThuTrongNgay).FullName);
            formToTypes.Add(AppForms.FormDanhSachChuaNopHocPhi, typeof(FrmDanhSachChuaNopHocPhi).FullName);
        }

        public Form GetForm(string key)
        {
            bool isFormCreated = forms.ContainsKey(key);

            if (isFormCreated && forms[key].IsDisposed)
            {
                forms.Remove(key);
                isFormCreated = false;
            }

            if (!isFormCreated)
            {
                string type = formToTypes[key];
                Form frm = Activator.CreateInstance(Type.GetType(type)) as Form;
                forms.Add(key, frm);
            }

            return forms[key];
        }

        public string GetFormCaption(string key)
        {
            Form form = this.GetForm(key);

            if (form != null)
            {
                return form.Text;
            }

            return CommonConstant.EMPTY;
        }

        public void RemoveForm(string key)
        {
            if (forms.ContainsKey(key))
            {
                forms.Remove(key);
            }
        }
    }
}
