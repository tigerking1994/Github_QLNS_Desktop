using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(5, "Danh mục cán bộ phụ cấp", 4, 0)]
    public class TlDmCanBoPhuCapImportModel : BindableBase
    {
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

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }
        [ColumnAttribute("", 0)]
        public string MaCbo { get; set; }
        [ColumnAttribute("", 1)]
        public string MaPhuCap { get; set; }
        [ColumnAttribute("", 2)]
        public string GiaTri { get; set; }
        [ColumnAttribute("", 3)]
        public string HeSo { get; set; }
        [ColumnAttribute("", 4)]
        public string MaKmcp { get; set; }
        [ColumnAttribute("", 5)]
        public string CongThuc { get; set; }
        [ColumnAttribute("", 6)]
        public string PhanTramCt { get; set; }
        [ColumnAttribute("", 7)]
        public string Chon { get; set; }
        [ColumnAttribute("", 8)]
        public string HuongPcSn { get; set; }
        [ColumnAttribute("", 9)]
        public string Flag { get; set; }
    }
}
