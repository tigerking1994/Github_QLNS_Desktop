using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDuToanModel : ModelBase
    {
        public Guid? IIdDuAnId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SNoiDung { get; set; }
        public double? FTongDuToanPheDuyet { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BActive { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool BLaTongDuToan { get; set; }
        public Guid? IIdDuToanGocId { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string STenDuAn { get; set; }
        public string TenDonVi { get; set; }
        public Guid? Id_DonVi { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public string ThoiGianThucHien { get; set; }
        public double? FTongMucDauTuSauDieuChinh { get; set; }
        public int? SoLanDieuChinh { get; set; }
        public string sSoLanDieuChinh
        {
            get
            {
                return string.Format("({0})", (SoLanDieuChinh ?? 0));
            }
        }
        public string DiaDiem { get; set; }
        public bool IsAdd { get; set; }
        public string TenDuToan { get; set; }
        public string LoaiDuToan { get; set; }
        public string Loai { get; set; }
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }
        public int TotalFiles { get; set; }
        public string SMoTa { get; set; }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        private bool _bIsDuToan;
        public bool BIsDuToan
        {
            get => _bIsDuToan;
            set => SetProperty(ref _bIsDuToan, value);
        }

        public Guid? IIdQddauTuId { get; set; }

        [NotMapped]
        public string sTenChuDauTu { get; set; }
    }
}
