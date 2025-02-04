namespace VTS.QLNS.CTC.App.Model.Import
{
    [Sheet(3, "2.2 DS Nhiệm vụ chi BQP", 6, 0)]
    public class NhDmNhiemVuChiImportKhoiTao : BindableBase
    {
        [ColumnAttribute("STT", 0)]
        public string STT { get; set; }

        [ColumnAttribute("Mã nhiệm vụ chi", 1)]
        public string MaNhiemVuChi { get; set; }

        [ColumnAttribute("Thuộc mã nhiệm vụ chi cha", 2)]
        public string MaNhiemVuChiCha { get; set; }

        [ColumnAttribute("Mã chương trình TTCP tương ứng", 3)]
        public string MaChươngTrinhTTCP { get; set; }

        [ColumnAttribute("Tên nhiệm vụ chi BQP", 4)]
        public string TenNhiemVuChiBQP { get; set; }
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }
        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }
    }
}
