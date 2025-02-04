using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQttBHXHChiTietModel : DetailModelBase
    {
        public override Guid Id { get; set; }
        private bool _isCancel;
        public bool IsCancel
        {
            get => _isCancel;
            set => SetProperty(ref _isCancel, value);
        }
        public Guid? QttBHXHId { get; set; }
        public int? INamLamViec { get; set; }
        public string IIDMaDonVi { get; set; }
        public double? LCS { get; set; }
        private int? _iQSBQNam;
        public int? IQSBQNam
        {
            get => _iQSBQNam;
            set
            {
                SetProperty(ref _iQSBQNam, value);
                OnPropertyChanged(nameof(FThuBHXHNLD));
                OnPropertyChanged(nameof(FThuBHXHNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHXH));
                OnPropertyChanged(nameof(FThuBHYTNLD));
                OnPropertyChanged(nameof(FThuBHYTNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHYT));
                OnPropertyChanged(nameof(FThuBHTNNLD));
                OnPropertyChanged(nameof(FThuBHTNNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHTN));

            }
        }
        private double? _fLuongChinh;
        public double? FLuongChinh
        {
            get => _fLuongChinh;
            set
            {
                SetProperty(ref _fLuongChinh, value);
                OnPropertyChanged(nameof(FTongQuyTienLuongNam));
                OnPropertyChanged(nameof(FThuBHXHNLD));
                OnPropertyChanged(nameof(FThuBHXHNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHXH));
                OnPropertyChanged(nameof(FThuBHYTNLD));
                OnPropertyChanged(nameof(FThuBHYTNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHYT));
                OnPropertyChanged(nameof(FThuBHTNNLD));
                OnPropertyChanged(nameof(FThuBHTNNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }
        private double? _fPhuCapChucVu;
        public double? FPhuCapChucVu
        {
            get => _fPhuCapChucVu;
            set
            {
                SetProperty(ref _fPhuCapChucVu, value);
                OnPropertyChanged(nameof(FTongQuyTienLuongNam));
                OnPropertyChanged(nameof(FThuBHXHNLD));
                OnPropertyChanged(nameof(FThuBHXHNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHXH));
                OnPropertyChanged(nameof(FThuBHYTNLD));
                OnPropertyChanged(nameof(FThuBHYTNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHYT));
                OnPropertyChanged(nameof(FThuBHTNNLD));
                OnPropertyChanged(nameof(FThuBHTNNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }
        private double? _fPCTNNghe;
        public double? FPCTNNghe
        {
            get => _fPCTNNghe;
            set
            {
                SetProperty(ref _fPCTNNghe, value);
                OnPropertyChanged(nameof(FTongQuyTienLuongNam));
                OnPropertyChanged(nameof(FThuBHXHNLD));
                OnPropertyChanged(nameof(FThuBHXHNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHXH));
                OnPropertyChanged(nameof(FThuBHYTNLD));
                OnPropertyChanged(nameof(FThuBHYTNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHYT));
                OnPropertyChanged(nameof(FThuBHTNNLD));
                OnPropertyChanged(nameof(FThuBHTNNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }
        private double? _fPCTNVuotKhung;
        public double? FPCTNVuotKhung
        {
            get => _fPCTNVuotKhung;
            set
            {
                SetProperty(ref _fPCTNVuotKhung, value);
                OnPropertyChanged(nameof(FTongQuyTienLuongNam));
                OnPropertyChanged(nameof(FThuBHXHNLD));
                OnPropertyChanged(nameof(FThuBHXHNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHXH));
                OnPropertyChanged(nameof(FThuBHYTNLD));
                OnPropertyChanged(nameof(FThuBHYTNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHYT));
                OnPropertyChanged(nameof(FThuBHTNNLD));
                OnPropertyChanged(nameof(FThuBHTNNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }
        private double? _fNghiOm;
        public double? FNghiOm
        {
            get => _fNghiOm;
            set
            {
                SetProperty(ref _fNghiOm, value);
                OnPropertyChanged(nameof(FTongQuyTienLuongNam));
                OnPropertyChanged(nameof(FThuBHXHNLD));
                OnPropertyChanged(nameof(FThuBHXHNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHXH));
                OnPropertyChanged(nameof(FThuBHYTNLD));
                OnPropertyChanged(nameof(FThuBHYTNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHYT));
                OnPropertyChanged(nameof(FThuBHTNNLD));
                OnPropertyChanged(nameof(FThuBHTNNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }
        private double? _fHSBL;
        public double? FHSBL
        {
            get => _fHSBL;
            set
            {
                SetProperty(ref _fHSBL, value);
                OnPropertyChanged(nameof(FTongQuyTienLuongNam));
                OnPropertyChanged(nameof(FThuBHXHNLD));
                OnPropertyChanged(nameof(FThuBHXHNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHXH));
                OnPropertyChanged(nameof(FThuBHYTNLD));
                OnPropertyChanged(nameof(FThuBHYTNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHYT));
                OnPropertyChanged(nameof(FThuBHTNNLD));
                OnPropertyChanged(nameof(FThuBHTNNSD));
                OnPropertyChanged(nameof(FTongSoPhaiThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }
        public double? FTongQuyTienLuongNam
        {
            get => Math.Round(FLuongChinh.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                    + Math.Round(FPhuCapChucVu.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                    + Math.Round(FPCTNNghe.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                    + Math.Round(FPCTNVuotKhung.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                    + Math.Round(FNghiOm.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                    + Math.Round(FHSBL.GetValueOrDefault(), MidpointRounding.AwayFromZero);
        }
        private double? _fDuToan;
        public double? FDuToan
        {
            get => _fDuToan;
            set
            {
                SetProperty(ref _fDuToan, value);
                OnPropertyChanged(nameof(FConLai));
            }
        }
        private double? _fDaQuyetToan;
        public double? FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set
            {
                SetProperty(ref _fDaQuyetToan, value);
                OnPropertyChanged(nameof(FConLai));
            }
        }
        private double? _fConLai;
        public double? FConLai
        {
            get => _fConLai = Math.Round(FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                    - Math.Round(FDaQuyetToan.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                    - Math.Round(FTongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            set
            {
                SetProperty(ref _fConLai, value);
            }
        }

        private double? _fThuBHXHNLD;
        public double? FThuBHXHNLD
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHXHNLD.GetValueOrDefault();
                else return _fThuBHXHNLD;
            }
            set
            {
                SetProperty(ref _fThuBHXHNLD, value);
                OnPropertyChanged(nameof(FTongSoPhaiThuBHXH));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }

        private double? _fThuBHXHNSD;
        public double? FThuBHXHNSD
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHXHNSD.GetValueOrDefault();
                else return _fThuBHXHNSD;
            }
            set
            {
                SetProperty(ref _fThuBHXHNSD, value);
                OnPropertyChanged(nameof(FTongSoPhaiThuBHXH));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }

        public double? FTongSoPhaiThuBHXH
        {
            get
            {
                return Math.Round(FThuBHXHNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                        + Math.Round(FThuBHXHNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            }
            set
            {
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }

        private double? _fThuBHYTNLD;
        public double? FThuBHYTNLD
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHYTNLD.GetValueOrDefault();
                else return _fThuBHYTNLD;
            }
            set
            {
                SetProperty(ref _fThuBHYTNLD, value);
                OnPropertyChanged(nameof(FTongSoPhaiThuBHYT));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }

        private double? _fThuBHYTNSD;
        public double? FThuBHYTNSD
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHYTNSD.GetValueOrDefault();
                else return _fThuBHYTNSD;
            }
            set
            {
                SetProperty(ref _fThuBHYTNSD, value);
                OnPropertyChanged(nameof(FTongSoPhaiThuBHYT));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }
        public double? FTongSoPhaiThuBHYT
        {
            get
            {
                return Math.Round(FThuBHYTNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                    + Math.Round(FThuBHYTNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            }
            set
            {
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }

        private double? _fThuBHTNNLD;
        public double? FThuBHTNNLD
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHTNNLD;
                else return _fThuBHTNNLD;
            }
            set
            {
                SetProperty(ref _fThuBHTNNLD, value);
                OnPropertyChanged(nameof(FTongSoPhaiThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }

        private double? _fThuBHTNNSD;
        public double? FThuBHTNNSD
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHTNNSD;
                else return _fThuBHTNNSD;
            }
            set
            {
                SetProperty(ref _fThuBHTNNSD, value);
                OnPropertyChanged(nameof(FTongSoPhaiThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }
        public double? FTongSoPhaiThuBHTN
        {
            get
            {
                return Math.Round(FThuBHTNNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                    + Math.Round(FThuBHTNNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            }
            set
            {
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FConLai));
            }
        }
        private double? _fTongCong;
        public double? FTongCong
        {
            get => Math.Round(FTongSoPhaiThuBHXH.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                + Math.Round(FTongSoPhaiThuBHYT.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                + Math.Round(FTongSoPhaiThuBHTN.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            set
            {
                SetProperty(ref _fTongCong, value);
                OnPropertyChanged(nameof(FConLai));
            }
        }
        private string _fGhiChu;
        public string SGhiChu
        {
            get => _fGhiChu;
            set
            {
                SetProperty(ref _fGhiChu, value);
            }
        }
        private double? _fTyLeBHXHNSD;
        public double? FTyLeBHXHNSD
        {
            get => _fTyLeBHXHNSD;
            set
            {
                SetProperty(ref _fTyLeBHXHNSD, value);
            }
        }
        private double? _fTyLeBHXHNLD;
        public double? FTyLeBHXHNLD
        {
            get => _fTyLeBHXHNLD;
            set
            {
                SetProperty(ref _fTyLeBHXHNLD, value);
            }
        }
        private double? _fTyLeBHYTNSD;
        public double? FTyLeBHYTNSD
        {
            get => _fTyLeBHYTNSD;
            set
            {
                SetProperty(ref _fTyLeBHYTNSD, value);
            }
        }
        private double? _fTyLeBHYTNLD;
        public double? FTyLeBHYTNLD
        {
            get => _fTyLeBHYTNLD;
            set
            {
                SetProperty(ref _fTyLeBHYTNLD, value);
            }
        }
        private double? _fTyLeBHTNNSD;
        public double? FTyLeBHTNNSD
        {
            get => _fTyLeBHTNNSD;
            set
            {
                SetProperty(ref _fTyLeBHTNNSD, value);
            }
        }
        private double? _fTyLeBHTNNLD;
        public double? FTyLeBHTNNLD
        {
            get => _fTyLeBHTNNLD;
            set
            {
                SetProperty(ref _fTyLeBHTNNLD, value);
            }
        }
        private double? _fHeSoLayQuyLuong;
        public double? FHeSoLayQuyLuong
        {
            get => _fHeSoLayQuyLuong;
            set
            {
                SetProperty(ref _fHeSoLayQuyLuong, value);
            }
        }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid IIDMLNS { get; set; }
        public Guid IIDMLNSCha { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLns { get; set; }
        public string STenBhMLNS { get; set; }
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public bool? BHangCha { get; set; }
        public bool IsHasData => FThuBHXHNLD != 0 || FThuBHXHNSD != 0 || FThuBHYTNLD != 0 || FThuBHYTNSD != 0 || FThuBHTNNLD != 0 || FThuBHTNNSD != 0
            || FDuToan != 0 || FDaQuyetToan != 0 || FConLai != 0 || !string.IsNullOrEmpty(SGhiChu);
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string SMaCapBac { get; set; }
        public bool IsHasBaseSalaryData => IQSBQNam.GetValueOrDefault() != 0 || FLuongChinh.GetValueOrDefault() != 0 || FPhuCapChucVu.GetValueOrDefault() != 0
            || FPCTNNghe.GetValueOrDefault() != 0 || FPCTNVuotKhung.GetValueOrDefault() != 0 || FNghiOm.GetValueOrDefault() != 0 || FHSBL.GetValueOrDefault() != 0;

        public bool IsEmptyData => IQSBQNam.GetValueOrDefault() != 0 && FLuongChinh.GetValueOrDefault() != 0 && FPhuCapChucVu.GetValueOrDefault() != 0
            && FPCTNNghe.GetValueOrDefault() != 0 && FPCTNVuotKhung.GetValueOrDefault() != 0 && FNghiOm.GetValueOrDefault() != 0 && FHSBL.GetValueOrDefault() != 0;

        public string SNsLuongChinh { get; set; }
        public string SNsPCCV { get; set; }
        public string SNsPCTN { get; set; }
        public string SNsPCTNVK { get; set; }
        public string SNsHSBL { get; set; }

    }
}
