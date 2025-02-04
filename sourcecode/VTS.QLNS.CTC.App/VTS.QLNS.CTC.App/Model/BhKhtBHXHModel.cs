using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhKhtBHXHModel : BindableBase
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string IID_MaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SMoTa { get; set; }
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
        public double? FTongKeHoach { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public Guid? IIDTongHopID { get; set; }
        public int? ILoaiTongHop { get; set; }
        public bool IsChildSummary { get; set; }
        public string SoChungTuParent { get; set; }

        private bool _isExpand;
        public bool IsExpand
        {
            get => _isExpand;
            set => SetProperty(ref _isExpand, value);
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public int Index { get; set; }
        public int? ILoaiChungTu { get; set; }
        public string SDssoChungTuTongHop { get; set; }
        public bool IsChildSumary { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        private double? _fThuBHXHNLDDong;
        public double? FThuBHXHNLDDong
        {
            get => _fThuBHXHNLDDong;
            set
            {
                SetProperty(ref _fThuBHXHNLDDong, value);
            }
        }
        private double? _fThuBHXHNSDDong;
        public double? FThuBHXHNSDDong
        {
            get => _fThuBHXHNSDDong;
            set
            {
                SetProperty(ref _fThuBHXHNSDDong, value);
            }
        }
        private double? _fThuBHXH;
        public double? FThuBHXH
        {
            get => _fThuBHXH;
            set
            {
                SetProperty(ref _fThuBHXH, value);
            }
        }
        private double? _fThuBHYTNLDDong;
        public double? FThuBHYTNLDDong
        {
            get => _fThuBHYTNLDDong;
            set
            {
                SetProperty(ref _fThuBHYTNLDDong, value);
            }
        }
        private double? _fThuBHYTNSDDong;
        public double? FThuBHYTNSDDong
        {
            get => _fThuBHYTNSDDong;
            set
            {
                SetProperty(ref _fThuBHYTNSDDong, value);
            }
        }
        private double? _fTongBHYT;
        public double? FTongBHYT
        {
            get => _fTongBHYT;
            set
            {
                SetProperty(ref _fTongBHYT, value);
            }
        }
        private double? _fThuBHTNNLDDong;
        public double? FThuBHTNNLDDong
        {
            get => _fThuBHTNNLDDong;
            set
            {
                SetProperty(ref _fThuBHTNNLDDong, value);
            }
        }
        private double? _fThuBHTNNSDDong;
        public double? FThuBHTNNSDDong
        {
            get => _fThuBHTNNSDDong;
            set
            {
                SetProperty(ref _fThuBHTNNSDDong, value);
            }
        }
        private double? _fThuBHTN;
        public double? FThuBHTN
        {
            get => _fThuBHTN;
            set
            {
                SetProperty(ref _fThuBHTN, value);
            }
        }
        private double? _fTong;
        public double? FTong
        {
            get => _fTong;
            set
            {
                SetProperty(ref _fTong, value);
            }
        }
        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }
        private int? _iQSBQNam;
        public int? IQSBQNam
        {
            get => _iQSBQNam;
            set
            {
                SetProperty(ref _iQSBQNam, value);
            }
        }
        private double? _fLuongChinh;
        public double? FLuongChinh
        {
            get => _fLuongChinh;
            set
            {
                SetProperty(ref _fLuongChinh, value);
            }
        }
        private double? _fPhuCapChucVu;
        public double? FPhuCapChucVu
        {
            get => _fPhuCapChucVu;
            set
            {
                SetProperty(ref _fPhuCapChucVu, value);
            }
        }
        private double? _fPCTNNghe;
        public double? FPCTNNghe
        {
            get => _fPCTNNghe;
            set
            {
                SetProperty(ref _fPCTNNghe, value);
            }
        }
        private double? _fPCTNVuotKhung;
        public double? FPCTNVuotKhung
        {
            get => _fPCTNVuotKhung;
            set
            {
                SetProperty(ref _fPCTNVuotKhung, value);
            }
        }
        private double? _fNghiOm;
        public double? FNghiOm
        {
            get => _fNghiOm;
            set
            {
                SetProperty(ref _fNghiOm, value);
            }
        }
        private double? _fHSBL;
        public double? FHSBL
        {
            get => _fHSBL;
            set
            {
                SetProperty(ref _fHSBL, value);
            }
        }
        private double? _fTongQTLN;
        public double? FTongQTLN
        {
            get => _fTongQTLN;
            set
            {
                SetProperty(ref _fTongQTLN, value);
            }
        }
        public string SBangLuongKeHoach { get; set; }
    }
}