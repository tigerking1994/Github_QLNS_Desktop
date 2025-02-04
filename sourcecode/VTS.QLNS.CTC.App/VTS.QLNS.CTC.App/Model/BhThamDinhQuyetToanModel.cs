using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhThamDinhQuyetToanModel : ModelBase
    {
        private int _iNamLamViec;
        public int INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private string _iID_MaDonVi;
        public string IID_MaDonVi
        {
            get => _iID_MaDonVi;
            set => SetProperty(ref _iID_MaDonVi, value);
        }
        private double _fSoBaoCao;
        public double FSoBaoCao
        {
            get => _fSoBaoCao;
            set => SetProperty(ref _fSoBaoCao, value);
        }
        private double _fSoThamDinh;
        public double FSoThamDinh
        {
            get => _fSoThamDinh;
            set => SetProperty(ref _fSoThamDinh, value);
        }
        private double _fQuanNhan;
        public double FQuanNhan
        {
            get => _fQuanNhan;
            set => SetProperty(ref _fQuanNhan, value);
        }
        private double _fCNVLDHD;
        public double FCNVLDHD
        {
            get => _fCNVLDHD;
            set => SetProperty(ref _fCNVLDHD, value);
        }
        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }
        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }
        private bool _bDaTongHop;
        public bool BDaTongHop
        {
            get => _bDaTongHop;
            set => SetProperty(ref _bDaTongHop, value);
        }

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        public string STongHop { get; set; }
        public bool IsLocked { get; set; }
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

        private string _sGiaiThichChenhLech;
        public string SGiaiThichChenhLech
        {
            get => _sGiaiThichChenhLech;
            set => SetProperty(ref _sGiaiThichChenhLech, value);
        }
        public bool IsSummaryVocher { get; set; }
        public bool IsChildSummary { get; set; }
        public string SoChungTuParent { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SMoTa { get; set; }

    }
}
