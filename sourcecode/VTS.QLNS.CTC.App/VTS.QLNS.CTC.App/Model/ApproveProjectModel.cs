using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ApproveProjectModel : ModelBase
    {
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string NgayQuyetDinhString { get; set; }
        public string sCoQuSCoQuanPheDuyetanPheDuyet { get; set; }
        public string SNguoiKy { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string SSoToTrinh { get; set; }
        public DateTime? DNgayToTrinh { get; set; }
        public string SSoThamDinh { get; set; }
        public DateTime? DNgayThamDinh { get; set; }
        public string SCoQuanThamDinh { get; set; }
        public double? FTongMucDauTuPheDuyet { get; set; }

        private string _sTenDuAn;
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }
        public string TenDonVi { get; set; }
        public string Id_DonVi { get; set; }
        public double? fTongMucDauTuSauDieuChinh { get; set; }
        public int? iSoLanDieuChinh { get; set; }
        public string sSoLanDieuChinh
        {
            get
            {
                return string.Format("({0})", (iSoLanDieuChinh ?? 0));
            }
        }
        private bool _isLocked;
        public bool IsLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        private string _sMaDuAn;
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        public string SDiaDiem { get; set; }
        public Guid? IIdNhomDuAnId { get; set; }
        public Guid? IIdHinhThucQuanLyId { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public string TenNhomDuAn { get; set; }
        public string TenHinhThucQL { get; set; }
        public string TenChuDauTu { get; set; }
        public string IdChuDTString { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public string IIdMaChuDauTuId { get; set; }
        public double? FTongMucDauTuChuTruong { get; set; }
        public string Loai { get; set; }
        public bool? BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public int TotalFiles { get; set; }
        public string SMoTa { get; set; }
        public string SUserCreate { get; set; }

        private int _iLoaiQuyetDinh;
        public int ILoaiQuyetDinh
        {
            get => _iLoaiQuyetDinh;
            set => SetProperty(ref _iLoaiQuyetDinh, value);
        }

        private string _sSoBuocThietKe;
        public string SSoBuocThietKe
        {
            get => _sSoBuocThietKe;
            set => SetProperty(ref _sSoBuocThietKe, value);
        }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }
        public Guid? IIdChuTruongDauTuId { get; set; }
    }
}
