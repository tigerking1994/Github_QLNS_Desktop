using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhPbdtcBHXHModel : BindableBase
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SLNS { get; set; }
        public string SDotNhan { get; set; }
        public string SID_MaDonVi { get; set; }
        private Double? _fTongTienTuChi;
        public Double? FTongTienTuChi { get => _fTongTienTuChi; set => SetProperty(ref _fTongTienTuChi, value); }
        public Double? _fTongTienHienVat;
        public Double? FTongTienHienVat { get => _fTongTienHienVat; set => SetProperty(ref _fTongTienHienVat, value); }
        public Double? _fTongTien;
        public Double? FTongTien { get => _fTongTien; set => SetProperty(ref _fTongTien, value); }

        private Double? _fTongTienTuChiTruocDieuChinh;
        public Double? FTongTienTuChiTruocDieuChinh { get => _fTongTienTuChiTruocDieuChinh; set => SetProperty(ref _fTongTienTuChiTruocDieuChinh, value); }

        private Double? _fTongTienTuChiSauDieuChinh;
        public Double? FTongTienTuChiSauDieuChinh { get => _fTongTienTuChiSauDieuChinh; set => SetProperty(ref _fTongTienTuChiSauDieuChinh, value); }

        private Double? _fTongTienHienVatTruocDieuChinh;
        public Double? FTongTienHienVatTruocDieuChinh { get => _fTongTienHienVatTruocDieuChinh; set => SetProperty(ref _fTongTienHienVatTruocDieuChinh, value); }

        private Double? _fTongTienHienVatSauDieuChinh;
        public Double? FTongTienHienVatSauDieuChinh { get => _fTongTienHienVatSauDieuChinh; set => SetProperty(ref _fTongTienHienVatSauDieuChinh, value); }
        public int? ILoaiDotNhanPhanBo { get; set; }

        public string SLoaiDotNhanPhanBo => ILoaiDotNhanPhanBo switch
        {
            1 => "Đầu năm",
            2 => "Bổ sung",
            _ => string.Empty
        };

        public int ILoaiChungTu { get; set; }
        public string SLoaiChungTu => ILoaiChungTu switch
        {
            1 => "Ngân sách sử dụng",
            2 => "Ngân sách đặc thù ngành",
            _ => string.Empty
        };
        public string SMoTa { get; set; }

        public int INamChungTu { get; set; }

        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }
        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        public string SNguoiTao { get; set; }
        public DateTime DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime DNgaySua { get; set; }
        public string IID_DotNhan { get; set; }
        public Guid? IIDLoaiDanhMucChi { get; set; }
        public string SMaLoaiChi { get; set; }

        public string SLoaiChi { get; set; }
    }
}