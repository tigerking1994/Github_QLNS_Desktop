using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDtctgBHXHModel : ModelBase
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public Guid IID_DonVi { get; set; }
        public string IID_MaDonVi { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string STenDonVi { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SLNS { get; set; }

        private double? _fTongTienTuChi;
        public double? FTongTienTuChi { get => _fTongTienTuChi; set => SetProperty(ref _fTongTienTuChi, value); }
        private double? _fTongTienTuChiTrenGiao;
        public double? FTongTienTuChiTrenGiao { get => _fTongTienTuChiTrenGiao; set => SetProperty(ref _fTongTienTuChiTrenGiao, value); }
        public double? _fTongTienHienVat;
        public double? FTongTienHienVat { get => _fTongTienHienVat; set => SetProperty(ref _fTongTienHienVat, value); }
        public double? _fTongTienKeHoach;
        public double? FTongTienKeHoach { get => _fTongTienKeHoach; set => SetProperty(ref _fTongTienKeHoach, value); }
        public double? _fTongTienBoSung;
        public double? FTongTienBoSung { get => _fTongTienBoSung; set => SetProperty(ref _fTongTienBoSung, value); }
        public double? _fTongTien;
        public double? FTongTien { get => _fTongTien; set => SetProperty(ref _fTongTien, value); }
        public int ILoaiDotNhanPhanBo { get; set; }
        public double? FSoPhanBo { get; set; }
        public double? FDaPhanBo { get; set; }
        public double? FSoChuaPhanBo { get; set; }
        public string SLoaiDotNhanPhanBo => ILoaiDotNhanPhanBo switch
        {
            1 => "Đầu năm",
            2 => "Bổ sung",
            _ => string.Empty
        };
        public string SMoTa { get; set; }

        public int INamLamViec { get; set; }

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

        public string SMaLoaiChi { get; set; }
        public string SLoaiChi { get; set; }
        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        public string SNguoiTao { get; set; }
        public DateTime SNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime DNgaySua { get; set; }

        public bool IsAdjusted { get; set; }
        public int? ILoaiChungTu { get; set; }

        public string SLoaiChungTu => ILoaiChungTu switch
        {
            1 => "Ngân sách sử dụng",
            2 => "Ngân sách đặc biệt",
            _ => string.Empty
        };
        public Guid? IIdLoaiDanhMucChi { get; set; }
    }
}