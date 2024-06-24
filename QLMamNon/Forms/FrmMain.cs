using DevExpress.XtraBars;
using QLMamNon.Components.Data.Static;
using QLMamNon.Components.ModuleMediator.Channel;
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
using QLMamNon.Workflow.Forms;
using System;

namespace QLMamNon.Forms
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private qlmamnonEntities entities;

        #region Public Methods

        public void SetManHinhCaption(string caption)
        {
            this.bsiManHinh.Caption = caption;
        }

        public void SetTrangThaiCaption(string caption)
        {
            this.bsiTrangThai.Caption = caption;
        }

        #endregion

        #region Events

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            IChannelManager formChannelManager = new FormChannelManager();
            formChannelManager.InitSubscribers(this);

            IFormWorkFlow formWorkFlow = new FrmMainFlow();
            formWorkFlow.InitWorkFlow(this);

            FormMainFacade.InitFormMain(this);

            this.entities = new qlmamnonEntities();

            this.initStaticData();
            this.ribbon.Minimized = true;
        }

        private void initStaticData()
        {
            StaticDataFacade.Add(StaticDataKeys.TinhThanhPho, new TinhThanhPhoData(entities));
            StaticDataFacade.Add(StaticDataKeys.PhuongXa, new PhuongXaData(entities));
            StaticDataFacade.Add(StaticDataKeys.QuanHuyen, new QuanHuyenData(entities));
            StaticDataFacade.Add(StaticDataKeys.TruongHoc, new TruongData(entities));
            StaticDataFacade.Add(StaticDataKeys.NamHoc, new NamHocData());
            StaticDataFacade.Add(StaticDataKeys.KhoiHoc, new KhoiData(entities));
            StaticDataFacade.Add(StaticDataKeys.LopHoc, new LopData(entities));
            StaticDataFacade.Add(StaticDataKeys.KhoanThu, new KhoanThuData(entities));
            StaticDataFacade.Add(StaticDataKeys.PhanLoaiChi, new PhanLoaiChi(entities));
            StaticDataFacade.Add(StaticDataKeys.PhanLoaiThu, new PhanLoaiThu(entities));
            StaticDataFacade.Add(StaticDataKeys.BangTinhPhi, new BangTinhPhi(entities));
            StaticDataFacade.Add(StaticDataKeys.TrangThaiHS, new TrangThaiHSData());
            StaticDataFacade.Add(StaticDataKeys.AdapterBangTinhPhi, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterBangThuTien, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterViewBangThuTien, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterHocSinh, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterHocSinhLop, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterLopKhoi, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterPhieuChi, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterPhieuThu, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterBangThuTienGenHistory, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterBangThuTienKhoanThu, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterUnknownColumnView, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterKhoanThuHangNam, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterUser, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterPrivilege, this.entities);
            StaticDataFacade.Add(StaticDataKeys.AdapterUserPrivilege, this.entities);
        }

        private void bbtnHocSinhThongTin_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormThongTinHocSinh);
        }

        private void bbtnHocSinhThongTinHocTap_ItemClick(object sender, ItemClickEventArgs e)
        {
            //FormMainFacade.ShowForm(AppForms.FormThongTinHocTap);
        }

        private void bbtnHocSinhXepLop_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormXepLop);
        }

        private void bbiTruongHoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormDanhMucTruongHoc);
        }

        private void bbiKhoiHoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormDanhMucKhoiHoc);
        }

        private void bbiLopHoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormDanhMucLopHoc);
        }

        private void bbiTinhThanhPho_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormDanhMucTinhThanhPho);
        }

        private void bbiQuanHuyen_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormDanhMucQuanHuyen);
        }

        private void bbiPhuongXa_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormDanhMucPhuongXa);
        }

        private void bbiTaoPhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormTaoPhieuThu);
        }

        private void bbiTaoPhieuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormTaoPhieuChi);
        }

        private void bbiPhanLoaiChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormDanhMucPhanLoaiChi);
        }

        private void bbiDSPhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormPhieuThu);
        }

        private void bbiDSPhieuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormPhieuChi);
        }

        private void bbiSoThuTienHS_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormSoThuTien);
        }

        private void bbiKhoanThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormDanhMucKhoanThu);
        }

        private void bbiKhoanThuHangNam_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormDanhMucKhoanThuHangNam);
        }

        private void bbiRptBaoCaoHoatDongTaiChinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormBaoCaoHoatDongTaiChinh);
        }

        private void bbiRptBaoCaoChiTietHoatDongTaiChinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormBaoCaoChiTietHoatDongTaiChinh);
        }

        private void bbiTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormTaiSan);
        }

        private void bbiPhanBoTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormPhanBoTaiSan);
        }

        private void bbiSoTheoDoiTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormSoTheoDoiTaiSan);
        }

        private void bbiPhuHuynhHS_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormTinNhanPhuHuynh);
        }

        private void bbỉptSoQuyTienMat_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormSoQuyTienMat);
        }

        private void bbiBaoCaoTinhHinhThuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormBaoCaoTinhHinhThuChi);
        }

        private void bbiBangKeThuHocPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormBangKeThuHocPhi);
        }

        private void bbiPreference_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormPreference);
        }

        private void bbiUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormUser);
        }

        private void bbiBangTinhPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormBangTinhPhi);
        }

        private void bbiPhanLoaiThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowForm(AppForms.FormDanhMucPhanLoaiThu);
        }

        private void bbiBangKeCacLoaiChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormBangKeCacLoaiChi);
        }

        private void bbiBangKeThuTrongNgay_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormBangKeThuTrongNgay);
        }

        private void bbiBackupDb_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormBackupDb);
        }

        private void bbiDanhSachHocSinhChuaNopHocPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMainFacade.ShowDialog(AppForms.FormDanhSachChuaNopHocPhi);
        }

        #endregion
    }
}