using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDaGoiThauModel : DetailModelBase
    {
        //public Guid Id { get; set; }
        public Guid? IdGoiThau { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string STenDuAn { get; set; }
        public string SMaGoiThau { get; set; }
        public string STenGoiThau { get; set; }
        public string LoaiGoiThau { get; set; }
        public string SHinhThucChonNhaThau { get; set; }
        public string SPhuongThucDauThau { get; set; }
        public string SHinhThucHopDong { get; set; }
        public string SThoiGianThucHien { get; set; }
        public DateTime? DNgayLap { get; set; }
        public Guid? IIdGoiThauGocId { get; set; }
        //public Guid? IIdParentId { get; set; }
        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set => SetProperty(ref _iIdParentId, value);
        }
        public bool? BIsGoc { get; set; }
        public bool? BActive { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public string TenNhaThau { get; set; }
        private double? _fTienTrungThau;
        public double? FTienTrungThau
        {
            get => _fTienTrungThau;
            set => SetProperty(ref _fTienTrungThau, value);
        }
        public string TenDonVi { get; set; }
        public string Id_DonVi { get; set; }
        public string SDiaDiem { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public int? SoLanDieuChinh { get; set; }
        public double? FTongTienSauDieuChinh { get; set; }
        public double? FTongMucDauTu { get; set; }

        private string _thoiGianThucHien;
        public string ThoiGianThucHien
        {
            get => _thoiGianThucHien;
            set => SetProperty(ref _thoiGianThucHien, value);
        }

        private double _fGiaTriTruocDieuChinh;
        public double FGiaTriTruocDieuChinh
        {
            get => _fGiaTriTruocDieuChinh;
            set => SetProperty(ref _fGiaTriTruocDieuChinh, value);
        }

        public DateTime? DBatDauChonNhaThau { get; set; }
        public DateTime? DKetThucChonNhaThau { get; set; }
        public string STenNhomQuanLy { get; set; }
        public string SSuCanThietDauTu { get; set; }
        public string SMucTieu { get; set; }
        public string SDienTichSuDungDat { get; set; }
        public string SNguonGocSuDungDat { get; set; }
        public string SQuyMo { get; set; }
        public string STenNhomDuAn { get; set; }
        public string STenHinhThucQuanLy { get; set; }
        public bool IsDieuChinh { get; set; }
        public bool IsUpdate { get; set; }
        public string SoQuyetDinh { get; set; }
        //public string TenDuToan { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public Guid? IIdKhlcnhaThau { get; set; }
        public string SUserCreate { get; set; }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        private VdtDaGoiThauModel _selectedGoiThauParent;
        public VdtDaGoiThauModel SelectedGoiThauParent
        {
            get => _selectedGoiThauParent;
            set => SetProperty(ref _selectedGoiThauParent, value);
        }
    }
}
