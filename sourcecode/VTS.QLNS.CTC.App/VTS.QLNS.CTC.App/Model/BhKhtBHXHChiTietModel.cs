using System;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhKhtBHXHChiTietModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid KhtBHXHId { get; set; }
        public Guid? IIDLoaiDoiTuong { get; set; }
        public string STenLoaiDoiTuong { get; set; }
        private int? _iQSBQNam;
        public int? IQSBQNam
        {
            get => _iQSBQNam;
            set
            {
                SetProperty(ref _iQSBQNam, value);
                OnPropertyChanged(nameof(FLuongChinh));
                OnPropertyChanged(nameof(FTongQuyTienLuongNam));
                OnPropertyChanged(nameof(FThuBHXHNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHXHNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fLuongChinh;
        public double? FLuongChinh
        {
            get => _fLuongChinh;
            //{
            //    if (SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
            //    {
            //        return _fLuongChinh = IQSBQNam.GetValueOrDefault() * 12 * (double)DHeSoLCS;
            //    }
            //    return _fLuongChinh;
            //}
            set
            {
                SetProperty(ref _fLuongChinh, value);
                OnPropertyChanged(nameof(FTongQuyTienLuongNam));
                OnPropertyChanged(nameof(FThuBHXHNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHXHNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
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
                OnPropertyChanged(nameof(FThuBHXHNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHXHNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
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
                OnPropertyChanged(nameof(FThuBHXHNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHXHNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
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
                OnPropertyChanged(nameof(FThuBHXHNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHXHNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
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
                OnPropertyChanged(nameof(FThuBHXHNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHXHNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fHSBL;
        public double? FHSBL
        {
            get => _fHSBL.GetValueOrDefault();
            set
            {
                SetProperty(ref _fHSBL, value);
                OnPropertyChanged(nameof(FTongQuyTienLuongNam));
                OnPropertyChanged(nameof(FThuBHXHNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHXHNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        public double? FTongQuyTienLuongNam
        {
            get => FLuongChinh.GetValueOrDefault() + FPhuCapChucVu.GetValueOrDefault() + FPCTNNghe.GetValueOrDefault() + FPCTNVuotKhung.GetValueOrDefault() + FNghiOm.GetValueOrDefault() + FHSBL.GetValueOrDefault();
            set
            {
                OnPropertyChanged(nameof(FThuBHXHNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHXHNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHYTNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiLaoDong));
                OnPropertyChanged(nameof(FThuBHTNNguoiSuDungLaoDong));
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fTongThuBHXH;
        public double? FTongThuBHXH
        {
            get
            {
                return Math.Round(FThuBHXHNguoiLaoDong.GetValueOrDefault(), 0) + Math.Round(FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault(), 0);
            }
            set
            {
                SetProperty(ref _fTongThuBHXH, value);
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fTongThuBHYT;
        public double? FTongThuBHYT
        {
            get => Math.Round(FThuBHYTNguoiLaoDong.GetValueOrDefault(), 0) + Math.Round(FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault(), 0);
            set
            {
                SetProperty(ref _fTongThuBHYT, value);
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fTongThuBHTN;
        public double? FTongThuBHTN
        {
            get => Math.Round(FThuBHTNNguoiLaoDong.GetValueOrDefault(), 0) + Math.Round(FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault(), 0);
            set
            {
                SetProperty(ref _fTongThuBHTN, value);
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        public double? FTongCong => FTongThuBHXH.GetValueOrDefault() + FTongThuBHYT.GetValueOrDefault() + FTongThuBHTN.GetValueOrDefault();

        private double? _fThuBHXHNguoiLaoDong;
        public double? FThuBHXHNguoiLaoDong
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHXHNLD.GetValueOrDefault();
                else return _fThuBHXHNguoiLaoDong;
            }
            set
            {
                SetProperty(ref _fThuBHXHNguoiLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHXHNguoiSuDungLaoDong;
        public double? FThuBHXHNguoiSuDungLaoDong
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHXHNSD.GetValueOrDefault();
                else return _fThuBHXHNguoiSuDungLaoDong;
            }
            set
            {
                SetProperty(ref _fThuBHXHNguoiSuDungLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHYTNguoiLaoDong;
        public double? FThuBHYTNguoiLaoDong
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHYTNLD.GetValueOrDefault();
                else return _fThuBHYTNguoiLaoDong;
            }
            set
            {
                SetProperty(ref _fThuBHYTNguoiLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHYTNguoiSuDungLaoDong;
        public double? FThuBHYTNguoiSuDungLaoDong
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHYTNSD.GetValueOrDefault();
                else return _fThuBHYTNguoiSuDungLaoDong;
            }
            set
            {
                SetProperty(ref _fThuBHYTNguoiSuDungLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHTNNguoiLaoDong;
        public double? FThuBHTNNguoiLaoDong
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHTNNLD.GetValueOrDefault();
                else return _fThuBHTNNguoiLaoDong;
            }
            set
            {
                SetProperty(ref _fThuBHTNNguoiLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHTNNguoiSuDungLaoDong;
        public double? FThuBHTNNguoiSuDungLaoDong
        {
            get
            {
                if (!IsHangCha)
                    return FTongQuyTienLuongNam.GetValueOrDefault() * FTyLeBHTNNSD.GetValueOrDefault();
                else return _fThuBHTNNguoiSuDungLaoDong;
            }
            set
            {
                SetProperty(ref _fThuBHTNNguoiSuDungLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public Guid IIDMucLucNganSach { get; set; }
        public Guid IdParent { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SKhoiDonVi { get; set; }
        public int IndexDataState { get; set; }
        public string Stt { get; set; }
        public bool IsFirstParentRow { get; set; }
        public bool IsRemainRow { get; set; }
        public string SMoTa { get; set; }
        public int ILoai { get; set; }
        public int? INamLamViec { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public double FHienVat { get; set; }
        public int Level { get; set; }
        public int? ILoaiChungTu { get; set; }
        public string SM { get; set; }
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public string STenBhMLNS { get; set; }
        public bool? BHangCha { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLNS { get; set; }
        public string SMaPhuCap { get; set; }
        public string SMaCapBac { get; set; }
        public decimal? DHeSoLCS { get; set; }

        public bool HasDataToPrint => IQSBQNam.GetValueOrDefault() != 0 || FLuongChinh.GetValueOrDefault() != 0 || FPhuCapChucVu.GetValueOrDefault() != 0 ||
            FPCTNNghe.GetValueOrDefault() != 0 || FPCTNVuotKhung.GetValueOrDefault() != 0 || FNghiOm.GetValueOrDefault() != 0 ||
            FHSBL.GetValueOrDefault() != 0 || FThuBHXHNguoiLaoDong.GetValueOrDefault() != 0 || FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault() != 0 ||
            FThuBHYTNguoiLaoDong.GetValueOrDefault() != 0 || FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault() != 0 || FThuBHTNNguoiLaoDong.GetValueOrDefault() != 0 ||
            FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault() != 0;

        public string SL { get; set; }
        public string SK { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }

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
    }
}
