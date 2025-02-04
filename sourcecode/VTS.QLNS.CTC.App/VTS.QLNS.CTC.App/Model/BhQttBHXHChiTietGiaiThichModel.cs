using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQttBHXHChiTietGiaiThichModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid? QttBHXHId { get; set; }
        public int? INamLamViec { get; set; }
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        private DateTime? _dTuNgay;
        public DateTime? DTuNgay 
        {
            get => _dTuNgay;
            set
            {
                SetProperty(ref _dTuNgay, value);
            }
        }
        private DateTime? _dDenNgay;
        public DateTime? DDenNgay
        {
            get => _dDenNgay;
            set
            {
                SetProperty(ref _dDenNgay, value);
            }
        }
        public int IQuyNam { get; set; }
        public int IQuyNamLoai { get; set; }
        public int? ILoaiGiaiThich { get; set; }
        public string SQuyNamMoTa { get; set; }
        public string SNoiDung { get; set; }
        public string SKienNghi { get; set; }
        public string SLNS { get; set; }
        public string SXauNoiMa { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public Guid? IIDMLNS { get; set; }
        public Guid IIDMLNSCha { get; set; }
        public string STenBhMLNS { get; set; }
        public string SLoaiThu { get; set; }
        public bool BHangCha { get; set; }
        private int _iSTT;
        public int ISTT
        {
            get => _iSTT;
            set
            {
                SetProperty(ref _iSTT, value);
            }
        }

        private int? _iQuanSo;
        public int? IQuanSo
        {
            get => _iQuanSo.GetValueOrDefault();
            set
            {
                SetProperty(ref _iQuanSo, value);
            }
        }

        private double? _fPhaiNopBHXH;
        public double? FPhaiNopBHXH
        {
            get
            {
                return FPhaiNopTrongQuyNam.GetValueOrDefault() + FTruyThuQuyNamTruoc.GetValueOrDefault() + FPhaiNopQuyNamTruoc.GetValueOrDefault();
            }
            set
            {
                SetProperty(ref _fPhaiNopBHXH, value);
            }
        }

        private double? _fPhaiNopTrongQuyNam;
        public double? FPhaiNopTrongQuyNam
        {
            get => _fPhaiNopTrongQuyNam.GetValueOrDefault();
            set
            {
                SetProperty(ref _fPhaiNopTrongQuyNam, value);
                OnPropertyChanged(nameof(FPhaiNopBHXH));
            }
        }
        private double? _fTruyThuQuyNamTruoc;
        public double? FTruyThuQuyNamTruoc
        {
            get => _fTruyThuQuyNamTruoc.GetValueOrDefault();
            set
            {
                SetProperty(ref _fTruyThuQuyNamTruoc, value);
                OnPropertyChanged(nameof(FPhaiNopBHXH));
            }
        }
        private double? _fPhaiNopQuyNamTruoc;
        public double? FPhaiNopQuyNamTruoc
        {
            get => _fPhaiNopQuyNamTruoc.GetValueOrDefault();
            set
            {
                SetProperty(ref _fPhaiNopQuyNamTruoc, value);
                OnPropertyChanged(nameof(FPhaiNopBHXH));
            }
        }
        private double? _fDaNopTrongQuyNam;
        public double? FDaNopTrongQuyNam
        {
            get => _fDaNopTrongQuyNam;
            set
            {
                SetProperty(ref _fDaNopTrongQuyNam, value);
            }
        }
        private double? _fConPhaiNopTiep;
        public double? FConPhaiNopTiep
        {
            get => _fConPhaiNopTiep;
            set
            {
                SetProperty(ref _fConPhaiNopTiep, value);
            }
        }
        private double? _fTruyThuBHXHNLD;
        public double? FTruyThuBHXHNLD
        {
            get => _fTruyThuBHXHNLD.GetValueOrDefault();
            set
            {
                SetProperty(ref _fTruyThuBHXHNLD, value);
                OnPropertyChanged(nameof(FTruyThuBHXHTongCong));
                OnPropertyChanged(nameof(FTongTruyThuBHXH));
            }
        }
        private double? _fTruyThuBHXHNSD;
        public double? FTruyThuBHXHNSD
        {
            get => _fTruyThuBHXHNSD.GetValueOrDefault();
            set
            {
                SetProperty(ref _fTruyThuBHXHNSD, value);
                OnPropertyChanged(nameof(FTruyThuBHXHTongCong));
                OnPropertyChanged(nameof(FTongTruyThuBHXH));
            }
        }
        public double? FTruyThuBHXHTongCong => FTruyThuBHXHNLD.GetValueOrDefault() + FTruyThuBHXHNSD.GetValueOrDefault();

        private double? _fTruyThuBHYTNLD;
        public double? FTruyThuBHYTNLD
        {
            get => _fTruyThuBHYTNLD.GetValueOrDefault();
            set
            {
                SetProperty(ref _fTruyThuBHYTNLD, value);
                OnPropertyChanged(nameof(FTruyThuBHYTTongCong));
                OnPropertyChanged(nameof(FTongTruyThuBHXH));
            }
        }
        private double? _fTruyThuBHYTNSD;
        public double? FTruyThuBHYTNSD
        {
            get => _fTruyThuBHYTNSD.GetValueOrDefault();
            set
            {
                SetProperty(ref _fTruyThuBHYTNSD, value);
                OnPropertyChanged(nameof(FTruyThuBHYTTongCong));
                OnPropertyChanged(nameof(FTongTruyThuBHXH));
            }
        }
        public double? FTruyThuBHYTTongCong => FTruyThuBHYTNLD.GetValueOrDefault() + FTruyThuBHYTNSD.GetValueOrDefault();

        private double? _fTruyThuBHTNNLD;
        public double? FTruyThuBHTNNLD
        {
            get => _fTruyThuBHTNNLD.GetValueOrDefault();
            set
            {
                SetProperty(ref _fTruyThuBHTNNLD, value);
                OnPropertyChanged(nameof(FTruyThuBHTNTongCong));
                OnPropertyChanged(nameof(FTongTruyThuBHXH));
            }
        }
        private double? _fTruyThuBHTNNSD;
        public double? FTruyThuBHTNNSD
        {
            get => _fTruyThuBHTNNSD.GetValueOrDefault();
            set
            {
                SetProperty(ref _fTruyThuBHTNNSD, value);
                OnPropertyChanged(nameof(FTruyThuBHTNTongCong));
                OnPropertyChanged(nameof(FTongTruyThuBHXH));
            }
        }
        public double? FTruyThuBHTNTongCong => FTruyThuBHTNNLD.GetValueOrDefault() + FTruyThuBHTNNSD.GetValueOrDefault();

        public double? FTongTruyThuBHXH
        {
            get => FTruyThuBHXHTongCong + FTruyThuBHYTTongCong.GetValueOrDefault() + FTruyThuBHTNTongCong.GetValueOrDefault();
        }
        private double? _fSoPhaiThuNop;
        public double? FSoPhaiThuNop
        {
            get => _fSoPhaiThuNop.GetValueOrDefault();
            set
            {
                SetProperty(ref _fSoPhaiThuNop, value);
                OnPropertyChanged(nameof(FSoConPhaiNop));
            }
        }
        private double? _fSoDaNopTrongNam;
        public double? FSoDaNopTrongNam
        {
            get => _fSoDaNopTrongNam.GetValueOrDefault();
            set
            {
                SetProperty(ref _fSoDaNopTrongNam, value);
                OnPropertyChanged(nameof(FTongSoDaNop));
                OnPropertyChanged(nameof(FSoConPhaiNop));
            }
        }
        private double? _fSoDaNopSau3112;
        public double? FSoDaNopSau3112
        {
            get => _fSoDaNopSau3112.GetValueOrDefault();
            set
            {
                SetProperty(ref _fSoDaNopSau3112, value);
                OnPropertyChanged(nameof(FTongSoDaNop));
                OnPropertyChanged(nameof(FSoConPhaiNop));
            }
        }
        public double? FTongSoDaNop => FSoDaNopTrongNam.GetValueOrDefault() + FSoDaNopSau3112.GetValueOrDefault();

        public double? FSoConPhaiNop => FSoPhaiThuNop.GetValueOrDefault() - (FSoDaNopTrongNam.GetValueOrDefault() + FSoDaNopSau3112.GetValueOrDefault());

        private double? _fQuyTienLuongCanCu;
        public double? FQuyTienLuongCanCu
        {
            get => _fQuyTienLuongCanCu.GetValueOrDefault();
            set
            {
                SetProperty(ref _fQuyTienLuongCanCu, value);
            }
        }
        private double? _fSoTienGiamDong;
        public double? FSoTienGiamDong
        {
            get => _fSoTienGiamDong.GetValueOrDefault();
            set
            {
                SetProperty(ref _fSoTienGiamDong, value);
            }
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }

        private bool _isModify;
        public bool IsModify
        {
            get => _isModify;
            set => SetProperty(ref _isModify, value);
        }

        private bool _isDelete;
        public bool IsDelete
        {
            get => _isDelete;
            set => SetProperty(ref _isDelete, value);
        }

        private double? _fLuongChinh;
        public double? FLuongChinh
        {
            get => _fLuongChinh.GetValueOrDefault();
            set
            {
                SetProperty(ref _fLuongChinh, value);
                OnPropertyChanged(nameof(FTongQuyLuong));
            }
        }

        private double? _fPCChucVu;
        public double? FPCChucVu
        {
            get => _fPCChucVu.GetValueOrDefault();
            set
            {
                SetProperty(ref _fPCChucVu, value);
                OnPropertyChanged(nameof(FTongQuyLuong));
            }
        }

        private double? _fPCTNNghe;
        public double? FPCTNNghe
        {
            get => _fPCTNNghe.GetValueOrDefault();
            set
            {
                SetProperty(ref _fPCTNNghe, value);
                OnPropertyChanged(nameof(FTongQuyLuong));
            }
        }

        private double? _fPCTNVuotKhung;
        public double? FPCTNVuotKhung
        {
            get => _fPCTNVuotKhung.GetValueOrDefault();
            set
            {
                SetProperty(ref _fPCTNVuotKhung, value);
                OnPropertyChanged(nameof(FTongQuyLuong));
            }
        }

        private double? _fNghiOm;
        public double? FNghiOm
        {
            get => _fNghiOm.GetValueOrDefault();
            set
            {
                SetProperty(ref _fNghiOm, value);
                OnPropertyChanged(nameof(FTongQuyLuong));
            }
        }

        private double? _fHSBL;
        public double? FHSBL
        {
            get => _fHSBL.GetValueOrDefault();
            set
            {
                SetProperty(ref _fHSBL, value);
                OnPropertyChanged(nameof(FTongQuyLuong));
            }
        }

        public double? FTongQuyLuong => FLuongChinh.GetValueOrDefault() + FPCChucVu.GetValueOrDefault() + FPCTNNghe.GetValueOrDefault() + FPCTNVuotKhung.GetValueOrDefault() + FNghiOm.GetValueOrDefault() + FHSBL.GetValueOrDefault();

        public bool IsHasTruyThuData => FTruyThuBHXHNLD != 0 || FTruyThuBHXHNSD != 0 || FTruyThuBHYTNLD != 0 || FTruyThuBHYTNSD != 0 || FTruyThuBHTNNLD != 0 || FTruyThuBHTNNSD != 0
           || FTruyThuBHXHNLD != null || FTruyThuBHXHNSD != null || FTruyThuBHYTNLD != null || FTruyThuBHYTNSD != null || FTruyThuBHTNNLD != null || FTruyThuBHTNNSD != null;

    }
}
