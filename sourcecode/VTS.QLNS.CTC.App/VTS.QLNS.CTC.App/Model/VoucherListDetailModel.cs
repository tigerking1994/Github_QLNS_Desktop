using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class VoucherListDetailModel : DetailModelBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public Guid CustomId { get; set; }

        private Guid? _iIdBkchungTu;
        public Guid? IIdBkchungTu
        {
            get => _iIdBkchungTu;
            set => SetProperty(ref _iIdBkchungTu, value);
        }

        private Guid _iIdMlns;
        public Guid IIdMlns
        {
            get => _iIdMlns;
            set => SetProperty(ref _iIdMlns, value);
        }

        private Guid? _iIdMlnsCha;
        public Guid? IIdMlnsCha
        {
            get => _iIdMlnsCha;
            set => SetProperty(ref _iIdMlnsCha, value);
        }

        private string _sXauNoiMa;
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sLns;
        public string SLns
        {
            get => _sLns;
            set => SetProperty(ref _sLns, value);
        }

        private string _sL;
        public string SL
        {
            get => _sL;
            set => SetProperty(ref _sL, value);
        }

        private string _sK;
        public string SK
        {
            get => _sK;
            set => SetProperty(ref _sK, value);
        }

        private string _sM;
        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTm;
        public string STm
        {
            get => _sTm;
            set => SetProperty(ref _sTm, value);
        }

        private string _sTtm;
        public string STtm
        {
            get => _sTtm;
            set => SetProperty(ref _sTtm, value);
        }

        private string _sNg;
        public string SNg
        {
            get => _sNg;
            set => SetProperty(ref _sNg, value);
        }

        private string _sTng;
        public string STng
        {
            get => _sTng;
            set => SetProperty(ref _sTng, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set => SetProperty(ref _bHangCha, value);
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

        private string _sLoai;
        public string SLoai
        {
            get => _sLoai;
            set => SetProperty(ref _sLoai, value);
        }

        private int? _iLoaiChi;
        public int? ILoaiChi
        {
            get => _iLoaiChi;
            set => SetProperty(ref _iLoaiChi, value);
        }

        private int? _iThangQuyLoai;
        public int? IThangQuyLoai
        {
            get => _iThangQuyLoai;
            set => SetProperty(ref _iThangQuyLoai, value);
        }

        private int? _iThangQuy;
        public int? IThangQuy
        {
            get => _iThangQuy;
            set => SetProperty(ref _iThangQuy, value);
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

        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }

        private DateTime _dNgayChungTu;
        public DateTime DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
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

        public bool HasData => !string.IsNullOrEmpty(SSoChungTu) || FTongTuChi != 0 || FTongHienVat != 0 || !string.IsNullOrEmpty(SMoTa) || !string.IsNullOrEmpty(SGhiChu) || !string.IsNullOrEmpty(IIdMaDonVi);

        public VoucherListDetailModel()
        {
            CustomId = Guid.NewGuid();
        }

        public VoucherListDetailModel(DateTime ngayChungTu)
        {
            CustomId = Guid.NewGuid();
            _dNgayChungTu = ngayChungTu;
        }

        private int _stt;
        public int Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }
    }
}
