using System;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NsNc3YChungTuChiTietModel : DetailModelBase
    {
        public Guid IIdCtsoKiemTra { get; set; }
        public Guid IdParent { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SKhoiDonVi { get; set; }
        public string TenDonViIdDonVi
        {
            get
            {
                if (IIdMaDonVi != null && STenDonVi != null)
                    return string.Concat(IIdMaDonVi, " - ", STenDonVi);
                return null;
            }
        }
        public Guid IIdMlskt { get; set; }
        public string Stt { get; set; }
        public bool IsFirstParentRow { get; set; }
        public bool IsRemainRow { get; set; }
        public string SMoTa { get; set; }
        public int ILoai { get; set; }
        public int INamLamViec { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }        
        public int Level { get; set; }
        public int? ILoaiChungTu { get; set; }
        public string Nganh { get; set; }
        public string NganhParent { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }

        private bool _isUpdateCanCu;
        public bool IsUpdateCanCu
        {
            get => _isUpdateCanCu;
            set => SetProperty(ref _isUpdateCanCu, value);
        }

        private string _sKyHieu;

        public string SKyHieu
        {
            get => _sKyHieu;
            set => SetProperty(ref _sKyHieu, value);
        }

        private string _sKyHieuCu;

        public string SKyHieuCu
        {
            get => _sKyHieuCu;
            set => SetProperty(ref _sKyHieuCu, value);
        }

        private double _fDuToan;
        public double FDuToan
        {
            set
            {
                SetProperty(ref _fDuToan, value);
            }
            get => _fDuToan;
        }

        private double _fUocTH;
        public double FUocTH
        {
            set
            {
                SetProperty(ref _fUocTH, value);
                OnPropertyChanged(nameof(FUTHNCNam1));
            }
            get => _fUocTH;
        }

        private double _fNCNam1;
        public double FNCNam1
        {
            set
            {
                SetProperty(ref _fNCNam1, value);
                OnPropertyChanged(nameof(FUTHNCNam1));
                OnPropertyChanged(nameof(TongNhuCau));
            }
            get => _fNCNam1;
        }

        public double FUTHNCNam1
        {
            set
            {
            }
            get => FUocTH != 0 ? (FNCNam1/FUocTH)*100 : 0;
        }

        private double _fNCNam2;
        public double FNCNam2
        {
            set
            {
                SetProperty(ref _fNCNam2, value);
                OnPropertyChanged(nameof(FUTHNCNam2));
                OnPropertyChanged(nameof(TongNhuCau));
            }
            get => _fNCNam2;
        }

        public double FUTHNCNam2
        {
            set
            {
            }
            get => FNCNam1 != 0 ? (FNCNam2 / FNCNam1)*100 : 0;
        }

        private double _fNCNam3;
        public double FNCNam3
        {
            set
            {
                SetProperty(ref _fNCNam3, value);
                OnPropertyChanged(nameof(FUTHNCNam3));
                OnPropertyChanged(nameof(TongNhuCau));
            }
            get => _fNCNam3;
        }

        public double FUTHNCNam3
        {
            set
            {
            }
            get => FNCNam2 != 0 ? (FNCNam3 / FNCNam2)*100 : 0;
        }

        public double TongNhuCau
        {
            set { }
            get
            {
                return FNCNam1 + FNCNam2 + FNCNam3;
            }
        }    
        
        public bool HasData => !IsHangCha && (FDuToan != 0 || FUocTH != 0 || FNCNam1 != 0 || FNCNam2 != 0 || FNCNam3 != 0 || !string.IsNullOrEmpty(SGhiChu));
        public bool isPrintDisplay { get; set; }
        public Guid IIdCtsoKiemTraChild { get; set; }
        
    }
}