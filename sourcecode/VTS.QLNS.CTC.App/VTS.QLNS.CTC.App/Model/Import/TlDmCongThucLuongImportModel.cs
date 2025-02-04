using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Danh sách công thức", 4, 0)]
    public class TlDmCongThucLuongImportModel : BindableBase
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
        public string Stt { get; set; }
        [ColumnAttribute("", 1)]
        public string MaCot { get; set; }
        [ColumnAttribute("", 2)]
        public string TenCot { get; set; }
        [ColumnAttribute("", 3)]
        public string CongThuc { get; set; }
        [ColumnAttribute("", 4)]
        public string NoiDung { get; set; }
        [ColumnAttribute("", 5)]
        public string MaTinhChat { get; set; }
        [ColumnAttribute("", 6)]
        public string MaCachTl { get; set; }
    }
}
