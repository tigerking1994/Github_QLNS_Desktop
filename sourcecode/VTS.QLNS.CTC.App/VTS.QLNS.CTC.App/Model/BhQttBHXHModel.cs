using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQttBHXHModel : BindableBase
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SMoTa { get; set; }
        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }
        public int IQuyNam { get; set; }
        public int IQuyNamLoai { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SQuyNamMoTa { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public string sDSMLNS { get; set; }
        public int? ILoaiTongHop { get; set; }
        public int? ILoai { get; set; }
        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }
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
        public bool IsChildSumary { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }
        public bool IsChildSummary { get; set; }
        public string SoChungTuParent { get; set; }
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
        private double? _fTongQuyTienLuongNam;
        public double? FTongQuyTienLuongNam
        {
            get => _fTongQuyTienLuongNam;
            set
            {
                SetProperty(ref _fTongQuyTienLuongNam, value);
            }
        }
        private double? _fDuToan;
        public double? FDuToan
        {
            get => _fDuToan;
            set
            {
                SetProperty(ref _fDuToan, value);
            }
        }
        private double? _fDaQuyetToan;
        public double? FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set
            {
                SetProperty(ref _fDaQuyetToan, value);
            }
        }
        private double? _fConLai;
        public double? FConLai
        {
            get => _fConLai;
            set
            {
                SetProperty(ref _fConLai, value);
            }
        }
        private double? _fThuBHXHNLD;
        public double? FThuBHXHNLD
        {
            get => _fThuBHXHNLD;
            set
            {
                SetProperty(ref _fThuBHXHNLD, value);
            }
        }
        private double? _fThuBHXHNSD;
        public double? FThuBHXHNSD
        {
            get => _fThuBHXHNSD;
            set
            {
                SetProperty(ref _fThuBHXHNSD, value);
            }
        }
        private double? _fTongSoPhaiThuBHXH;
        public double? FTongSoPhaiThuBHXH
        {
            get => _fTongSoPhaiThuBHXH;
            set
            {
                SetProperty(ref _fTongSoPhaiThuBHXH, value);
            }
        }

        private double? _fThuBHYTNLD;
        public double? FThuBHYTNLD
        {
            get => _fThuBHYTNLD;
            set
            {
                SetProperty(ref _fThuBHYTNLD, value);
            }
        }
        private double? _fThuBHYTNSD;
        public double? FThuBHYTNSD
        {
            get => _fThuBHYTNSD;
            set
            {
                SetProperty(ref _fThuBHYTNSD, value);
            }
        }
        private double? _fTongSoPhaiThuBHYT;
        public double? FTongSoPhaiThuBHYT
        {
            get => _fTongSoPhaiThuBHYT;
            set
            {
                SetProperty(ref _fTongSoPhaiThuBHYT, value);
            }
        }

        private double? _fThuBHTNNLD;
        public double? FThuBHTNNLD
        {
            get => _fThuBHTNNLD;
            set
            {
                SetProperty(ref _fThuBHTNNLD, value);
            }
        }
        private double? _fThuBHTNNSD;
        public double? FThuBHTNNSD
        {
            get => _fThuBHTNNSD;
            set
            {
                SetProperty(ref _fThuBHTNNSD, value);
            }
        }
        private double? _fTongSoPhaiThuBHTN;
        public double? FTongSoPhaiThuBHTN
        {
            get => _fTongSoPhaiThuBHTN;
            set
            {
                SetProperty(ref _fTongSoPhaiThuBHTN, value);
            }
        }
        private double? _fTongCong;
        public double? FTongCong
        {
            get => _fTongCong;
            set
            {
                SetProperty(ref _fTongCong, value);
            }
        }
        private double? _fSoPhaiThuBHXHNLD;
        public double? FSoPhaiThuBHXHNLD
        {
            get => _fSoPhaiThuBHXHNLD;
            set
            {
                SetProperty(ref _fSoPhaiThuBHXHNLD, value);
            }
        }
        private double? _fSoPhaiThuBHXHNSD;
        public double? FSoPhaiThuBHXHNSD
        {
            get => _fSoPhaiThuBHXHNSD;
            set
            {
                SetProperty(ref _fSoPhaiThuBHXHNSD, value);
            }
        }
        private double? _fTongPhaiThuBHXH;
        public double? FTongPhaiThuBHXH
        {
            get => _fTongPhaiThuBHXH;
            set
            {
                SetProperty(ref _fTongPhaiThuBHXH, value);
            }
        }
        private bool _isFilter;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }
    }
}
