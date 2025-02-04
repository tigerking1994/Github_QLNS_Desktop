using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class VoucherListModel : ModelBase
    {
        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }

        private int? _iSoChungTuIndex;
        public int? ISoChungTuIndex
        {
            get => _iSoChungTuIndex;
            set => SetProperty(ref _iSoChungTuIndex, value);
        }

        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private string _iIdMaDonVi;
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
        }

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private string _sXauNoiMa;
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private string _sNoiDung;
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private string _sLoai;
        public string SLoai
        {
            get => _sLoai;
            set => SetProperty(ref _sLoai, value);
        }

        private int _iThangQuyLoai;
        public int IThangQuyLoai
        {
            get => _iThangQuyLoai;
            set => SetProperty(ref _iThangQuyLoai, value);
        }

        private int _iThangQuy;
        public int IThangQuy
        {
            get => _iThangQuy;
            set => SetProperty(ref _iThangQuy, value);
        }

        private string _sThangQuyMoTa;
        public string SThangQuyMoTa
        {
            get => _sThangQuyMoTa;
            set => SetProperty(ref _sThangQuyMoTa, value);
        }

        private double _fTongTuChi;
        public double FTongTuChi
        {
            get => _fTongTuChi;
            set => SetProperty(ref _fTongTuChi, value);
        }

        private double _fTongHienVat;
        public double FTongHienVat
        {
            get => _fTongHienVat;
            set => SetProperty(ref _fTongHienVat, value);
        }

        private int _iNamNganSach;
        public int INamNganSach
        {
            get => _iNamNganSach;
            set => SetProperty(ref _iNamNganSach, value);
        }

        private int _iIdMaNguonNganSach;
        public int IIdMaNguonNganSach
        {
            get => _iIdMaNguonNganSach;
            set => SetProperty(ref _iIdMaNguonNganSach, value);
        }

        private int? _iNamLamViec;
        public int? INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private DateTime? _dNgayTao;
        public DateTime? DNgayTao
        {
            get => _dNgayTao;
            set => SetProperty(ref _dNgayTao, value);
        }

        private string _sNguoiTao;
        public string SNguoiTao
        {
            get => _sNguoiTao;
            set => SetProperty(ref _sNguoiTao, value);
        }

        private DateTime? _dNgaySua;
        public DateTime? DNgaySua
        {
            get => _dNgaySua;
            set => SetProperty(ref _dNgaySua, value);
        }

        private string _sNguoiSua;
        public string SNguoiSua
        {
            get => _sNguoiSua;
            set => SetProperty(ref _sNguoiSua, value);
        }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        private Guid? _iIdDeTaiId;
        public Guid? IIdDeTaiId
        {
            get => _iIdDeTaiId;
            set => SetProperty(ref _iIdDeTaiId, value);
        }
    }
}
