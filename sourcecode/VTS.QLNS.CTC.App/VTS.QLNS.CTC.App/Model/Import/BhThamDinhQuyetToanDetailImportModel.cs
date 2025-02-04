namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 8, 0)]
    public class BhThamDinhQuyetToanDetailImportModel : BindableBase
    {
        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

        private string _iMa;
        [ColumnAttribute("Mã", 0)]
        public string IMa
        {
            get => _iMa;
            set => SetProperty(ref _iMa, value);
        }

        private string _sNoiDung;
        [ColumnAttribute("Nội dung", 1)]
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private string _sSoBaoCao;
        [ColumnAttribute("Số báo cáo", 2)]
        public string SSoBaoCao
        {
            get => _sSoBaoCao;
            set => SetProperty(ref _sSoBaoCao, value);
        }
        private string _sSoThamDinh;
        [ColumnAttribute("Số thẩm định", 3)]
        public string SSoThamDinh
        {
            get => _sSoThamDinh;
            set => SetProperty(ref _sSoThamDinh, value);
        }

        private string _sChenhLech;
        [ColumnAttribute("Chênh lệch", 4)]
        public string SChenhLech
        {
            get => _sChenhLech;
            set => SetProperty(ref _sChenhLech, value);
        }

        private string _sQuanNhan;
        [ColumnAttribute("Quân nhân", 5)]
        public string SQuanNhan
        {
            get => _sQuanNhan;
            set => SetProperty(ref _sQuanNhan, value);
        }
        private string _sSoCNVLDHD;
        [ColumnAttribute("Số CNV - LDHD", 6)]
        public string SSoCNVLDHD
        {
            get => _sSoCNVLDHD;
            set => SetProperty(ref _sSoCNVLDHD, value);
        }
        private string _sTongSo;
        [ColumnAttribute("Tổng số", 7)]
        public string STongSo
        {
            get => _sTongSo;
            set => SetProperty(ref _sTongSo, value);
        }
        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 8)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        //public double FChenhLech => Math.Abs(FSoThamDinh - FSoBaoCao);
        //public double FTongSo => FQuanNhan + FCNVLDHD;

        public bool HasData => !string.IsNullOrWhiteSpace(SNoiDung) || !string.IsNullOrWhiteSpace(SSoBaoCao)
            || !string.IsNullOrWhiteSpace(SSoThamDinh) || !string.IsNullOrWhiteSpace(SSoCNVLDHD);
    }
}
