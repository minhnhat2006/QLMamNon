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
        // Ngày vào lớp của từng dòng ở lưới "chuyển đến" tại thời điểm nạp lưới, khóa theo HocSinhId.
        // Giá trị chụp lại là giá trị của editor (hocsinh.NgayVaoLop - cột mà UCEditFormXepLop bind vào),
        // nên "người dùng đã sửa ngày vào lớp" đúng bằng "NgayVaoLop khác giá trị lúc nạp".
        private readonly Dictionary<int, DateTime?> ngayVaoLopSnapshots = new Dictionary<int, DateTime?>();

        private bool isHocSinhExisted(BindingSource hocSinhRowBindingSource, int hocSinhId)
        {
            return findHocSinhById(hocSinhRowBindingSource, hocSinhId) != null;
        }

        // Tìm theo id ngay trên BindingSource để không phụ thuộc kiểu danh sách bên dưới
        // (lưới "chuyển đi" là BindingList<hocsinh>, không phải List<hocsinh>).
        private static hocsinh findHocSinhById(BindingSource hocSinhRowBindingSource, int hocSinhId)
        {
            foreach (hocsinh row in hocSinhRowBindingSource)
            {
                if (hocSinhId == row.HocSinhId)
                {
                    return row;
                }
            }

            return null;
        }

        private static void removeHocSinhById(BindingSource hocSinhRowBindingSource, int hocSinhId)
        {
            hocsinh row = findHocSinhById(hocSinhRowBindingSource, hocSinhId);
            if (row != null)
            {
                hocSinhRowBindingSource.Remove(row);
            }
        }

        // Chụp lại ngày vào lớp của các dòng vừa nạp. Chỉ gọi khi lưới thực sự được nạp lại, để ảnh chụp
        // luôn khớp với những gì lưới đang hiển thị.
        private void takeNgayVaoLopSnapshots(List<hocsinh> hocSinhTable)
        {
            this.ngayVaoLopSnapshots.Clear();

            foreach (hocsinh row in hocSinhTable)
            {
                this.ngayVaoLopSnapshots[row.HocSinhId] = row.NgayVaoLop;
            }
        }

        // Người dùng có sửa ngày vào lớp của dòng này trong form xếp lớp hay không. Dòng mới thêm vào lưới
        // không có ảnh chụp: trạng thái EF Added/Detached đã đủ để kích hoạt insertHocSinhToLop.
        private bool isNgayVaoLopChangedByUser(hocsinh hocSinhRow)
        {
            DateTime? ngayVaoLopSnapshot;
            if (!this.ngayVaoLopSnapshots.TryGetValue(hocSinhRow.HocSinhId, out ngayVaoLopSnapshot))
            {
                return false;
            }

            return ngayVaoLopSnapshot != hocSinhRow.NgayVaoLop;
        }

        private void loadHocSinhToGridDi()
        {
            int? namHoc = null;
            if (!ControlUtil.IsEditValueNull(this.cmbNamHocDi))
            {
                namHoc = (int?)this.cmbNamHocDi.EditValue;
            }

            int? lopHoc = (int?)this.cmbLopHocDi.EditValue;
            List<hocsinh> hocSinhTable;

            if (lopHoc == null)
            {
                hocSinhTable = Entities.getHocSinhNotAssignedToLop().ToList();
            }
            else
            {
                if (namHoc == null)
                {
                    MessageBox.Show("Xin vui lòng chọn Năm học", "Chọn năm học", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

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

            this.takeNgayVaoLopSnapshots(hocSinhTable);
        }
    }
}
