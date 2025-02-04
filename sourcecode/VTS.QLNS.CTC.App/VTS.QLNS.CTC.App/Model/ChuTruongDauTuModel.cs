using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ChuTruongDauTuModel : ModelBase
    {
        public string SSoQuyetDinh { get; set; }
        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }
        public string SSoToTrinh { get; set; }
        public DateTime? DNgayToTrinh { get; set; }
        public string SSoThamDinh { get; set; }
        public DateTime? DNgayThamDinh { get; set; }
        public string SCoQuanThamDinh { get; set; }
        public string SCoQuanPheDuyet { get; set; }
        public string SNguoiKy { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public double? FTmdtduKienToTrinh { get; set; }
        public double? FTmdtduKienThamDinh { get; set; }
        public double? FTmdtduKienPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SLoaiDieuChinh { get; set; }
        public string SKhoiCong { get; set; }
        public string SHoanThanh { get; set; }
        public string STenDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public string SDienTichSuDungDat { get; set; }
        public string SNguonGocSuDungDat { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string SSuCanThietDauTu { get; set; }

        private string _sMucTieu;
        public string SMucTieu
        {
            get => _sMucTieu;
            set => SetProperty(ref _sMucTieu, value);
        }

        private string _sQuyMo;
        public string SQuyMo
        {
            get => _sQuyMo;
            set => SetProperty(ref _sQuyMo, value);
        }
        public Guid? IIdDonViThucHienId { get; set; }
        public Guid? IIdLoaiDuAn { get; set; }
        public Guid? IIdHinhThucDauTuId { get; set; }
        public Guid? IIdHinhThucQuanLyId { get; set; }
        public Guid? IIdNhomDuAnId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
       
        public bool? BIsDelete { get; set; }
        //private bool _isLocked;
        //public bool IsLocked
        //{
        //    get => _isLocked;
        //    set => SetProperty(ref _isLocked, value);
        //}
        public string IdDonvi { get; set; }
        public string TenDonVi { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public string IIdMaChuDauTuId { get; set; }
        public string TenChuDauTu { get; set; }
        public string TenCapPheDuyet { get; set; }
        public bool? BIsGoc { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool BActive { get; set; }
        public int? ILanDieuChinh { get; set; }
        public string sSoLanDieuChinh
        {
            get
            {
                return string.Format("({0})", (ILanDieuChinh ?? 0));
            }
        }
        public int TotalFiles { get; set; }
        public string SMoTa { get; set; }
        public string SUserCreate { get; set; }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        public string Loai => BIsGoc.GetValueOrDefault() ? "Gốc" : "Điều chỉnh";
    }
}
