using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(1, "Danh mục phụ cấp", 4, 0)]
    public class TlDmPhuCapImportModel : BindableBase
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
        public string MaPhuCap { get; set; }
        [ColumnAttribute("", 2)]
        public string TenPhuCap { get; set; }
        [ColumnAttribute("", 3)]
        public string GiaTri { get; set; }
        [ColumnAttribute("", 4)]
        public string IthangToiDa { get; set; }
        [ColumnAttribute("", 5)]
        public string IsFormula { get; set; }
        [ColumnAttribute("", 6)]
        public string Chon { get; set; }
        [ColumnAttribute("", 7)]
        public string IsReadonly { get; set; }
        [ColumnAttribute("", 8)]
        public string Parent { get; set; }
        [ColumnAttribute("", 9)]
        public string ILoai { get; set; }
        [ColumnAttribute("", 10)]
        public string IDinhDang { get; set; }
        [ColumnAttribute("", 11)]
        public string BGiaTri { get; set; }
        [ColumnAttribute("", 12)]
        public string BHuongPcSn { get; set; }
        [ColumnAttribute("", 13)]
        public string BSaoChep { get; set; }
    }
}
